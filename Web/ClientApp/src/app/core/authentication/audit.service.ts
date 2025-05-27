import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuditService {

  private programCode = null;

  get code() {
    return this.programCode;
  }

  set code(value) {
    this.programCode = value;
  }
}