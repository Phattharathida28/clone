import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { qort01DetailResolver } from './qort01-detail.resolver';

describe('qort01DetailResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => qort01DetailResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
