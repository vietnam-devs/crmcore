import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { HttpClientModule,HttpClient } from '@angular/common/http';

import { AuthModule,OidcSecurityService,OpenIDImplicitFlowConfiguration} from 'angular-auth-oidc-client';

import { AuthInterceptorProvider } from './interceptors/auth.interceptor';
import { ErrorInterceptorProvider } from './interceptors/error.interceptor';

import { AppComponent } from './app.component';
// Import routing module
import { AppRoutingModule } from './app.routing';

// Import containers
import { LayoutComponent } from './containers';
const APP_CONTAINERS = [LayoutComponent];

// Import components
import {
  AppAsideComponent,
  AppBreadcrumbsComponent,
  AppFooterComponent,
  AppHeaderComponent,
  AppSidebarComponent,
  AppSidebarFooterComponent,
  AppSidebarFormComponent,
  AppSidebarHeaderComponent,
  AppSidebarMinimizerComponent,
  APP_SIDEBAR_NAV,
  AutoLoginComponent,
  UnauthorizedComponent
} from './components';

const APP_COMPONENTS = [
  AppAsideComponent,
  AppBreadcrumbsComponent,
  AppFooterComponent,
  AppHeaderComponent,
  AppSidebarComponent,
  AppSidebarFooterComponent,
  AppSidebarFormComponent,
  AppSidebarHeaderComponent,
  AppSidebarMinimizerComponent,
  APP_SIDEBAR_NAV,
  AutoLoginComponent,
  UnauthorizedComponent
];

// Import directives
import {
  AsideToggleDirective,
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
  SIDEBAR_TOGGLE_DIRECTIVES
} from './shared/directives';

const APP_DIRECTIVES = [
  AsideToggleDirective,
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
  SIDEBAR_TOGGLE_DIRECTIVES
];



@NgModule({
  declarations: [
    AppComponent,
    ...APP_CONTAINERS,
    ...APP_COMPONENTS,
    ...APP_DIRECTIVES
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,    
    HttpClientModule,
    AuthModule.forRoot()
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    },
     AuthInterceptorProvider,
     ErrorInterceptorProvider,        
     OidcSecurityService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  clientConfiguration: any;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private http: HttpClient
  ) {
    this.configClient().subscribe((config: any) => {
      this.clientConfiguration = config;

      let openIDImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();

      openIDImplicitFlowConfiguration.stsServer = this.clientConfiguration.stsServer;
      openIDImplicitFlowConfiguration.redirect_url = this.clientConfiguration.redirect_url;
      // The Client MUST validate that the aud (audience) Claim contains its client_id value registered at the Issuer identified by the iss (issuer) Claim as an audience.
      // The ID Token MUST be rejected if the ID Token does not list the Client as a valid audience, or if it contains additional audiences not trusted by the Client.
      openIDImplicitFlowConfiguration.client_id = this.clientConfiguration.client_id;
      openIDImplicitFlowConfiguration.response_type = this.clientConfiguration.response_type;
      openIDImplicitFlowConfiguration.scope = this.clientConfiguration.scope;
      openIDImplicitFlowConfiguration.post_logout_redirect_uri = this.clientConfiguration.post_logout_redirect_uri;
      openIDImplicitFlowConfiguration.start_checksession = this.clientConfiguration.start_checksession;
      openIDImplicitFlowConfiguration.silent_renew = this.clientConfiguration.silent_renew;
      openIDImplicitFlowConfiguration.post_login_route = this.clientConfiguration.startup_route;
      // HTTP 403
      openIDImplicitFlowConfiguration.forbidden_route = this.clientConfiguration.forbidden_route;
      // HTTP 401
      openIDImplicitFlowConfiguration.unauthorized_route = this.clientConfiguration.unauthorized_route;
      openIDImplicitFlowConfiguration.log_console_warning_active = this.clientConfiguration.log_console_warning_active;
      openIDImplicitFlowConfiguration.log_console_debug_active = this.clientConfiguration.log_console_debug_active;
      // id_token C8: The iat Claim can be used to reject tokens that were issued too far away from the current time,
      // limiting the amount of time that nonces need to be stored to prevent attacks.The acceptable range is Client specific.
      openIDImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds = this.clientConfiguration.max_id_token_iat_offset_allowed_in_seconds;

      this.oidcSecurityService.setupModule(openIDImplicitFlowConfiguration);
    });
  }

  configClient() {
    console.log('window.location', window.location);
    console.log('window.location.href', window.location.href);
    console.log('window.location.origin', window.location.origin);
    console.log(`${window.location.origin}/api/ClientAppSettings`);

    return this.http.get(`${window.location.origin}/api/ClientAppSettings`);
  }
}
