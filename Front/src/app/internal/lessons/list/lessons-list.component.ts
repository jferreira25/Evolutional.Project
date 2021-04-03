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
import { Lessons } from 'src/app/common/models/lessons';
import { LessonsService } from 'src/app/common/services/lessons.service';

@Component({
    selector: 'app-lessons-list',
    templateUrl: './lessons-list.component.html',
    styleUrls: ['./lessons-list.component.css']
  })
  export class LessonsListComponent implements OnInit {
  
    public form: FormGroup;
    public dsLessons: MatTableDataSource<Lessons[]>;
    public totalRow: number;

    constructor(
      private builder: FormBuilder,
      private router: Router,
      private routeStack: RouteStackService,
      private bottomSheet: MatBottomSheet,
      private util: UtilService,
      private lessonsService: LessonsService
      
    ) {
      this.form = this.createFormGroup();
      this.dsLessons = new MatTableDataSource();
    }
  
    ngOnInit() {
      this.filter();
      this.dsLessons = new MatTableDataSource();
    }
  
  public filter(): void {
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

     this.lessonsService.filter(this.form.value).subscribe((res: Lessons[]) => {
       this.totalRow = res["totalRows"];
      this.dsLessons = res["lessons"];
     });
  }
  
  public clearFilter(): void {
    this.form.controls.name.setValue('');
    this.form.controls.currentPage.setValue(0);
    this.form.controls.pageLength.setValue(10);
    this.filter();
  }

  public add(): void {
    this.routeStack.goTo(this.router, 'app/lessons/add');
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
          description: 'Alterar nome matéria',
          hidden: false,
          action: () => {
            this.routeStack.goTo(this.router, 'app/lessons/add', id);
          }
        }
      ];
    }
}