import { Component, OnInit, HostListener } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { UtilService } from 'src/app/common/utils/util.service';
import { RouteStackService } from 'src/app/common/services/route-stack.service';
import { CustomValidator } from 'src/app/common/utils/custom-validator';
import { UsersService } from 'src/app/common/services/users.service';
import { User } from 'src/app/common/models/user';


@Component({
  selector: 'app-users-form',
  templateUrl: './users-form.component.html',
  styleUrls: ['./users-form.component.css']
})
export class UsersFormComponent implements OnInit {

  public form: FormGroup;

  constructor(
    private router: Router,
    private util: UtilService,
    private builder: FormBuilder,
    private route: ActivatedRoute,
    private routeStack: RouteStackService,
    private usersService: UsersService
  ) {
    this.form = this.createFormGroup();
  }

  ngOnInit() {
    this.getUsers(+this.route.snapshot.paramMap.get('id'));
  }

  public submit(value: any): void {
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

    if (value.id) {
      this.usersService.put(value).subscribe((res: any) => {
        this.util.snackMsg('usuário alterado!');
        this.back();
      });
    } else {
      this.usersService.post(value).subscribe((res: any) => {
        this.util.snackMsg('usuario cadastrado!');
        this.back();
      });
    }
  }

  public back(): void {
    this.routeStack.backToCaller(this.router, ['app/user/']);
  }
  
  private getUsers(id: number): void {
     if (!id) return;

    this.usersService.get(id).subscribe((res: User) => {
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
      password:['', [
        CustomValidator.required,
        CustomValidator.maxLength(10),
        CustomValidator.minLength(5)
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
