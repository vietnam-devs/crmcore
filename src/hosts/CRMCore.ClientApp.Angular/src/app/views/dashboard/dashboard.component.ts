import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
@Component({
  templateUrl: 'dashboard.component.html'
})
export class DashboardComponent {

  constructor( private http: HttpClient) {
    
   }

}
