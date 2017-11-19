import 'babel-polyfill';

import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { ConnectedRouter } from 'react-router-redux';
import { HashRouter, Route, Switch } from 'react-router-dom';

import createStore, { routerHistory } from 'redux/configureStore';
// import registerServiceWorker from './registerServiceWorker';

// Containers
import Full from './containers/Layout/Full';

// Styles
import 'font-awesome/css/font-awesome.min.css';
import 'simple-line-icons/css/simple-line-icons.css';
import './styles/style.css';

const store = createStore();

ReactDOM.render(
  <Provider store={store} key="provider">
    <ConnectedRouter history={routerHistory}>
      <HashRouter>
        <Switch>
          <Route path="/" name="Home" component={Full} />
        </Switch>
      </HashRouter>
    </ConnectedRouter>
  </Provider>,
  document.getElementById('root')
);

// registerServiceWorker();
