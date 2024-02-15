# React GD

React.js integration with the Godot Game Engine by using [Microsoft's Clearscript](https://github.com/microsoft/ClearScript) to run JS code via [Google's v8](https://v8.dev/).

Runs on Godot 4.2.1's Mono build.

> This is not production-ready code! Nor is any support or maintenance going to be expected. Use at your own risk.

## Running the sample

This is not exactly practical at the moment, so run at your own risk!

There are both NuGet and NPM dependencies, so you'll need to install them after downloading the game project:

```bash
# in the project directory
$ dotnet restore
$ cd app
$ npm install
```

The React app lives in the `app` folder, while the binding code to make it work in Godot lives in the `react` folder -- **all** of the UI should go into the `app/src` folder as `.ts` files.

This project uses `esbuild` to build the JS app, which should've been installed when you installed dependencies. You can build the app as follows using:

```bash
# in the ./app directory
$ node build

# or use this to automatically rebuild on changes
$ node build -w
```

## How do I use this in my own project?

Uh. I'm not sure I recommend it??? But if you really want to:

1. Copy the `react` folder into the project directory of your Godot game. You'll need Godot 4.2 or up.
2. Install `Microsoft.ClearScript.Complete` in your project via dotnet.
3. Go into the `react` folder and run `node scripts/bootstrap-app` from there. This will create an `app` folder where your React code should live.
4. Run `npm install` inside of the `app` folder.
5. Add the `ReactRoot.tscn` scene to the root of your game. (Alternatively, since it is a fairly simple scene: Create a `CanvasLayer` node at the root of your game, and attach the `react/gd_nodes/Document.cs` script to it.)

Then it should work? Some limitations:

* You can only have one instance of React in your game, so if you want to switch UI views, you'll have to have a router from within your React app.
* Likewise, things might go bad if you at any point remove the node containing `Document.cs` from the node tree.
* This doesn't use the HTML DOM! So any React libraries that rely on the DOM cannot be used. State management libraries like Redux can, however, since they care only about the react core.

## How do you send game logic back and forth between JS and C#?

I haven't thought too far yet...but some initial thoughts.

Ideally, there's a one-directional flow. JS calls methods from C#, but C# shouldn't have to call anything from JS.

The entry point and root node of the React app is a `Document`, which can be found at `react/gd-nodes/Document.cs`. The v8 engine is defined there, as are all the functions to create and update components.

The v8 engine is exposed from the `Document` as the `Engine` variable, and thus it's possible to add other C# objects to it to send them to Javascript. From those objects, you can directly call functions as if they were in C#.

### Events?

There should also be a way to define `Actions` in C#, then `Invoke` them and have the results automatically be sent to JS, but I haven't quite figured that out yet `:^)`

## Other Libraries

There's some code in here that aren't related to React (that isn't an npm or nuget package), mostly for convenience as I was writing out this demo and seeing this project's viability. They're listed as follows:

* `common/WindowManagement.cs` - helper node to ensure the game window starts at the right size on high-DPI screens. I wrote this, and I copy and paste it into every game.
* `vendor/DirectoryWatcher.gd` - https://github.com/KoBeWi/Godot-Directory-Watcher - helper node written in GDScript (hooray, mixing GDScript and C#!) that is used to implement a 'live reload' function by watching when the react scripts change and reloading the Document node.
* `addons/godot-css-theme` - https://github.com/kuma-gee/godot-css-theme - used to turn CSS into Godot theme objects, which is just handy.
* `react/utils/SetTimeout.cs` - original version retrieved from https://github.com/microsoft/ClearScript/issues/475, with some modifications that I made to have it work. I barely understand it myself...

