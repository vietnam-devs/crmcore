import { combineEpics } from 'redux-observable';
import { combineReducers } from 'redux';
import { routerReducer } from 'react-router-redux';
import { reducer as formReducer } from 'redux-form';

import schemaReducer, { schemaEpics } from './schema';
import taskReducer, { taskEpics } from './task';
import commonReducer from './common';

const reducers = {
  routing: routerReducer,
  form: formReducer,
  schema: schemaReducer,
  task: taskReducer,
  common: commonReducer
};

export const rootEpic = combineEpics(...schemaEpics, ...taskEpics);

export default combineReducers(reducers);
