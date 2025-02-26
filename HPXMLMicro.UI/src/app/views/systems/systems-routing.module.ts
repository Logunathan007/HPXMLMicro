import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SystemsComponent } from './systems.component';
import { DistributedSystemComponent } from './distributed-system/distributed-system.component';
import { HvacComponent } from './hvac/hvac.component';
import { PvSystemComponent } from './pv-system/pv-system.component';

const routes: Routes = [
  {
    path: '',
    component:SystemsComponent,
    children:[
      {
        path: '',
        pathMatch:'full',
        redirectTo:"distribution-system"
      },
      {
        path:'distribution-system',
        component:DistributedSystemComponent
      },
      {
        path:'hvac',
        component:HvacComponent
      },
      {
        path:'pv-system',
        component:PvSystemComponent
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemsRoutingModule { }
