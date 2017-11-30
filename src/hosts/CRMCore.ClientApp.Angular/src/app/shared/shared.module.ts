
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule ,ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { ConfigService } from '../core/services/config.service';


@NgModule({
    imports: [FormsModule,ReactiveFormsModule, CommonModule,HttpClientModule],
    providers: [ConfigService],     
    exports: [ReactiveFormsModule, CommonModule, FormsModule,HttpClientModule]
})

export class SharedModule { }
