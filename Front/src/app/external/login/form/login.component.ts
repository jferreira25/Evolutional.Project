import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { RouteStackService } from 'src/app/common/services/route-stack.service';
import { CustomValidator } from 'src/app/common/utils/custom-validator';
import { AuthenticationService } from 'src/app/common/services/authentication.service';
import { UtilService } from 'src/app/common/utils/util.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public form: FormGroup;

  constructor(
    private builder: FormBuilder,
    private authenticationService: AuthenticationService,
    private util: UtilService,
    private router: Router,
    private routeStack: RouteStackService
  ) {
    this.form = this.createFormGroup();
  }

  ngOnInit() {
    this.util.logout();
  }

  public login(value: any): void {
    if (this.form.invalid) {
      CustomValidator.showErrors(this.form);
      return;
    }

   this.authenticationService.auth(value).subscribe((res: any) => {
      this.util.setToken(res.token);
      this.routeStack.goTo(this.router, '/app/students');
    });
  }

  private createFormGroup(): FormGroup {
    return this.builder.group({
      login: ['', [
        CustomValidator.required,
        CustomValidator.maxLength(50)
      ]],
      password: ['', [
        CustomValidator.required,
        CustomValidator.maxLength(20)
      ]]
    });
  }
}
