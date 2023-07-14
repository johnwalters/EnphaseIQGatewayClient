import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetersDetailComponent } from './meters-detail.component';

describe('MetersDetailComponent', () => {
  let component: MetersDetailComponent;
  let fixture: ComponentFixture<MetersDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MetersDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MetersDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
