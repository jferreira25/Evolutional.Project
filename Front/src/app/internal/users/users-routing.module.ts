import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RoleGuard } from 'src/app/common/auth-guards/role.guard';
import { RoleEnum } from 'src/app/common/enuns/role.enum';
import { UsersFormComponent } from './form/users-form.component';
import { UsersListComponent } from './list/users-list.component';

const routes: Routes = [
    {
      path: '',
      component: UsersListComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    },
    {
      path: 'add',
      component: UsersFormComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    },
    {
      path: 'add/:id',
      component: UsersFormComponent,
      canActivate: [RoleGuard],
      data: { roles: [RoleEnum.ADMIN, RoleEnum.APPROVER, RoleEnum.USER] }
    }
  ];
  
  @NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })
  export class UsersRoutingModule { }
  