import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UtilService } from 'src/app/common/utils/util.service';
import { RouteStackService } from 'src/app/common/services/route-stack.service';
import { CustomValidator } from 'src/app/common/utils/custom-validator';
import { LessonsService } from 'src/app/common/services/lessons.service';
import { Lessons } from 'src/app/common/models/lessons';


@Component({
  selector: 'app-lessons-form',
  templateUrl: './lessons-form.component.html',
  styleUrls: ['./lessons-form.component.css']
})
export class LessonsFormComponent implements OnInit {

  public form: FormGroup;

  constructor(
    private router: Router,
    private util: UtilService,
    private builder: FormBuilder,
    private route: ActivatedRoute,
    private routeStack: RouteStackService,
    private lessonsService: LessonsService
  ) {
    this.form = this.createFormGroup();
  }

  ngOnInit() {
    this.getLessons(+this.route.snapshot.paramMap.get('id'));
  }

  public submit(value: any): void {
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

    if (value.id) {
      this.lessonsService.put(value).subscribe((res: any) => {
        this.util.snackMsg('Matéria alterada!');
        this.back();
      });
    } else {
      this.lessonsService.post(value).subscribe((res: any) => {
        this.util.snackMsg('Matéria cadastrada!');
        this.back();
      });
    }
  }

  public back(): void {
    this.routeStack.backToCaller(this.router, ['app/lessons/']);
  }
  
  private getLessons(id: number): void {
     if (!id) return;

    this.lessonsService.get(id).subscribe((res: Lessons) => {
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
    });
  }

  @HostListener('window:beforeunload', ['$event']) onBeforeUnload(event) {
    if (this.form.pristine) return;
    event.preventDefault();
    event.returnValue = 'Unsaved modifications';
    return event;
  }
}
