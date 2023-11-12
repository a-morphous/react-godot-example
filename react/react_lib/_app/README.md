# React App

This folder contains all of the code for the Javascript React part of the app!

You'll want to run `npm install` in this directory to install dependencies, and then you can use:

```
$ node build # build the JS file that will be consumed by the Godot app

# or this to live watch the source files and rebuild as needed.
$ node build -w
```

The built code will end up in `dist/index.js`.

> NOTE: Right now, app paths are hardcoded, so if you change the name of the `app` folder or where the built files end up, you'll need to also change the path in `Document.cs`'s `Setup()` function.