import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-skills-edit',
  templateUrl: './skills-edit.component.html',
  styleUrls: ['../euro.scss']
})
export class SkillsEditComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
