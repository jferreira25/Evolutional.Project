import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RoleGuard } from 'src/app/common/auth-guards/role.guard';
import { RoleEnum } from 'src/app/common/enuns/role.enum';
import { StudentsFormComponent } from './form/students-form.component';
import { StudentsListComponent } from './list/students-list.component';

const routes: Routes = [
    {
      path: '',
      component: StudentsListComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    },
    {
      path: 'add',
      component: StudentsFormComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    },
    {
      path: 'add/:id',
      component: StudentsFormComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class StudentsRoutingModule { }
  