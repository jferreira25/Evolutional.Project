import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/core/material.module';
import { ComponentsModule } from 'src/app/common/components/components.module';
import { DirectivesModule } from 'src/app/common/directives/directives.module';
import { StudentsFormComponent } from './form/students-form.component';
import { StudentsListComponent } from './list/students-list.component';
import { StudentsRoutingModule } from './students-routing.module';

@NgModule({
  declarations: [
    StudentsListComponent,
    StudentsFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    StudentsRoutingModule,
    MaterialModule,
    ComponentsModule,
    DirectivesModule
  ]
})
export class StudentsModule { }
