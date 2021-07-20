import { TestBed } from '@angular/core/testing';

import { ApiClienteService } from './api-cliente.service';

describe('ApiClienteService', () => {
  let service: ApiClienteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiClienteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
