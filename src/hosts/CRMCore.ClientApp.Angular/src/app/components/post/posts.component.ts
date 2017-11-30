import {Component, Input} from '@angular/core';

import { PostService } from '../../components/post/post.service';
import { Post } from '../../model/post.model';

@Component({
    selector: 'posts',
    templateUrl:'./posts.component.html'
})

export class PostsComponent{
    @Input() posts;
    @Input() searchTerm :string;
    toggleAddPost:boolean = false;
    editPosts: Post[] = [];
      
    constructor(private postService: PostService) {}

    handlePost() {
        this.toggleAddPost = !this.toggleAddPost;       
    }
   
   editPost(post: Post) {       
      if(this.posts.includes(post)){
        if(!this.editPosts.includes(post)){
          this.editPosts.push(post)
        }
        else{
          this.editPosts.splice(this.editPosts.indexOf(post), 1) 
          this.postService.editPost(post).subscribe(res => {
            //TODO
            console.log('Update Succesful')
          }, err => {         
            console.error('Update Unsuccesful')
          })       
        }
      }
  }

  deletePost(post:Post){
    this.postService.deletePost(post.id).subscribe(res => {
      this.posts.splice(this.posts.indexOf(post), 1);
    })
  }

  postCreatedListen(post: Post){
     this.posts.push(post);
  }
  
}