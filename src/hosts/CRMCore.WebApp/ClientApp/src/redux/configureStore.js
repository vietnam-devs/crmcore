import { createStore as _createStore, applyMiddleware } from 'redux';
import { routerMiddleware } from 'react-router-redux';
import { createEpicMiddleware } from 'redux-observable';
import { CreateJumpstateMiddleware } from 'jumpstate';
import createHistory from 'history/createBrowserHistory';

import reducers, { rootEpic } from './modules/reducers';

export const routerHistory = createHistory();

// Combine all epics and instantiate the app-wide store instance
const epicMiddleware = createEpicMiddleware(rootEpic);

export default function createStore() {
  const middlewares = [
    routerMiddleware(routerHistory),
    CreateJumpstateMiddleware(),
    epicMiddleware
  ];

  const store = _createStore(
    reducers,
    window.__REDUX_DEVTOOLS_EXTENSION__ &&
      window.__REDUX_DEVTOOLS_EXTENSION__(),
    applyMiddleware(...middlewares)
  );

  return store;
}
