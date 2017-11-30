import { Component, OnInit } from '@angular/core';

import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html'
})
export class UnauthorizedComponent implements OnInit {
  constructor(private oidcSecurityService: OidcSecurityService) {}

  ngOnInit() {
  }

  login(): void {
    this.oidcSecurityService.authorize();
  }
}
