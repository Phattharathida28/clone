import { Component, ElementRef } from '@angular/core';
import { LayoutService } from "./service/layout.service";

@Component({
    selector: 'x-sidebar',
    templateUrl: './sidebar.component.html'
})
export class SidebarComponent {
    constructor(public layoutService: LayoutService, public el: ElementRef) { }
}

