var path = require('path');
var webpack = require('webpack');
var extractTextPlugin = require('extract-text-webpack-plugin');

var resolve = filePath => path.join(__dirname, filePath);

var babelOptions = {
  presets: [['es2015', { 'modules': false }]],
  plugins: ['transform-runtime']
};

var plugins =
  { css: new extractTextPlugin('application.css') }

var loaders =
  { fable:
      { test: /\.fs(x|proj)?$/,
        use: 
          { loader: 'fable-loader',
            options:
              { babel: babelOptions,
                define:
                  ['DEBUG'] } } },
    javascript:
      { test: /\.js$/,
        exclude: /node_modules/,
        use:
          { loader: 'babel-loader',
            options: babelOptions } },
    sass:
      { test: /\.sass$/,
        use:
          [ "style-loader",
            "css-loader",
            "sass-loader" ] },
    files:
      { test: /\.(png|jpg|gif|eot|woff2|ttf|woff|svg)$/,
        use: 'url-loader?limit=800&name=[name].[ext]' },
    bootstrap:
      { test: /bootstrap-sass\/assets\/javascripts\//,
        use: 'imports-loader?jQuery=jquery' } };

module.exports =
  { loaders,
    plugins,
    babelOptions,
    resolve };
