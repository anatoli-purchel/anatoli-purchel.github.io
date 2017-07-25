var webpack = require('webpack');
var devConfig = require('./webpack.config');
var commonConfig = require('./webpack.config.common');

console.log('Bundling for production...');

var plugins =
  Object.assign({}, commonConfig.plugins,
    { uglifyjs:
        new webpack.optimize.UglifyJsPlugin(
          { minimize: true, comments: false }),
      commonsChunk:
        new webpack.optimize.CommonsChunkPlugin(
          { names:
              [ 'vendor',
                'manifest' ] }) })

module.exports = Object.assign({}, devConfig,
  { entry:
      { vendor:
          [ 'react',
            'react-dom',
            'whatwg-fetch' ],
        application: commonConfig.resolve('./PersonalSite.fsproj') },
    output:
      Object.assign({}, devConfig.output,
        { filename: '[name].js' }),
    plugins:
      [ plugins.commonsChunk,
        plugins.css,
        plugins.uglifyjs ] });
