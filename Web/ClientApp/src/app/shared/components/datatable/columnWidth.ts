import { Directive, ElementRef, Input, Renderer2 } from '@angular/core'

@Directive({
  selector: '[columnWidth]',
})
export class ColumnWidthDirective {
    @Input() columnWidth = '';
    elementRef!:ElementRef;
    renderer!:Renderer2;
    constructor(elementRef: ElementRef,renderer: Renderer2) {
       this.elementRef = elementRef;
       this.renderer = renderer;
    }

    ngAfterContentInit(){
        this.renderer.setStyle(this.elementRef.nativeElement, 'minWidth', this.columnWidth);
    }
}