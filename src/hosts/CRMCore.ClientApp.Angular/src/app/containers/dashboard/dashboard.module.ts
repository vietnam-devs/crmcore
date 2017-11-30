import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { DashboardComponent } from './dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { PostsComponent } from '../../components/post/posts.component';
import { AddPostComponent } from '../../components/post/addpost.component';
import { SearchPostComponent } from '../../components/post/searchpost.component';

import { PostService } from '../../components/post/post.service';
import { FilterPipe} from '../../pipe/filter.pipe'

@NgModule({
    imports: [ DashboardRoutingModule,SharedModule],
    providers: [PostService],
    declarations: [ DashboardComponent,PostsComponent,
                    AddPostComponent,SearchPostComponent ,
                    FilterPipe
                  ]
  })
export class DashboardModule { }