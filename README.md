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
# in the app directory
$ node build

# or use this to automatically rebuild on changes
$ node build -w
```

## How do you send game logic back and forth between JS and C#?

I haven't thought too far yet...but some initial thoughts.

Ideally, there's a one-directional flow. JS calls methods from C#, but C# shouldn't have to call anything from JS.

The entry point and root node of the React app is a `Document`, which can be found at `react/gd-nodes/Document.cs`. The v8 engine is defined there, as are all the functions to create and update components.

The v8 engine is exposed from the `Document` as the `Engine` variable, and thus it's possible to add other C# objects to it to send them to Javascript. From those objects, you can directly call functions as if they were in C#.

### Events?

There should also be a way to define `Actions` in C#, then `Invoke` them and have the results automatically be sent to JS, but I haven't quite figured that out yet `:^)`