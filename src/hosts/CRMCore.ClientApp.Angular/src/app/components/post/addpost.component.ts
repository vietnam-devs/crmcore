import { Component ,Input, Output,OnInit, EventEmitter } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { PostService } from '../../components/post/post.service';
import { Post } from '../../core/models/post.model';

@Component({
    selector: 'add-post',
    templateUrl: './addpost.component.html'    
})
export class AddPostComponent  implements OnInit{
  @Input() toggleAddPost;
  @Output() postCreatedEvent = new EventEmitter();
  
  post: Post;
  postForm: FormGroup;
  
  constructor(private postService: PostService) {
    this.post = new Post();
  }

  ngOnInit() {
        this.postForm = new FormGroup({
            title: new FormControl(this.post.title, Validators.required),
            description: new FormControl(this.post.description, Validators.required)
        });       
    }

  createPost() {
    this.postService.createPost(this.post)
      .subscribe((res) => {       
        this.postCreatedEvent.emit(res);
        this.reset();
      })
  }

  reset() {
   this.postForm.reset();
  }
}
