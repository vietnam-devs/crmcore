import { Observable } from 'rxjs/Observable';

import { get, post, put, del } from './../client';
import { actionCreators as commonActionCreators } from './common';
import { globalConfig as GlobalConfig } from './../../configs';

const SCHEMAS_URL = `${GlobalConfig.apiServer}/schema/api/schemas`;

// Load schemas
const LOAD_SCHEMAS = 'LOAD_SCHEMAS';
const LOAD_SCHEMAS_SUCCESSED = 'LOAD_SCHEMAS_SUCCESSED';
const LOAD_SCHEMAS_FAILED = 'LOAD_SCHEMAS_FAILED';

const initialState = {
  loading: true,
  loaded: false,
  schemas: [],
  schemaSelected: null,
  error: null,
  page: 0
};

const schemaRequest = {
  loadSchemas: page => get(`${SCHEMAS_URL}?page=${page + 1}`)
};

export const schemaEpics = [
  action$ => {
    return action$
      .ofType('LOAD_SCHEMAS')
      .debounceTime(100)
      .switchMap(action =>
        schemaRequest
          .loadSchemas(action.page)
          .map(result => {
            return actionCreators.loadSchemasSuccessed(result.response);
          })
          .catch(error =>
            Observable.of(actionCreators.loadSchemasFailed(error))
          )
      );
  }
];

export const actionCreators = {
  loadSchemas: page => {
    return {
      type: LOAD_SCHEMAS,
      page
    };
  },
  loadSchemasSuccessed: data => {
    return {
      type: LOAD_SCHEMAS_SUCCESSED,
      data
    };
  },
  loadSchemasFailed: error => {
    return {
      type: LOAD_SCHEMAS_FAILED,
      error
    };
  }
};

export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case LOAD_SCHEMAS:
      return {
        ...state,
        loading: true
      };

    case LOAD_SCHEMAS_SUCCESSED:
      console.log(action);
      return {
        ...state,
        loaded: true,
        loading: false,
        schemas: action.data,
        page: action.page || 0
      };

    case LOAD_SCHEMAS_FAILED:
      return {
        ...state,
        error: action.error,
        loaded: true,
        loading: false,
        schemas: [],
        page: 0
      };

    default:
      return state;
  }
}
