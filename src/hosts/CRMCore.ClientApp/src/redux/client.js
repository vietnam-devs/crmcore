import { Observable } from 'rxjs';

const defaultHeaders = {
  //Authorization: "<put here in future>",
  'Content-Type': 'application/json'
};

export const get = (url, headers) =>
  Observable.ajax.get(url, Object.assign({}, defaultHeaders, headers));

export const post = (url, body, headers) =>
  Observable.ajax.post(url, body, Object.assign({}, defaultHeaders, headers));

export const put = (url, body, headers) =>
  Observable.ajax.put(url, body, Object.assign({}, defaultHeaders, headers));

export const del = (url, headers) =>
  Observable.ajax.delete(url, Object.assign({}, defaultHeaders, headers));
