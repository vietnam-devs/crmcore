import { Observable } from 'rxjs';
import { globalConfig as GlobalConfig } from 'configs';

const defaultHeaders = {
  //Authorization: "<put here in future>",
  'Content-Type': 'application/json'
};

const fullUri = `${GlobalConfig.apiServer}`;

export const get = (url, headers) =>
  Observable.ajax.get(`${fullUri}/${url}`, Object.assign({}, defaultHeaders, headers));

export const post = (url, body, headers) =>
  Observable.ajax.post(`${fullUri}/${url}`, body, Object.assign({}, defaultHeaders, headers));

export const put = (url, body, headers) =>
  Observable.ajax.put(`${fullUri}/${url}`, body, Object.assign({}, defaultHeaders, headers));

export const del = (url, headers) =>
  Observable.ajax.delete(`${fullUri}/${url}`, Object.assign({}, defaultHeaders, headers));
