import * as esbuild from 'esbuild'

import minimist from 'minimist'
var argv = minimist(process.argv.slice(2))

const build = async () => {
	/**
	 * @type {esbuild.BuildOptions}
	 */
	const options = {
		entryPoints: ["./src/index.tsx"],
		outfile: "./dist/index.js",
		bundle: true,
		format: "esm",
		target: "es2017",
		
	}
	if (argv.w) {
		const context = await esbuild.context(options)
		await context.watch()
	} else {
		await esbuild.build(options)
	}
}
build()
