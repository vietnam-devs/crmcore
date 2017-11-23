const windowIfDefined = typeof window === 'undefined' ? null : window;
const url =
  windowIfDefined &&
  `${window.location.protocol}//${window.location.hostname}:5000`;

let authorityUri = url;
let apiServerUri = url;

if (windowIfDefined.serverVariables) {
  authorityUri = windowIfDefined.serverVariables.AuthorityServerUri;
  apiServerUri = windowIfDefined.serverVariables.ApiServerUri;
}

export const globalConfig = {
  authorityServer: authorityUri,
  apiServer: apiServerUri
};
