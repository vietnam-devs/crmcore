import { combineEpics } from 'redux-observable';
import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { reducer as formReducer } from 'redux-form';

import schemaReducer, { schemaEpics } from './schema';
import commonReducer from './common';

const reducers = {
  routing: routerReducer,
  form: formReducer,
  schema: schemaReducer,
  common: commonReducer
};

export const rootEpic = combineEpics(...schemaEpics);

export default combineReducers(reducers);
