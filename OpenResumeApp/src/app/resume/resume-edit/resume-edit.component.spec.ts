import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResumeEditComponent } from './resume-edit.component';

describe('ResumeEditComponent', () => {
  let component: ResumeEditComponent;
  let fixture: ComponentFixture<ResumeEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResumeEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResumeEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
