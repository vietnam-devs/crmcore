import { NgModule } from '@angular/core';

import { PostsRoutingModule } from './posts-routing.module';
import { SharedModule } from '../shared/shared.module';
import { PostService } from './services/post.service';

@NgModule({
  declarations: [ PostsRoutingModule.components ],
  imports: [ PostsRoutingModule, SharedModule ],
  providers: [ PostService ]
})
export class PostsModule {}
