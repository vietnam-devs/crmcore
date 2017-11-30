import {Directive, HostListener} from '@angular/core';

@Directive({
    selector: '[appAsideMenuToggler]',
})

export class AsideToggleDirective{
    constructor(){}

    @HostListener('click', ['$event'])

    toggleOpen($event: any){
        $event.preventDefault();
        document.querySelector('body').classList.toggle('aside-menu-hidden');
    }
}

