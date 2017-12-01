import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';

import {
  AuthModule,
  OidcSecurityService,
  OpenIDImplicitFlowConfiguration
} from 'angular-auth-oidc-client';

import { AppComponent } from './app.component';

// Import routing module
import { AppRoutingModule } from './app.routing';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';

import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { AuthInterceptor } from './core/auth/Auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AppRoutingModule.components
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    SharedModule,
    CoreModule.forRoot(),
    AuthModule.forRoot(),
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({ maxAge: 25 })
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
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

      const openIDImplicitFlowConfiguration = new OpenIDImplicitFlowConfiguration();

      openIDImplicitFlowConfiguration.stsServer = this.clientConfiguration.stsServer;
      openIDImplicitFlowConfiguration.redirect_url = this.clientConfiguration.redirect_url;
      // The Client MUST validate that the aud (audience)
      // Claim contains its client_id value registered at the Issuer
      // identified by the iss (issuer) Claim as an audience.
      // The ID Token MUST be rejected if the ID Token does not list
      // the Client as a valid audience, or if it contains additional
      // audiences not trusted by the Client.
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
      openIDImplicitFlowConfiguration.max_id_token_iat_offset_allowed_in_seconds =
      this.clientConfiguration.max_id_token_iat_offset_allowed_in_seconds;

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
