import { Component, OnInit, OnDestroy } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
    selector: 'app-auto-component',
    template: '.<div>redirecting to login</div>'
})

export class AutoLoginComponent implements OnInit, OnDestroy {
    constructor(public oidcSecurityService: OidcSecurityService
    ) {
        this.oidcSecurityService.onModuleSetup.subscribe(() => { this.onModuleSetup(); });
    }

    ngOnInit() {
        if (this.oidcSecurityService.moduleSetup) {
            this.onModuleSetup();
        }
    }

    ngOnDestroy(): void {
        this.oidcSecurityService.onModuleSetup.unsubscribe();
    }

    private onModuleSetup() {
        this.oidcSecurityService.authorize();
    }
}