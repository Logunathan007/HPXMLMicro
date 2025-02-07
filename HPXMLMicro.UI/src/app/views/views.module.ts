import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { ViewsRoutingModule } from './views-routing.module';
import { AddressComponent } from './address/address.component';
import { SystemsComponent } from './systems/systems.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {  NgSelectModule } from '@ng-select/ng-select';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { HttpClientModule } from '@angular/common/http';
import { ViewsComponent } from './views.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    AboutComponent,
    AddressComponent,
    SystemsComponent,
    ViewsComponent
  ],
  imports: [
    CommonModule,
    ViewsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    HttpClientModule,
    NgxJsonViewerModule
  ],
})
export class ViewsModule { }
