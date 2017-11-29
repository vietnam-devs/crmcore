import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnauthorizedComponent, AutoLoginComponent } from './components';

// Import Containers
import {
  LayoutComponent,  
} from './containers';

export const routes: Routes = [
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: 'autologin', component: AutoLoginComponent },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: LayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'dashboard',
        loadChildren: './views/dashboard/dashboard.module#DashboardModule'
      }
    ]
  }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
