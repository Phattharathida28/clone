import { DOCUMENT } from '@angular/common';
import { Directive, ElementRef, Inject, NgZone, PLATFORM_ID, Renderer2 } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { ButtonDirective } from 'primeng/button';
import { Ripple } from 'primeng/ripple';

@Directive({
  selector: 'button'
})
export class PrimeButtonDirective extends ButtonDirective {

  constructor(el: ElementRef, @Inject(DOCUMENT) document: Document) {
    super(el, document);
  }
}

@Directive({
  selector: 'button',
})
export class PrimeRippleDirective extends Ripple {

  constructor(@Inject(DOCUMENT) document: Document, @Inject(PLATFORM_ID) platformId: any, renderer: Renderer2, el: ElementRef, zone: NgZone, config: PrimeNGConfig){
    super(document, platformId, renderer, el, zone, config);
  }
}
