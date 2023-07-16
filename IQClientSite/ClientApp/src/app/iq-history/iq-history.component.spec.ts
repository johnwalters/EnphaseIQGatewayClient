import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IqHistoryComponent } from './iq-history.component';

describe('IqHistoryComponent', () => {
  let component: IqHistoryComponent;
  let fixture: ComponentFixture<IqHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IqHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IqHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
