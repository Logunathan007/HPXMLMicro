import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { ViewsRoutingModule } from './views-routing.module';
import { AddressComponent } from './address/address.component';
import { SystemsComponent } from './systems/systems.component';
import { ZonesComponent } from './zones/zones.component';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';
import { HttpClientModule, HttpClientXsrfModule } from '@angular/common/http';
import { NgxJsonViewerModule } from 'ngx-json-viewer';



@NgModule({
  declarations: [
    AboutComponent,
    AddressComponent,
    SystemsComponent,
    ZonesComponent,
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
