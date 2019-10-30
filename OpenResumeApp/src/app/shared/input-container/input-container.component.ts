import { Component, OnInit, Input, ContentChild, AfterContentInit } from '@angular/core';
import {NgModel, FormControlName} from '@angular/forms';

@Component({
  selector: 'openr-input-container',
  templateUrl: './input-container.component.html'
})
export class InputContainerComponent implements OnInit, AfterContentInit {

  input: any;
  @Input() label: string;
  @Input() errorMessage: string;
  @ContentChild(NgModel, {static: false}) model: NgModel;
  @ContentChild(FormControlName, {static: false}) control: FormControlName;

  constructor() { }

  ngOnInit() {
  }

  ngAfterContentInit(): void {
    this.input = this.model || this.control;
    if (this.input === undefined) {
      throw new Error('This component must be used with an directive ng-model or FormControlName.');
    }
  }

  hasSuccess(): boolean {
    return this.input.valid && (this.input.dirty || this.input.touched) && this.errorMessage !== undefined;
  }

  hasError(): boolean {
    return !this.input.valid && (this.input.dirty || this.input.touched) && this.errorMessage !== undefined;
  }
}
