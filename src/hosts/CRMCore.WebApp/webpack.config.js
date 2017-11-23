'use strict';

const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

const extractCSS = new ExtractTextPlugin('vendor.css');
const OUTPUT_PATH = '/wwwroot/';

module.exports = {
  entry: {
    crmcore: __dirname + '/packages/index.js',
    vendors: [
      'jquery',
      'jquery-validation',
      'jquery-validation-unobtrusive',
      'bootstrap',
      'popper.js'
    ]
  },

  output: {
    path: __dirname + OUTPUT_PATH + 'server',
    filename: '[name].js',
    library: '[name]_[hash]'
  },

  module: {
    rules: [
      {
        test: /\.js$/,
        include: [path.resolve(__dirname, 'packages')],
        exclude: /(node_modules|wwwroot)/,
        use: ['babel-loader']
      },
      {
        test: /\.css$/,
        use: extractCSS.extract({
          fallback: 'style-loader',
          use: 'css-loader'
        })
      },
      {
        test: /\.(png|jpg|jpeg|gif|svg)$/,
        use: 'file-loader?name=images/[name].[ext]'
      },
      {
        test: /\.(woff|woff2|eot|ttf)(\?|$)/,
        use: 'file-loader?name=fonts/[name].[ext]'
      }
    ]
  },

  plugins: [
    extractCSS,
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery',
      Popper: 'popper.js'
    }),
    new CopyWebpackPlugin([
      {
        context: path.resolve(__dirname, 'node_modules/redoc/dist'),
        from: '**/*',
        to: path.resolve(__dirname, 'wwwroot/server/vendors/redoc')
      }
    ])
  ],

  stats: {
    colors: true
  },

  devtool: 'source-map'
};
