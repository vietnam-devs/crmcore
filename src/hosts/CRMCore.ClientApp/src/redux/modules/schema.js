import { Observable } from 'rxjs/Observable';

import { get, post, put, del } from './../client';
import { actionCreators as commonActionCreators } from './common';
import { globalConfig as GlobalConfig } from './../../configs';

const SCHEMAS_URL = `${GlobalConfig.apiServer}/schema/api`;

// Load schemas
const LOAD_SCHEMAS = 'LOAD_SCHEMAS';
const LOAD_SCHEMAS_SUCCESSED = 'LOAD_SCHEMAS_SUCCESSED';
const LOAD_SCHEMAS_FAILED = 'LOAD_SCHEMAS_FAILED';

// Load specific schema
const LOAD_SPECIFIC_SCHEMA = 'LOAD_SPECIFIC_SCHEMA';
const LOAD_SPECIFIC_SCHEMA_SUCCESSED = 'LOAD_SPECIFIC_SCHEMA_SUCCESSED';
const LOAD_SPECIFIC_SCHEMA_FAILED = 'LOAD_SPECIFIC_SCHEMA_FAILED';

const initialState = {
  loading: true,
  loaded: false,
  schemaByIds: [],
  schemas: [],
  schemaSelected: null,
  error: null,
  page: 0
};

const schemaRequest = {
  loadSchemas: page => get(`${SCHEMAS_URL}/orgs/org1/schemas?page=${page + 1}`),
  loadSpecificSchema: name => get(`${SCHEMAS_URL}/orgs/org1/schemas/${name}`)
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
  },
  action$ => {
    return action$
      .ofType('LOAD_SPECIFIC_SCHEMA')
      .debounceTime(100)
      .switchMap(action =>
        schemaRequest
          .loadSpecificSchema(action.name)
          .map(result => {
            return actionCreators.loadSpecificSchemaSucceed(result.response);
          })
          .catch(error =>
            Observable.of(actionCreators.loadSpecificSchemaFailed(error))
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
  },
  loadSpecificSchema: name => {
    return {
      type: LOAD_SPECIFIC_SCHEMA,
      name
    };
  },
  loadSpecificSchemaSucceed: schemaDetailsItem => {
    return {
      type: LOAD_SPECIFIC_SCHEMA_SUCCESSED,
      schemaDetailsItem
    };
  },
  loadSpecificSchemaFailed: error => {
    return {
      type: LOAD_SPECIFIC_SCHEMA_FAILED,
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
        schemaByIds: action.data.map(schema => schema.id),
        schemas: action.data.reduce((obj, schema) => {
          obj[schema.id] = schema;
          return obj;
        }, {}),
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

    case LOAD_SPECIFIC_SCHEMA:
      return {
        ...state,
        schemaSelected: null,
        loaded: false,
        loading: true
      };

    case LOAD_SPECIFIC_SCHEMA_SUCCESSED:
      console.log(action);
      return {
        ...state,
        schemaSelected: action.schemaDetailsItem,
        loaded: true,
        loading: false
      };

    case LOAD_SPECIFIC_SCHEMA_FAILED:
      console.log(action);
      return {
        ...state,
        error: action.error,
        schemaSelected: null,
        loaded: true,
        loading: false
      };

    default:
      return state;
  }
}
