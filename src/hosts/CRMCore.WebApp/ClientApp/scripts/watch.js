// Reference to https://gist.github.com/int128/e0cdec598c5b3db728ff35758abdbafd
process.env.NODE_ENV = 'development';

const fs = require('fs-extra');
const paths = require('react-scripts/config/paths');
const webpack = require('webpack');
const config = require('react-scripts/config/webpack.config.dev.js');

// removes react-dev-utils/webpackHotDevClient.js at first in the array
config.entry = config.entry.filter(
  entry => !entry.includes('webpackHotDevClient')
);

config.output.path = paths.appBuild;
paths.publicUrl = paths.appBuild + '/';

webpack(config).watch({}, (err, stats) => {
  if (err) {
    console.error(err);
  } else {
    copyPublicFolder();
  }
  console.error(stats.toString({
    chunks: false,
    colors: true
  }));
});

function copyPublicFolder() {
  fs.copySync(paths.appPublic, paths.appBuild, {
    dereference: true,
    filter: file => file !== paths.appHtml
  });
}