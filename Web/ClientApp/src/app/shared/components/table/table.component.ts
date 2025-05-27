import { AfterContentInit, AfterViewInit, Component, ContentChildren, ElementRef, Input, OnChanges, OnInit, QueryList, SimpleChanges, TemplateRef, ViewChild } from '@angular/core';
import { PrimeTemplate } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'datatable:not([server])',
  templateUrl: './table.component.html'
})
export class TableComponent implements OnInit, AfterContentInit, AfterViewInit, OnChanges {
  @Input() rows!: any[];
  @Input() paginator: boolean = true;
  @Input() showCurrentPageReport: boolean = true;
  @Input() rowsPerPageOptions: number[] = [10, 25, 50, 100];
  @Input() sort: boolean = true;

  originalData: any[] = [];
  page: number = 10;
  sortField: string = "";
  sortOrder: number = 0;

  @ContentChildren(PrimeTemplate)
  templates!: QueryList<any>;
  headerTemplate!: TemplateRef<any>;
  bodyTemplate!: TemplateRef<any>;
  captionTemplate!: TemplateRef<any>;
  summaryTemplate!: TemplateRef<any>;

  @ViewChild('table') table: Table;

  constructor() { }

  ngOnInit(): void {
    this.originalData = [...this.rows]
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.originalData = [...changes["rows"]["currentValue"]]
  }

  ngAfterContentInit() {
    this.headerTemplate = this.gePrimeTemplateByType('header')?.template;
    this.bodyTemplate = this.gePrimeTemplateByType('body')?.template;
    this.captionTemplate = this.gePrimeTemplateByType('caption')?.template;
    this.summaryTemplate = this.gePrimeTemplateByType('summary')?.template;
  }

  ngAfterViewInit(): void {
    if (this.rows.length > 0 && this.sort) {
      this.table.el.nativeElement.querySelectorAll("div div table thead tr th").forEach((f, i) => {
        const prop = f.getAttribute("prop");
        if (prop) {
          f.classList.add("cursor-pointer")
          f.addEventListener('click', () => this.onThClick(f, prop));
          const sortIcon = document.createElement('i');
          sortIcon.setAttribute('class', `pl-1 pi pi-sort-alt`);
          f.appendChild(sortIcon)
        }
      })
    }
  }

  gePrimeTemplateByType(type: string): PrimeTemplate {
    return this.templates.find(template => {
      return template.getType() === type;
    });
  }

  onThClick(element: Element, column: string) {
    this.sortField = column

    if (element.querySelector("i").classList.contains("pi-sort-alt")) {
      element.querySelector("i").classList.remove("pi-sort-alt")
      element.querySelector("i").classList.add("pi-sort-amount-down")
      this.sortOrder = -1
      this.table.el.nativeElement.querySelectorAll(`div div table thead tr th:not([prop="${column}"]) i`).forEach(f => f.setAttribute("class", `pl-1 pi pi-sort-alt`))
    }
    else if (element.querySelector("i").classList.contains("pi-sort-amount-down")) {
      element.querySelector("i").classList.remove("pi-sort-amount-down")
      element.querySelector("i").classList.add("pi-sort-amount-up")
      this.sortOrder = 1
      this.table.el.nativeElement.querySelectorAll(`div div table thead tr th:not([prop="${column}"]) i`).forEach(f => f.setAttribute("class", `pl-1 pi pi-sort-alt`))
    }
    else if (element.querySelector("i").classList.contains("pi-sort-amount-up")) {
      element.querySelector("i").classList.remove("pi-sort-amount-up")
      element.querySelector("i").classList.add("pi-sort-alt")
      this.sortOrder = 0
      this.table.el.nativeElement.querySelectorAll(`div div table thead tr th:not([prop="${column}"]) i`).forEach(f => f.setAttribute("class", `pl-1 pi pi-sort-alt`))
    }

    this.sortData();
  }

  sortData() {
    if (this.sortField) {
      if (this.sortOrder == 0) {
        this.rows = [...this.originalData]
      }
      else {
        this.rows.sort((a, b) => {
          const valueA = a[this.sortField] || "";
          const valueB = b[this.sortField] || "";

          if (valueA < valueB) {
            return -1 * this.sortOrder;
          } else if (valueA > valueB) {
            return 1 * this.sortOrder;
          } else {
            return 0;
          }
        });
      }
    }
  }
}
