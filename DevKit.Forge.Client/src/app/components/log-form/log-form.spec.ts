import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogForm } from './log-form';

describe('LogForm', () => {
  let component: LogForm;
  let fixture: ComponentFixture<LogForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LogForm],
    }).compileComponents();

    fixture = TestBed.createComponent(LogForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
