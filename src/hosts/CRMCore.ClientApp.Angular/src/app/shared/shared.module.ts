import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule , ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { FilterPipe } from './pipe';

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
  APP_SIDEBAR_NAV
} from './components';

const SHARE_COMPONENTS = [
  AppAsideComponent,
  AppBreadcrumbsComponent,
  AppFooterComponent,
  AppHeaderComponent,
  AppSidebarComponent,
  AppSidebarFooterComponent,
  AppSidebarFormComponent,
  AppSidebarHeaderComponent,
  AppSidebarMinimizerComponent,
  APP_SIDEBAR_NAV
];

// Import directives
import {
  AsideToggleDirective,
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
  SIDEBAR_TOGGLE_DIRECTIVES
} from './directives';

const SHARE_DIRECTIVES = [
  AsideToggleDirective,
  NAV_DROPDOWN_DIRECTIVES,
  ReplaceDirective,
  SIDEBAR_TOGGLE_DIRECTIVES
];

const SHARE_PIPES = [
  FilterPipe,
];

@NgModule({
    declarations: [
      ...SHARE_COMPONENTS,
      ...SHARE_DIRECTIVES,
      SHARE_PIPES
    ],
    imports: [
      FormsModule,
      ReactiveFormsModule,
      CommonModule,
      HttpClientModule,
      RouterModule
    ],
    exports: [
      ReactiveFormsModule,
      CommonModule,
      FormsModule,
      HttpClientModule,
      SHARE_COMPONENTS,
      SHARE_DIRECTIVES,
      SHARE_PIPES
    ]
})

export class SharedModule { }
