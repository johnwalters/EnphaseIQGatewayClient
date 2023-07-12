import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DbDetailComponent } from './db-detail.component';

describe('DbDetailComponent', () => {
  let component: DbDetailComponent;
  let fixture: ComponentFixture<DbDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DbDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DbDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
