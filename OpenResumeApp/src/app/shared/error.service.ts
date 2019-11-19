import { ErrorMessage } from './input-container/error-message.model';
import { NgModule } from '@angular/core';

@NgModule()
export class ErrorService {
    constructor() { }

  Password: ErrorMessage[] = [new ErrorMessage('minlength', 'NEW-USER.ERRORS.PASSWORD-LENGTH'),
                              new ErrorMessage('required', 'ERRORS.REQUIRED')];

  PasswordConfirm: ErrorMessage[] = [new ErrorMessage('passwordNotMatch', 'NEW-USER.ERRORS.PASSWORD-MATCH'),
                                     new ErrorMessage('required', 'ERRORS.REQUIRED')];

  Email: ErrorMessage[] = [new ErrorMessage('emailNotMatch', 'NEW-USER.ERRORS.EMAIL-MATCH'),
                           new ErrorMessage('pattern', 'NEW-USER.ERRORS.INVALID-EMAIL'),
                           new ErrorMessage('required', 'ERRORS.REQUIRED')];

  UserName: ErrorMessage[] = [new ErrorMessage('minlength', 'NEW-USER.ERRORS.USER-LENGTH'),
                              new ErrorMessage('required', 'ERRORS.REQUIRED')];

  Required: ErrorMessage[] = [new ErrorMessage('required', 'ERRORS.REQUIRED')];
}
