import 'babel-polyfill';

import React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Route, Switch } from 'react-router-dom';

// Containers
import Full from './containers/Layout/Full';

// Styles
import 'font-awesome/css/font-awesome.min.css';
import 'simple-line-icons/css/simple-line-icons.css';
import './styles/style.css';

// import registerServiceWorker from './registerServiceWorker';

ReactDOM.render(
  <HashRouter>
    <Switch>
      <Route path="/" name="Home" component={Full} />
    </Switch>
  </HashRouter>,
  document.getElementById('root')
);

// registerServiceWorker();
