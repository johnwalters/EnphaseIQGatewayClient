import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsumptionHistoryComponent } from './consumption-history.component';

describe('ConsumptionHistoryComponent', () => {
  let component: ConsumptionHistoryComponent;
  let fixture: ComponentFixture<ConsumptionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConsumptionHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsumptionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
