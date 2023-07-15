import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeterReadingsDetailComponent } from './meter-readings-detail.component';

describe('MeterReadingsDetailComponent', () => {
  let component: MeterReadingsDetailComponent;
  let fixture: ComponentFixture<MeterReadingsDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MeterReadingsDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MeterReadingsDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
