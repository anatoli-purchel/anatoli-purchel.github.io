var path = require('path');
var webpack = require('webpack');
var extractTextPlugin = require('extract-text-webpack-plugin');
var commonConfig = require('./webpack.config.common');

module.exports =
  { devtool: 'source-map',
    entry: commonConfig.resolve('./PersonalSite.fsproj'),
    output:
      { filename: 'application.js',
        path: commonConfig.resolve('./public/assets'),
        publicPath: '/assets' },
    plugins:
      [ commonConfig.plugins.css ],
    resolve:
      { modules:
          [ 'node_modules',
            commonConfig.resolve('./node_modules/') ] },
    devServer:
      { contentBase: commonConfig.resolve('./public'),
        port: 8070 },
    module:
      { rules:
          [ commonConfig.loaders.fable,
            commonConfig.loaders.javascript,
            commonConfig.loaders.sass,
            commonConfig.loaders.files,
            commonConfig.loaders.bootstrap ] } };
