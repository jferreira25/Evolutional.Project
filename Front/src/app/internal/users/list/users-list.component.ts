import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { MatTableDataSource, MatBottomSheet, PageEvent } from '@angular/material';
import { Paginator } from 'src/app/common/models/paginator';
import { Router } from '@angular/router';
import { RouteStackService } from 'src/app/common/services/route-stack.service';
import { CustomValidator } from 'src/app/common/utils/custom-validator';
import { ActionBottomSheetComponent } from 'src/app/common/components/action-bottom-sheet/action-bottom-sheet.component';
import { ActionBottomSheet } from 'src/app/common/models/action-bottom-sheet';
import { UtilService } from 'src/app/common/utils/util.service';
import { User } from 'src/app/common/models/user';
import { UsersService } from 'src/app/common/services/users.service';

@Component({
    selector: 'app-users-list',
    templateUrl: './users-list.component.html',
    styleUrls: ['./users-list.component.css']
  })
  export class UsersListComponent implements OnInit {
  
    public form: FormGroup;
    public dsUsers: MatTableDataSource<User[]>;
    public totalRow: number;

    constructor(
      private builder: FormBuilder,
      private router: Router,
      private routeStack: RouteStackService,
      private bottomSheet: MatBottomSheet,
      private util: UtilService,
      private usersService: UsersService
      
    ) {
      this.form = this.createFormGroup();
      this.dsUsers = new MatTableDataSource();
    }
  
    ngOnInit() {
      this.filter();
      this.dsUsers = new MatTableDataSource();
    }
  
  public filter(): void {
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

     this.usersService.filter(this.form.value).subscribe((res: User[]) => {
      this.totalRow = res["totalRows"];
      this.dsUsers = res["users"];
     });
  }
  
  public clearFilter(): void {
    this.form.controls.name.setValue('');
    this.form.controls.currentPage.setValue(0);
    this.form.controls.pageLength.setValue(10);
    this.filter();
  }

  public add(): void {
    this.routeStack.goTo(this.router, 'app/users/add');
  }
  
    public pageChangeEvent(pageEvent: PageEvent): void {
      this.form.controls.currentPage.setValue(pageEvent.pageIndex);
      this.form.controls.pageLength.setValue(pageEvent.pageSize);
      this.filter();
    }

    private createFormGroup(): FormGroup {
        return this.builder.group({
          name: ['', [
            CustomValidator.maxLength(100),
            CustomValidator.minLength(5)
          ]],
          currentPage: [0, []],
          pageLength: [10, []]
        });
      }
  
    public openBottomSheet(id: any): void {
      var bt = this.bottomSheet.open(ActionBottomSheetComponent, {
        data: this.createAction(id)
      });
  
      bt.afterDismissed().subscribe((actionBottomSheet: ActionBottomSheet) => {
        if (actionBottomSheet)
          actionBottomSheet.action();
      });
    }
  
    private createAction(id: any): ActionBottomSheet[] {
      return [
        {
          icon: 'edit',
          id: id,
          class: '',
          title: 'Alterar',
          description: 'Alterar dados usuário',
          hidden: false,
          action: () => {
            this.routeStack.goTo(this.router, 'app/users/add', id);
          }
        }
      ];
    }
}