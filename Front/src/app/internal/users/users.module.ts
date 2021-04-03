import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/core/material.module';
import { ComponentsModule } from 'src/app/common/components/components.module';
import { DirectivesModule } from 'src/app/common/directives/directives.module';
import { UsersFormComponent } from './form/users-form.component';
import { UsersListComponent } from './list/users-list.component';
import { UsersRoutingModule } from './users-routing.module';

@NgModule({
  declarations: [
    UsersListComponent,
    UsersFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UsersRoutingModule,
    MaterialModule,
    ComponentsModule,
    DirectivesModule
  ]
})
export class UsersModule { }
