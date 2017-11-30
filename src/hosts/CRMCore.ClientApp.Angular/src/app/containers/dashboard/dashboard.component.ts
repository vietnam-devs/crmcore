import { Component, OnInit  } from '@angular/core';
import { Router } from '@angular/router';

import { PostService } from '../../components/post/post.service';
import { Post } from '../../core/models/post.model';

@Component({
  templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit { 
    posts :Post[] =[];
    searchTerm: string;
    constructor(private postService: PostService) {}

  ngOnInit(): void {   
    this.postService.getPosts()
      .subscribe(result => { this.posts = result       
      })
  }

  postCreatedListen(newPost: Post){
     this.posts.push(newPost);
  }

  listenSearchEvent(searchTerm: string){
     this.searchTerm = searchTerm;
  }
}
