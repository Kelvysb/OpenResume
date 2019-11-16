import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { UserService } from '../user.service';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { MessageComponent } from 'src/app/shared/message/message.component';
import { User } from '../user.model';
import { Md5 } from 'ts-md5';

@Component({
  selector: 'openr-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.scss']
})
export class NewUserComponent implements OnInit {

  @ViewChild('Message', {static: false}) Message: MessageComponent;
  userForm: FormGroup;
  emailPattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private translate: TranslateService,
              private cookieService: CookieService,
              private router: Router) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  static equalsTo(group: AbstractControl): {[key: string]: boolean} {
    const password = group.get('password');
    const passwordConfirmation = group.get('cofirmPassword');
    if (!password || !passwordConfirmation) {
      return undefined;
    }
    if (password.value !== passwordConfirmation.value) {
      return {passwordNotMatch: true};
    }
    return undefined;
  }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      userName: this.formBuilder.control('', [Validators.required]),
      firstName: this.formBuilder.control('', [Validators.required]),
      lastName: this.formBuilder.control('', [Validators.required]),
      email: this.formBuilder.control('', [Validators.required, Validators.pattern(this.emailPattern)]),
      password: this.formBuilder.control('', [Validators.required]),
      confirmPassword: this.formBuilder.control('', [Validators.required])
    }, {validator: NewUserComponent.equalsTo});
  }

  Confirm() {
    const user: User = {
        email: this.userForm.controls.email.value,
        passwordHash: Md5.hashStr(this.userForm.controls.password.value) as string
      };

    this.userService.Login(user).subscribe((result: User) => {
        this.ExecuteConfirm();
      }, (error: any) => {
        this.translate.get('NEW-USER.ERRORS.' + error.error).subscribe((text: string) => {
          this.Message.Show(text, true, 6000);
        });
      });
  }

  ExecuteConfirm() {

  }
}
