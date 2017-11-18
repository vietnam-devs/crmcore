import 'babel-polyfill';

import React from 'react';
import ReactDOM from 'react-dom';
import { HashRouter, Route, Switch } from 'react-router-dom';

// Containers
import Full from './containers/Layout/Full';

// Styles
// Import Font Awesome Icons Set
import 'font-awesome/css/font-awesome.min.css';
// Import Simple Line Icons Set
import 'simple-line-icons/css/simple-line-icons.css';
// Import Main styles for this application
import './scss/style.css';
// Temp fix for reactstrap
// import './scss/core/_dropdown-menu-right.scss';

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
