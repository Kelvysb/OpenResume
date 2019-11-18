import { Component, OnInit, Input, ContentChild, AfterContentInit } from '@angular/core';
import { NgModel, FormControlName } from '@angular/forms';
import { ErrorMessage } from './error-message.model';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-input-container',
  templateUrl: './input-container.component.html'
})
export class InputContainerComponent implements OnInit, AfterContentInit {

  input: any;
  @Input() label: string;
  @Input() ErrorMessages: ErrorMessage[];
  @ContentChild(NgModel, {static: false}) model: NgModel;
  @ContentChild(FormControlName, {static: false}) control: FormControlName;

  constructor(private translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
   }

  ngOnInit() {
  }

  ngAfterContentInit(): void {
    this.input = this.model || this.control;
    if (this.input === undefined) {
      throw new Error('This component must be used with an directive ng-model or FormControlName.');
    }
  }

  CheckErrors(): boolean {
    let result = false;
    if (this.ErrorMessages !== undefined) {
      this.ErrorMessages.forEach(element => {
        if (this.HasError(element.error)) {
          result = true;
        }
      });
    }
    return result;
  }

  HasError(error: string): boolean {
    return (this.input._parent.form.hasError(error) || this.input.hasError(error))
            && (this.input.dirty || this.input.touched)
            && this.ErrorMessages !== undefined;
  }

  CheckOk(): boolean {
    return this.input.valid
           && (this.input.dirty || this.input.touched)
           && this.ErrorMessages !== undefined
           && !this.CheckErrors();
  }
}
