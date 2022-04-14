import { TestBed } from '@angular/core/testing';

import { DataParsingService } from './data-parsing.service';

describe('DataParsingService', () => {
  let service: DataParsingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DataParsingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
