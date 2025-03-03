import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AddressComponent } from './address/address.component';
import { SystemsComponent } from './systems/systems.component';
import { ViewsComponent } from './views.component';

const routes: Routes = [

  {
    path:'',
    component:ViewsComponent,
    children:[
      {
        path:'',
        pathMatch:'full',
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
        loadChildren: ()=> import('./zones/zones.module').then(obj=>obj.ZonesModule)
      },
      {
        path:'systems',
        loadChildren: ()=> import('./systems/systems.module').then(obj=> obj.SystemsModule)
      }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ViewsRoutingModule { }
