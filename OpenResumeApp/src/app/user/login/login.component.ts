import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { User } from '../user.model';
import { Md5 } from 'ts-md5/dist/md5';
import { MessageComponent } from 'src/app/shared/message/message.component';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { ErrorMessage } from 'src/app/shared/input-container/error-message.model';
import { PatternService } from '../../shared/pattern.service';
import { ErrorService } from '../../shared/error.service';
import { LanguageService } from '../../shared/language.service';

@Component({
  selector: 'openr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @ViewChild('Message', {static: false}) Message: MessageComponent;
  loginForm: FormGroup;
  errorMessagesRequired: ErrorMessage[] = [new ErrorMessage('required', 'ERRORS.REQUIRED')];

  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private translate: TranslateService,
              private patternService: PatternService,
              public errorService: ErrorService,
              private cookieService: CookieService,
              private languageService: LanguageService,
              private router: Router) { }
  ngOnInit() {
    this.translate.use(this.languageService.Current());

    if (this.cookieService.check('User')) {
      this.router.navigateByUrl('/user');
    }

    this.loginForm = this.formBuilder.group({
      email: this.formBuilder.control('', [Validators.required, Validators.pattern(this.patternService.Email)]),
      password: this.formBuilder.control('', [Validators.required])
    });
  }

  login() {
    const user: User = {
        email: this.loginForm.controls.email.value,
        passwordHash: Md5.hashStr(this.loginForm.controls.password.value) as string
      };

    this.userService.Login(user).subscribe((result: User) => {
        this.ExecuteLogin(result);
      }, (error: any) => {
        this.translate.get('LOGIN.ERRORS.' + error.error).subscribe((text: string) => {
          this.Message.Show(text, true, 6000);
        });
      });

  }

  ExecuteLogin(user: User) {
    this.userService.SetLoggegUser(user);
    this.router.navigateByUrl('/user');
  }

}
