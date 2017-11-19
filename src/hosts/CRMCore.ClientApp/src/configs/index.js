const windowIfDefined = typeof window === 'undefined' ? null : window;
const url =
  windowIfDefined &&
  `${window.location.protocol}//${window.location.hostname}:5000`;

export const globalConfig = {
  authorityServer: url,
  apiServer: url
};
