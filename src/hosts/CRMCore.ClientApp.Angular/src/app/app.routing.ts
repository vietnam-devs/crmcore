import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnauthorizedComponent, AutoLoginComponent } from './containers';

export const routes: Routes = [
  { path: 'unauthorized', component: UnauthorizedComponent },
  { path: 'autologin', component: AutoLoginComponent },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: 'dashboard',
    loadChildren: './dashboard/dashboard.module#DashboardModule'
  },
  {
    path: 'posts',
    loadChildren: './posts/posts.module#PostsModule'
  }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {
  static components = [ UnauthorizedComponent, AutoLoginComponent, ];
}
