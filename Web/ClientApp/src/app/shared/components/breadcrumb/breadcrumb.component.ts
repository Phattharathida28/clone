import { Component, Input, OnInit } from '@angular/core';
import { NotifyService } from '@app/core/services/notify.service';
import { MenuItem } from 'primeng/api';
import { zip } from 'rxjs';

@Component({
  selector: 'breadcrumb',
  template: `<p-breadcrumb [style]="{'background': 'none', 'border': 'none'}" [model]="items" [home]="home"></p-breadcrumb>`
})
export class BreadcrumbComponent implements OnInit {
  home: MenuItem = { icon: 'pi pi-home', routerLink: '/' }
  @Input() items: MenuItem[] = []

  constructor(private ms: NotifyService) { }

  ngOnInit(): void {
    const labels = this.items.map(m => m.label)
    zip(labels.map(m => this.ms.translatedMessage(m))).subscribe((res) => {
      this.items.map((m, i) => m.label = res[i])
    })
  }
}
