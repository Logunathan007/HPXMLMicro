import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AddressComponent } from './address/address.component';
import { ZonesComponent } from './zones/zones.component';
import { SystemsComponent } from './systems/systems.component';

const routes: Routes = [
  {
    path:'',
    pathMatch: 'full',
    redirectTo:'address'
  },
  {
    path:'about',
    component:AboutComponent
  },
  {
    path:'address',
    component:AddressComponent
  },
  {
    path:'zones',
    component:ZonesComponent
  },
  {
    path:'systems',
    component:SystemsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ViewsRoutingModule { }
