using System;
using System.Threading.Tasks;
using Godot;
using Microsoft.ClearScript.V8;

namespace Spectral.React
{

    /// <summary>
    /// A helper class that uses async / await and Task.delay
    /// to create an implementation of setTimeout. 
    /// There is only ever *one* task that is being awaited, which is created from a Schedule call.
    /// All setTimeout and setInterval calls are created in a queue.
    /// If a new timeout is added, the existing task is canceled, and a new one created, and that function
    /// added to the timeout queue in the order in which they should execute.
    /// 
    /// The task always counts down to the first 'timeout' that the app is waiting for. When the
    /// delay is reached, the first function in the timeout queue is executed, and then a new Schedule
    /// is called with the time required to reach the next timeout function. And so on and so forth.
    /// </summary>
    // code inspired from https://github.com/microsoft/ClearScript/issues/475
    public sealed class TimerImpl
    {
        private System.Threading.CancellationTokenSource _token;
        private Func<double> _callback = () => System.Threading.Timeout.Infinite;

        public void Initialize(dynamic callback) => _callback = () => (double)callback();

        public async void Schedule(double delay)
        {
            if (delay < 0)
            {
                if (_token != null)
                {
                    _token.Cancel();
                    _token = null;
                }
            }
            else if (delay == 0)
            {
                Schedule(_callback());
            }
            else
            {
                _token ??= new System.Threading.CancellationTokenSource();
                try {
                    await Task.Delay((int)delay, _token.Token);
                    Schedule(_callback());
                } catch (TaskCanceledException e) {
                    // this is normal
                    GD.Print("we canceled a task!");
                }
                
            }
        }
    }

    public class SetTimeout
    {
        public SetTimeout(V8ScriptEngine engine)
        {
            dynamic setup = engine.Evaluate(
                @"(impl => {
    let queue = [], nextId = 0;
    const maxId = 1000000000000, getNextId = () => nextId = (nextId % maxId) + 1;
    const add = entry => {
        const index = queue.findIndex(element => element.due > entry.due);
        index >= 0 ? queue.splice(index, 0, entry) : queue.push(entry);
    }
    function set(periodic, func, delay) {
        const id = getNextId(), now = Date.now(), args = [...arguments].slice(3);
        add({ id, periodic, func: func, delay, due: now + delay });
        impl.Schedule(queue[0].due - now);
        return id;
    };
    function clear(id) {
        queue = queue.filter(entry => entry.id != id);
        impl.Schedule(queue.length > 0 ? queue[0].due - Date.now() : -1);
    };
    globalThis.setTimeout = set.bind(undefined, false);
    globalThis.setInterval = set.bind(undefined, true);
    globalThis.clearTimeout = globalThis.clearInterval = clear.bind();
    impl.Initialize(() => {
        const now = Date.now();
        while ((queue.length > 0) && (now >= queue[0].due)) {
            const entry = queue.shift();
            if (entry.periodic) add({ ...entry, due: now + entry.delay });
            entry.func();
        }
        return queue.length > 0 ? queue[0].due - now : -1;
    });
})"
            );
            setup(new TimerImpl());

            // HACK: setImmediate as a 'do this now' function
            engine.Execute(
                @"
            globalThis.setImmediate = (func) => {
                setTimeout(func, 1)
            }
        "
            );
        }
    }

}
