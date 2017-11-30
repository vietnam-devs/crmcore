import {Component, Input, OnInit} from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-posts',
    templateUrl: './posts.component.html'
})

export class PostsComponent implements OnInit {

  constructor(private router: Router) {}

  ngOnInit(): void {
  }
}

