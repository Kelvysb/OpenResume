import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { User } from '../user.model';
import {Md5} from 'ts-md5/dist/md5';
import { NotifyService } from 'src/app/shared/notify.service';
import { MessageComponent } from 'src/app/shared/message/message.component';
import { TranslateService } from '@ngx-translate/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'openr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @ViewChild('Message', {static: false}) Message: MessageComponent;
  loginForm: FormGroup;
  emailPattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

  constructor(private formBuilder: FormBuilder,
              private userService: UserService,
              private translate: TranslateService,
              private cookieService: CookieService,
              private router: Router) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }
  ngOnInit() {
    if (this.cookieService.check('User')) {
      this.router.navigateByUrl('/user');
    }

    this.loginForm = this.formBuilder.group({
      email: this.formBuilder.control('', [Validators.required, Validators.pattern(this.emailPattern)]),
      password: this.formBuilder.control('', [Validators.required])
    });
  }

  login() {
    const user: User = {
        Email: this.loginForm.controls.email.value,
        PasswordHash: Md5.hashStr(this.loginForm.controls.password.value) as string
      };

    this.userService.login(user).subscribe((result: User) => {
        this.ExecuteLogin(result);
      }, (error: any) => {
        this.translate.get('LOGIN.ERRORS.' + error.error).subscribe((text: string) => {
          this.Message.Show(text, true, 6000);
        });
      });

  }

  ExecuteLogin(user: User) {
    this.cookieService.set('User', JSON.stringify(user));
    this.router.navigateByUrl('/user');
  }

}
