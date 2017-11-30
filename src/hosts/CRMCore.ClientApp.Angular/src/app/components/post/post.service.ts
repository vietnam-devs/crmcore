import { HttpClient ,HttpErrorResponse  } from '@angular/common/http';
import { Injectable } from '@angular/core';

import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { Post } from '../../core/models/post.model';
import { ConfigService } from '../../core/services/config.service';


@Injectable()
export class PostService {
    posts: Post[] = [];     
    postUrl: string;

    constructor(private http: HttpClient, private configService: ConfigService) { 
     this.postUrl =   `${configService.api_url}/api/task1`;
    }

    getPosts(): Observable<Post[]> {
      return this.http.get<Post[]>(this.postUrl)
      .map(res  => { return res })
    };
   
   
    editPost(post:Post): Observable<Post>{
     let editUrl = `${this.postUrl}/${post.id}`    
     return this.http.put<Post>(editUrl, post);
    }

    deletePost(id:number):Observable<Post>{   
    let deleteUrl = `${this.postUrl}/${id}`
    return this.http.delete<Post>(deleteUrl)
    .map(res  => {
      return res;
    })    
   }

   createPost(post: Post): Observable<Post>{    
    return this.http.post<Post>(`${this.postUrl}`, post);
  }
}