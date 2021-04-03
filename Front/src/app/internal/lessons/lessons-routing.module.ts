import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RoleGuard } from 'src/app/common/auth-guards/role.guard';
import { RoleEnum } from 'src/app/common/enuns/role.enum';
import { LessonsFormComponent } from './form/lessons-form.component';
import { LessonsListComponent } from './list/lessons-list.component';

const routes: Routes = [
    {
      path: '',
      component: LessonsListComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    },
    {
      path: 'add',
      component: LessonsFormComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    },
    {
      path: 'add/:id',
      component: LessonsFormComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class LessonsRoutingModule { }
  