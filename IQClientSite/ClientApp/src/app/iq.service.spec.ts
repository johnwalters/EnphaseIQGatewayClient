import { TestBed } from '@angular/core/testing';

import { IqService } from './iq.service';

describe('IqService', () => {
  let service: IqService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IqService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
