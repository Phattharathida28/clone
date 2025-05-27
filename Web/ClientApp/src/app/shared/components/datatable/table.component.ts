import { animate, state, style, transition, trigger } from '@angular/animations';
import { DataSource } from '@angular/cdk/collections';
import { AfterContentInit, ChangeDetectorRef, Component, ContentChild, ContentChildren, EventEmitter, Input, OnInit, Output, Query, QueryList, SimpleChanges, TemplateRef, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, MatSortable, MatSortHeader, Sort } from '@angular/material/sort';
import { MatHeaderRowDef, MatRowDef, MatColumnDef, MatNoDataRow, MatTable, MatTableDataSource, MatFooterCellDef, MatFooterRowDef } from '@angular/material/table';
import { merge } from 'rxjs';


@Component({
  selector: 'app-table:not([server])',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed,void', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
      transition('expanded <=> void', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)'))
    ]),
  ],
})
export class DataTableComponent<T> implements AfterContentInit {
  @ContentChildren(MatHeaderRowDef) headerRowDefs!: QueryList<MatHeaderRowDef>;
  @ContentChildren(MatRowDef) rowDefs!: QueryList<MatRowDef<T>>;
  @ContentChildren(MatColumnDef) columnDefs!: QueryList<MatColumnDef>;
  @ContentChildren(MatFooterRowDef) footerRowDefs!: QueryList<MatFooterRowDef>;

  @ViewChild(MatTable, { static: true }) table!: MatTable<T>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ContentChild(MatSort) sort!: MatSort;
  @ContentChild(TemplateRef) detailTemplate!: TemplateRef<any>;
  @Input() multiTemplateDataRows: boolean = true;
  @Input() columns: string[] = [];
  @Input() dataSource!: MatTableDataSource<T>;
  @Input() pagination = true;
  @Input() defaultPageSize = 5;
  @Input() expandRow = false;
  @Input() containerHeight: string | number = 'auto';
  pageSizeOption = [5, ...Array.from(Array(20).keys()).map((v, i) => 10 + i * 10)];
  expandedElement = new Set<any>();
  currentRow?: any;

  private previousMatColumnDef!: MatColumnDef[];

  ngOnInit() {
    if (this.expandRow) {
      this.columns.unshift('expand');
    }

  }

  // constructor(@Optional() @Self() public matSort: MatSort){ //next project can set on component
  //   console.log(matSort)
  // }
  constructor(private dtr: ChangeDetectorRef) {

  }

  getPropertyByPath(obj: Object, pathString: string) {
    return pathString.split('.').reduce((o, i) => o[i], obj);
  }

  ngAfterViewInit() {
    if (this.pagination) {
      this.dataSource.paginator = this.paginator;
    }
  }

  ngAfterContentInit() {
    this.columnDefs.changes.subscribe(c => {
      if (this.previousMatColumnDef.length) {
        this.previousMatColumnDef.forEach(columnDef => this.table.removeColumnDef(columnDef));
      }
      this.columnDefs.forEach(columnDef => this.table.addColumnDef(columnDef));
      this.previousMatColumnDef = [...this.columnDefs];
    })

    if (this.sort) {
      this.dataSource.sortingDataAccessor = (data, sortHeaderId: string) => {
        return this.getPropertyByPath(data, sortHeaderId);
      }
      this.dataSource.sort = this.sort;
    }
    this.columnDefs.forEach(columnDef => this.table.addColumnDef(columnDef));
    this.previousMatColumnDef = [...this.columnDefs];

    if (this.rowDefs?.length) {
      this.rowDefs.forEach(rowDef => this.table.addRowDef(rowDef));
    }
    this.headerRowDefs.forEach(headerRowDef => this.table.addHeaderRowDef(headerRowDef));
    this.footerRowDefs.forEach(footerRowDef => this.table.addFooterRowDef(footerRowDef));

  }

  expandAll() {
    if (this.expandRow) {
      this.dataSource.data.forEach(item => {
        this.expandedElement.add(item);
      })
      this.dtr.detectChanges();
    }
  }

  toggle(row: any) {
    this.expandedElement.has(row) ? this.expandedElement.delete(row) : this.expandedElement.add(row)
  }

  highlight(row: any) {
    this.currentRow = row;
  }

  goLastPage() {
    if (this.dataSource.paginator) {
      this.dataSource.paginator.lastPage();
    }
  }

  goPage(pageNo: any) {
    this.paginator.pageIndex = pageNo;
    this.paginator._changePageSize(this.paginator.pageSize);
  }
}
