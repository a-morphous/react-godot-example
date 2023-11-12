#!/usr/bin/env node

/**
 * Run this file to create a new app directory, or update the react_lib of an existing one if it exists already.
 */

import fs from "fs"
import path from "path"

import { dirname } from 'path';
import { fileURLToPath } from 'url';

const __dirname = dirname(fileURLToPath(import.meta.url));

const APP_FOLDER_NAME = "app"
const PROJECT_DIR = path.resolve(__dirname, "..", "..");

const APP_PATH = path.resolve(PROJECT_DIR, APP_FOLDER_NAME);

if (fs.existsSync(APP_PATH)) {
	// replace react-lib, but nothing else
	copyFolderSync(path.resolve(__dirname, "..", "react_lib", "_lib"), path.resolve(APP_PATH, "react_lib"))
} else {
	// we need to create the whole app folder!
	copyFolderSync(path.resolve(__dirname, "..", "react_lib", "_app"), path.resolve(APP_PATH))
	copyFolderSync(path.resolve(__dirname, "..", "react_lib", "_lib"), path.resolve(APP_PATH, "react_lib"))
}

function copyFolderSync(from, to) {
    fs.mkdirSync(to);
    fs.readdirSync(from).forEach(element => {
        if (fs.lstatSync(path.join(from, element)).isFile()) {
            fs.copyFileSync(path.join(from, element), path.join(to, element));
        } else {
            copyFolderSync(path.join(from, element), path.join(to, element));
        }
    });
}
