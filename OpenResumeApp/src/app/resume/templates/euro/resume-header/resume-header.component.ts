import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'openr-resume-header',
  templateUrl: './resume-header.component.html',
  styleUrls: ['../euro.scss']
})
export class ResumeHeaderComponent implements OnInit {

  constructor(translate: TranslateService) {
    translate.setDefaultLang('en-us');
    translate.use('en-us');
  }

  ngOnInit() {
  }

}
