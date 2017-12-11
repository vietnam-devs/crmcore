import 'babel-polyfill';

import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { OidcProvider, loadUser } from 'redux-oidc';
import { ConnectedRouter } from 'react-router-redux';
import { HashRouter, Route, Switch } from 'react-router-dom';

import { userManager } from 'configs';
import createStore, { routerHistory } from 'redux/configureStore';
// import registerServiceWorker from './registerServiceWorker';

// Containers
import { Login, Callback } from 'containers';
import FullLayout from './containers/Layout/FullLayout';
import CrmLayout from './containers/Layout/CrmLayout';
import MetadataLayout from './containers/Layout/MetadataLayout';

// Styles
import 'font-awesome/css/font-awesome.min.css';
import 'simple-line-icons/css/simple-line-icons.css';
import 'react-table/react-table.css';
import './styles/style.css';

const store = createStore();

// load the current user into the redux store
loadUser(store, userManager);

ReactDOM.render(
  <Provider store={store} key="provider">
    <OidcProvider store={store} userManager={userManager} key="oidcProvider">
      <ConnectedRouter history={routerHistory}>
        <div>
          <Switch>
            <Route exact path={`/login`} key="login" component={Login} />
            <Route
              exact
              path={`/callback`}
              key="callback"
              component={Callback}
            />
            <Route
              path="/metadata"
              name="Metadata"
              component={MetadataLayout}
            />
            <Route path="/crm" name="Crm" component={CrmLayout} />
            <Route path="/" name="Home" component={FullLayout} />
          </Switch>
        </div>
      </ConnectedRouter>
    </OidcProvider>
  </Provider>,
  document.getElementById('root')
);

// registerServiceWorker();
