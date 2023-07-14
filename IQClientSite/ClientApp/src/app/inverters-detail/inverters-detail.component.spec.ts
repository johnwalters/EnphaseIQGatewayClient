import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvertersDetailComponent } from './inverters-detail.component';

describe('InvertersDetailComponent', () => {
  let component: InvertersDetailComponent;
  let fixture: ComponentFixture<InvertersDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvertersDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvertersDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
