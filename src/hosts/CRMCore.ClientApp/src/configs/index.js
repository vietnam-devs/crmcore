import { createUserManager } from 'redux-oidc';

const windowIfDefined = typeof window === 'undefined' ? null : window;
const serverUri =
  windowIfDefined &&
  `${window.location.protocol}//${window.location.hostname}:5000`;

let clientUri = windowIfDefined &&
`${window.location.protocol}//${window.location.hostname}:3000`;

let authorityUri = serverUri;
let apiServerUri = serverUri;

if (windowIfDefined.serverVariables) {
  authorityUri = windowIfDefined.serverVariables.AuthorityServerUri;
  apiServerUri = windowIfDefined.serverVariables.ApiServerUri;
  clientUri = windowIfDefined.serverVariables.ClientUri;
}

const userManagerConfig = {
  client_id: '1a0e947d-99d7-4c6d-a2f3-a196a7da64a4',
  redirect_uri: `${clientUri}/callback`,
  response_type: 'token id_token',
  scope: 'openid profile Notifications Contacts',
  authority: authorityUri,
  silent_redirect_uri: `${clientUri}/silent_renew.html`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true
};

export const userManager = createUserManager(userManagerConfig);

export const globalConfig = {
  authorityServer: authorityUri,
  apiServer: apiServerUri,
  clientUri: clientUri
};
