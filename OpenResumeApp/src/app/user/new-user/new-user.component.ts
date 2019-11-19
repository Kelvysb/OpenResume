import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { UserService } from '../user.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { MessageComponent } from 'src/app/shared/message/message.component';
import { User } from '../user.model';
import { Md5 } from 'ts-md5';
import { ErrorService } from '../../shared/error.service';
import { PatternService } from '../../shared/pattern.service';
import { LanguageService } from '../../shared/language.service';

@Component({
  selector: 'openr-new-user',
  templateUrl: './new-user.component.html',
  styleUrls: ['./new-user.component.scss']
})
export class NewUserComponent implements OnInit {

  @ViewChild('Message', { static: false }) Message: MessageComponent;
  userForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private translate: TranslateService,
              public errorService: ErrorService,
              private patternService: PatternService,
              private languageService: LanguageService,
              private router: Router) { }

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
    this.translate.use(this.languageService.Current());
    this.userForm = this.formBuilder.group({
      userName: this.formBuilder.control('', [Validators.required, Validators.minLength(6)]),
      firstName: this.formBuilder.control('', [Validators.required]),
      lastName: this.formBuilder.control('', [Validators.required]),
      email: this.formBuilder.control('', [Validators.required, Validators.pattern(this.patternService.Email)]),
      confirmEmail: this.formBuilder.control('', [Validators.required, Validators.pattern(this.patternService.Email)]),
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
