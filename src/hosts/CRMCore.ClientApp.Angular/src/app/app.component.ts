import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import {
  OidcSecurityService,
  AuthorizationResult
} from 'angular-auth-oidc-client';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-root',
  //templateUrl: './app.component.html',
  template: '<router-outlet></router-outlet>',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnDestroy {
  title = 'app';
  isAuthorizedSubscription: Subscription;
  hash: string;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    public router: Router
  ) {
    if (this.oidcSecurityService.moduleSetup) {
      this.onOidcModuleSetup();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.onOidcModuleSetup();
      });
    }

    this.router.events
      .filter((event: any) => event instanceof NavigationStart)
      .subscribe((data: NavigationStart) => {
        if (data.url == '/id_token') {
          this.hash = window.location.hash;
          this.router.navigate([]);
        }
      });

    this.oidcSecurityService.onAuthorizationResult.subscribe(
      (authorizationResult: AuthorizationResult) => {
        this.onAuthorizationResultComplete(authorizationResult);
      }
    );
  }

  ngOnDestroy(): void {
    this.oidcSecurityService.onModuleSetup.unsubscribe();
  }

  private onOidcModuleSetup() {
    const hash = this.getTokenHash();

    if (hash) {
      this.oidcSecurityService.authorizedCallback(hash);
    } else {
      if ('/autologin' !== window.location.pathname) {
        this.write('redirect', window.location.pathname);
      }
      console.log('AppComponent:onModuleSetup');
      this.oidcSecurityService
        .getIsAuthorized()
        .subscribe((authorized: boolean) => {
          if (!authorized) {
            this.router.navigate(['/autologin']);
          }
        });
    }
  }

  private getTokenHash() {
    if (typeof location !== 'undefined' && this.hash) {
      const indexHash = this.hash.indexOf('id_token');
      return indexHash > -1 && this.hash.substr(indexHash);
    }
  }

  private onAuthorizationResultComplete(authorizationResult: AuthorizationResult) {
    const path = this.read('redirect');
    if (authorizationResult === AuthorizationResult.authorized) {
      this.router.navigate([path]);
    } 
  }

  private read(key: string): any {
    const data = localStorage.getItem(key);
    if (data != null) {
      return JSON.parse(data);
    }
    return;
  }

  private write(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
}
