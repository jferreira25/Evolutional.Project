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
import { Students } from 'src/app/common/models/students';
import { StudentsService } from 'src/app/common/services/students.service';
import { DownloadResponse } from 'src/app/common/models/download-response';

@Component({
    selector: 'app-students-list',
    templateUrl: './students-list.component.html',
    styleUrls: ['./students-list.component.css']
  })
  export class StudentsListComponent implements OnInit {
  
    public form: FormGroup;
    public dsStudents: MatTableDataSource<Students[]>;
    public totalRow: number;
    public DisableDownload:boolean = false;

    constructor(
      private builder: FormBuilder,
      private router: Router,
      private routeStack: RouteStackService,
      private bottomSheet: MatBottomSheet,
      private util: UtilService,
      private studentsService: StudentsService
      
    ) {
      this.form = this.createFormGroup();
      this.dsStudents = new MatTableDataSource();
    }
  
    ngOnInit() {
      this.filter();
      this.dsStudents = new MatTableDataSource();
    }

    public back(): void {
      this.routeStack.backToCaller(this.router, ['app/students/']);
    }

  public filter(): void {
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

     this.studentsService.filter(this.form.value).subscribe((res: Students[]) => {
      this.totalRow = res["totalRows"];
      this.dsStudents = res["students"];

      if(this.totalRow==0)
         this.DisableDownload = true;
       else
        this.DisableDownload = false;

     });
  }
  
  public clearFilter(): void {
    this.form.controls.name.setValue('');
    this.form.controls.currentPage.setValue(0);
    this.form.controls.pageLength.setValue(10);
    this.filter();
  }

  public add(): void {
    this.routeStack.goTo(this.router, 'app/students/add');
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
          schoolGrades: ['', [
            CustomValidator.maxLength(100),
            CustomValidator.minLength(5)
          ]],
          lessonName: ['', [
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

    public generate():void{
      this.studentsService.generate().subscribe((res: any) => {
        this.util.snackMsg('dados cadastrado!');
        window.location.reload();
      });
    }

    public download(): void {
      this.studentsService.download().subscribe((res: DownloadResponse) => {
        this.util.download(res);
      });
    }
  
    private createAction(id: any): ActionBottomSheet[] {
      return [
        {
          icon: 'edit',
          id: id,
          class: '',
          title: 'Alterar',
          description: 'Alterar dados do Aluno',
          hidden: false,
          action: () => {
            this.routeStack.goTo(this.router, 'app/students/add', id);
          }
        }
      ];
    }
}