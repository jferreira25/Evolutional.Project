import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/core/material.module';
import { ComponentsModule } from 'src/app/common/components/components.module';
import { DirectivesModule } from 'src/app/common/directives/directives.module';
import { LessonsFormComponent } from './form/lessons-form.component';
import { LessonsListComponent } from './list/lessons-list.component';
import { LessonsRoutingModule } from './lessons-routing.module';

@NgModule({
  declarations: [
    LessonsListComponent,
    LessonsFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    LessonsRoutingModule,
    MaterialModule,
    ComponentsModule,
    DirectivesModule
  ]
})
export class LessonsModule { }
