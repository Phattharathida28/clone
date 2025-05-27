import { Component } from '@angular/core';
import { LayoutService } from "./service/layout.service";

@Component({
    selector: 'x-footer',
    templateUrl: './footer.component.html'
})
export class FooterComponent {
    constructor(public layoutService: LayoutService) { }
}
