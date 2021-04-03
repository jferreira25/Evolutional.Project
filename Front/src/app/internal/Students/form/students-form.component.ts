import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UtilService } from 'src/app/common/utils/util.service';
import { RouteStackService } from 'src/app/common/services/route-stack.service';
import { CustomValidator } from 'src/app/common/utils/custom-validator';
import { Students } from 'src/app/common/models/students';
import { StudentsService } from 'src/app/common/services/students.service';
import { LessonsService } from 'src/app/common/services/lessons.service';
import { Lessons } from 'src/app/common/models/lessons';


@Component({
  selector: 'app-students-form',
  templateUrl: './students-form.component.html',
  styleUrls: ['./students-form.component.css']
})
export class StudentsFormComponent implements OnInit {

  public form: FormGroup;
  public selectLesson: any[] = [];
  lessonControl = new FormControl('', CustomValidator.required);
  
  constructor(
    private router: Router,
    private util: UtilService,
    private builder: FormBuilder,
    private route: ActivatedRoute,
    private routeStack: RouteStackService,
    private studentsService: StudentsService,
    private lessonsService: LessonsService,
  ) {
    this.form = this.createFormGroup();
  }

  ngOnInit() {
    this.getStudents(+this.route.snapshot.paramMap.get('id'));
    this.getAllLessons();
    
  }


  public submit(value: any): void {
    debugger;
    this.form.controls['lessonId'].setValue(this.lessonControl.value);
    value.lessonId = this.lessonControl.value;
    
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

    if (value.id) {
      this.studentsService.put(value).subscribe((res: any) => {
        this.util.snackMsg('dados de estudante  alterado!');
        this.back();
      });
    } else {
      this.studentsService.post(value).subscribe((res: any) => {
        this.util.snackMsg('dados de estudante cadastrado!');
        this.back();
      });
    }
  }

  public back(): void {
    this.routeStack.backToCaller(this.router, ['app/students/']);
  }
  
  
  private getAllLessons(): void {
   this.lessonsService.filter('').subscribe((res: Lessons[]) => {
    this.selectLesson = res["lessons"];
   this.lessonControl.setValue(1);
    
   });

 }

  private getStudents(id: number): void {
     if (!id) return;

    this.studentsService.get(id).subscribe((res: Students) => {
      this.form.patchValue(res);
    });
  }

  private createFormGroup(): FormGroup {
    return this.builder.group({
      id: ['', []],
      name: ['', [
        CustomValidator.required,
        CustomValidator.maxLength(250)
      ]],
      schoolGrades:['', [
        CustomValidator.required,
        CustomValidator.maxLength(4)
      ]],
      lessonId:['', [
        CustomValidator.required
      ]]
    });
  }

  @HostListener('window:beforeunload', ['$event']) onBeforeUnload(event) {
    if (this.form.pristine) return;
    event.preventDefault();
    event.returnValue = 'Unsaved modifications';
    return event;
  }
}
