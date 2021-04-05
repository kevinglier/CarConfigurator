const path = require("path");

module.exports = {
    configureWebpack: config => {
        if (process.env.NODE_ENV === "production") {
            config.output.filename = '[name].min.js'
            config.output.chunkFilename = '[name].min.js'
        } else {
            config.output.filename = '[name].js'
            config.output.chunkFilename = '[name].js';
        }
    },
    chainWebpack: config => {
        config.resolve.alias
            .set("@api", path.resolve(__dirname, "./src/api"));

        config.plugins.delete("hmr"); // Disable Hot Update
    },
    devServer: {
        hot: false,
        liveReload: false
    }
}