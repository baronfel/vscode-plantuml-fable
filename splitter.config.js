/*

Configuration file for fable-splitter, the non-webpack Fable compiler frontend.
Docs can be found at https://github.com/fable-compiler/Fable/blob/master/src/fable-splitter/README.md

*/

const path = require('path');
const resolve = (relPath) => path.join(__dirname, relPath);

module.exports = {
    // path to project file for the 'entry point'
    entry: resolve("src/VSCode.PlantUML.Fable.fsproj"),
    // path to output directory. You'll end up with one js file per input fs file in this directory
    outDir: resolve("build"),
    // only compile files that are referenced in the fsproj,
    allFiles: false
};