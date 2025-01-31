import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { ViewsComponent } from './views.component';
import { ViewsRoutingModule } from './views-routing.module';
import { AddressComponent } from './address/address.component';
import { SystemsComponent } from './systems/systems.component';
import { ZonesComponent } from './zones/zones.component';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { NgSelectComponent, NgSelectModule } from '@ng-select/ng-select';



@NgModule({
  declarations: [
    AboutComponent,
    ViewsComponent,
    AddressComponent,
    SystemsComponent,
    ZonesComponent,
  ],
  imports: [
    CommonModule,
    ViewsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule
  ],
})
export class ViewsModule { }
