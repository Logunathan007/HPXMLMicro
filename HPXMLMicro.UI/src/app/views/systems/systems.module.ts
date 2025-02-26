import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DistributedSystemComponent } from './distributed-system/distributed-system.component';
import { SystemsRoutingModule } from './systems-routing.module';
import { SystemsComponent } from './systems.component';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { NgSelectModule } from '@ng-select/ng-select';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    DistributedSystemComponent,
    SystemsComponent,
  ],
  imports: [
    CommonModule,
    SystemsRoutingModule,
    NgxJsonViewerModule,
    NgSelectModule,
    ReactiveFormsModule,

  ]
})
export class SystemsModule { }
