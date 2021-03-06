import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-skills-additional-edit',
  templateUrl: './skills-additional-edit.component.html',
  styleUrls: ['../euro.scss']
})
export class SkillsAdditionalEditComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
