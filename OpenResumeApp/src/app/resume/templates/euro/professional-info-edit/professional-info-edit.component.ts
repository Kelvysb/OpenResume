import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-professional-info-edit',
  templateUrl: './professional-info-edit.component.html',
  styleUrls: ['../euro.scss']
})
export class ProfessionalInfoEditComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
