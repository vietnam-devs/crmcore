import { Component ,Output , EventEmitter } from '@angular/core';

@Component({
    selector: 'search-post',
    templateUrl: './searchpost.component.html'    
})
export class SearchPostComponent {
    @Output() searchEvent = new EventEmitter();
    searchTerm: string;
    
    search(){
         this.searchEvent.emit(this.searchTerm);
    }
}
