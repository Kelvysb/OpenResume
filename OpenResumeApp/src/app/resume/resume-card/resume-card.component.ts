import { Component, OnInit, Input } from '@angular/core';
import { ResumeService } from '../resume.service';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/user/user.service';
import { Resume } from '../resume.model';

@Component({
  selector: 'openr-resume-card',
  templateUrl: './resume-card.component.html',
  styleUrls: ['./resume-card.component.scss']
})
export class ResumeCardComponent implements OnInit {

  @Input() sequence = 0;
  @Input() resume: Resume;

  constructor(private userService: UserService,
              private resumeService: ResumeService,
              private translate: TranslateService,
              private router: Router) {
                this.translate.setDefaultLang('en-us');
                this.translate.use('en-us');
              }

  ngOnInit() {
    
  }
}
