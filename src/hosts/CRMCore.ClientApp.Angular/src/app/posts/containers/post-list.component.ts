import {Component, Input, OnInit} from '@angular/core';

import { PostService } from '../services/post.service';
import { Post } from '../models/post.model';

@Component({
    selector: 'app-post-list',
    templateUrl: './post-list.component.html'
})

export class PostListComponent implements OnInit {
  posts: Post[] = [];
  searchTerm: string;

  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.postService.getPosts()
      .subscribe(result => { this.posts = result; });
  }

  postCreatedListen(newPost: Post) {
    this.posts.push(newPost);
  }

  listenSearchEvent(searchTerm: string) {
    this.searchTerm = searchTerm;
  }
}
