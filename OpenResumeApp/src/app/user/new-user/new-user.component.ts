import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { UserService } from '../user.service';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { MessageComponent } from 'src/app/shared/message/message.component';
import { User } from '../user.model';
import { Md5 } from 'ts-md5';
import { ErrorMessage } from '../../shared/input-container/error-message.model';

@Component({
  selector: 'openr-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.scss']
})
export class NewUserComponent implements OnInit {

  @ViewChild('Message', { static: false }) Message: MessageComponent;
  userForm: FormGroup;
  emailPattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
  errorMessagesPassword: ErrorMessage[] = [new ErrorMessage('minlength', 'NEW-USER.ERRORS.PASSWORD-LENGTH'),
                                           new ErrorMessage('required', 'ERRORS.REQUIRED')];
  errorMessagesPasswordConfirm: ErrorMessage[] = [new ErrorMessage('passwordNotMatch', 'NEW-USER.ERRORS.PASSWORD-MATCH'),
                                                  new ErrorMessage('required', 'ERRORS.REQUIRED')];
  errorMessagesEmail: ErrorMessage[] = [new ErrorMessage('emailNotMatch', 'NEW-USER.ERRORS.EMAIL-MATCH'),
                                        new ErrorMessage('pattern', 'NEW-USER.ERRORS.INVALID-EMAIL'),
                                        new ErrorMessage('required', 'ERRORS.REQUIRED')];
  errorMessagesUserName: ErrorMessage[] = [new ErrorMessage('minlength', 'NEW-USER.ERRORS.USER-LENGTH'),
                                           new ErrorMessage('required', 'ERRORS.REQUIRED')];
  errorMessagesRequired: ErrorMessage[] = [new ErrorMessage('required', 'ERRORS.REQUIRED')];

  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private translate: TranslateService,
              private cookieService: CookieService,
              private router: Router) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  static equalsTo(group: AbstractControl): { [key: string]: boolean } {
    return {
      passwordNotMatch: NewUserComponent.CheckPasswordMatch(group),
      emailNotMatch: NewUserComponent.CheckEmailMatch(group)
    };
  }

  private static CheckEmailMatch(group: AbstractControl): boolean {
    const email = group.get('email');
    const emailConfirmation = group.get('confirmEmail');
    return (email.value !== emailConfirmation.value);
  }

  private static CheckPasswordMatch(group: AbstractControl): boolean {
    const password = group.get('password');
    const passwordConfirmation = group.get('confirmPassword');
    return (password.value !== passwordConfirmation.value);
  }

  ngOnInit() {
    this.userForm = this.formBuilder.group({
      userName: this.formBuilder.control('', [Validators.required, Validators.minLength(6)]),
      firstName: this.formBuilder.control('', [Validators.required]),
      lastName: this.formBuilder.control('', [Validators.required]),
      email: this.formBuilder.control('', [Validators.required, Validators.pattern(this.emailPattern)]),
      confirmEmail: this.formBuilder.control('', [Validators.required, Validators.pattern(this.emailPattern)]),
      password: this.formBuilder.control('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: this.formBuilder.control('', [Validators.required])
    }, { validator: NewUserComponent.equalsTo });
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
