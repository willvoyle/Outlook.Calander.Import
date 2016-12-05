import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { CalendarComponent } from "./calendar.component";

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule
    ],
    declarations: [
        CalendarComponent
    ],
    bootstrap: [
        CalendarComponent
    ]
})

export class AppModule {

}