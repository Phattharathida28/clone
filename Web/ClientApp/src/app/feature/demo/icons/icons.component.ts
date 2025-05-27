import { Component } from '@angular/core';
import { ICONS } from './icons.data';
import { NotifyService } from '@app/core/services/notify.service';

@Component({
  selector: 'x-icon',
  templateUrl: './icons.component.html'
})
export class IconsComponent {

  icons: any[] = ICONS;

  filteredIcons: any[] = ICONS;

  constructor(private ns: NotifyService) { }

  onInput(data: string) {
    this.filteredIcons = this.icons.filter(f => f.includes(data))
  }

  copyIcon(icon: string) {
    navigator.clipboard.writeText(`<i class="pi ${icon}"></i>`)
    this.ns.success("Copied.")
  }

}

