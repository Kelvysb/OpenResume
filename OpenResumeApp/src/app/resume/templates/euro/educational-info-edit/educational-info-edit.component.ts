import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-educational-info-edit',
  templateUrl: './educational-info-edit.component.html',
  styleUrls: ['../euro.scss']
})
export class EducationalInfoEditComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
