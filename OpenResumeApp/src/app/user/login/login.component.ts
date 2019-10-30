import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { User } from '../user.model';
import {Md5} from 'ts-md5/dist/md5';

@Component({
  selector: 'openr-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  emailPattern = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

  constructor(private formBuilder: FormBuilder,
              private userService: UserService) { }

  ngOnInit() {
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

      console.log(user.PasswordHash);
  }

}
