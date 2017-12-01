import { CommonModule } from '@angular/common';
import { ModuleWithProviders, NgModule } from '@angular/core';

import { ConfigService } from './services/config.service';

@NgModule({
    imports: [
        CommonModule
    ]
})

export class CoreModule {
    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
              ConfigService
            ]
        };
    }
}
