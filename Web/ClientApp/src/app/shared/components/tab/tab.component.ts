import { AfterContentInit, AfterViewInit, Component, ContentChildren, Input, QueryList, TemplateRef, ViewChild } from '@angular/core';
import { SelectItem, PrimeTemplate } from 'primeng/api';
import { TabView } from 'primeng/tabview';

@Component({
  selector: 'tab',
  templateUrl: './tab.component.html',
  styles: ``
})
export class TabComponent implements AfterContentInit, AfterViewInit {
  @Input() items: SelectItem[] = [];

  @ViewChild("tab") tab: TabView
  @ContentChildren(PrimeTemplate)
  templates!: QueryList<any>;
  contentTemplate!: TemplateRef<any>;

  constructor() { }

  gePrimeTemplateByType(type: string): PrimeTemplate {
    return this.templates.find(template => {
      return template.getType() === type;
    });
  }

  ngAfterContentInit() {
    this.contentTemplate = this.gePrimeTemplateByType('content')?.template;
  }

  ngAfterViewInit(): void {
      this.onChange()
  }

  onChange() {
    const tab = this.tab.el.nativeElement.querySelector(".p-tabview").classList

    tab.remove("p-tabview-active")
    setTimeout(() => {
      tab.add("p-tabview-active")
    }, 0);
  }
}
