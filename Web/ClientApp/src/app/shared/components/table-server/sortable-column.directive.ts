import {
  Directive,
  HostListener,
  Output,
  EventEmitter,
  Input,
  ElementRef,
} from '@angular/core';
import { Table, TableService } from 'primeng/table';
import { TableServerService } from './table-server.service';
import { SortEvent, SortMeta } from './table-server.interface';
import { PaginatedDataSource } from './server-datasource';

@Directive({
  selector: '[pSortableColumn]',
})
export class SortableColumnDirective {
  constructor(
    private dataTable: Table,
    private dataTableService: TableServerService
  ) { }

  @HostListener('click', ['$event'])
  onClick(event: MouseEvent) {
    if (
      (document.getElementsByTagName('datatable') &&
        document.getElementsByTagName('datatable').length > 0)
      &&
      (document.getElementsByClassName('datatable [server]') &&
        document.getElementsByClassName('datatable [server]').length > 0)
    ) {
      event.preventDefault();
      event.stopPropagation();

      if (this.dataTable) {
        let sortEvent: SortEvent = {
          mode: this.dataTable.sortMode,
          field: this.dataTable.sortField,
          order: this.dataTable.sortOrder,
        };
        let sortMeta: SortMeta = {
          field: this.dataTable.sortField,
          order: this.dataTable.sortOrder,
        };

        this.dataTable.sortFunction.emit(sortEvent);
        this.dataTable.onSort.emit(sortMeta);
        this.dataTableService.sortFunction$.next(sortEvent);
        this.dataTableService.onSort$.next(sortMeta);
      }
    }
  }
}

@Directive({
  selector: '[dataTableServerRoot]',
})
export class TableServerSortableColumnDirective {
  @Input() id;
  @Input() dataSource: PaginatedDataSource<any, any>;

  constructor(
    private dataTable: Table,
    private dataTableService: TableServerService,
    private elRef: ElementRef,
    private tableService: TableService
  ) { }
  @Output() sortFunction: EventEmitter<SortEvent> =
    new EventEmitter<SortEvent>();
  @Output() onSort: EventEmitter<SortMeta | SortMeta[]> = new EventEmitter<
    SortMeta | SortMeta[]
  >();

  private subscription: any;

  ngAfterViewInit() {
    let dataSourcePage: any = {};
    dataSourcePage['id'] = this.id;
    let getPage = <any>this.dataTableService.find(this.id);
    if (!getPage || getPage.length == 0) {
      this.dataTableService.push(dataSourcePage);
    } else {
      dataSourcePage = getPage;
    }

    this.subscription = this.dataTableService.sortFunction.subscribe((event: SortEvent) => {
      if (event) {
        const sortFunction = this.dataTable.sortFunction;
        let returnEvent;

        if (!event['id']) event['id'] = this.id;

        let getPage = <any>this.dataTableService.find(this.id);
        if (getPage && getPage['field']) {
          if (event['id'] && event['id'] == this.id) {
            dataSourcePage['data'] = event.data;
            dataSourcePage['field'] = event.field;
            dataSourcePage['mode'] = event.mode;
            dataSourcePage['multiSortMeta'] = event.multiSortMeta;
            dataSourcePage['order'] = event.order;

            //Output
            this.sortFunction.emit(event);
            returnEvent = event;
          }
          else {
            dataSourcePage['data'] = getPage.data;
            dataSourcePage['field'] = getPage.field;
            dataSourcePage['mode'] = getPage.mode;
            dataSourcePage['multiSortMeta'] = getPage.multiSortMeta;
            dataSourcePage['order'] = getPage.order;

            //Output
            this.sortFunction.emit(getPage);
            returnEvent = getPage;
          }
        }
        else if (getPage && !getPage['field']) {
          if (event['id'] && event['id'] == this.id) {
            dataSourcePage = getPage;
            event.data = this.dataTable.value;
            dataSourcePage['data'] = event.data;
            dataSourcePage['field'] = event.field;
            dataSourcePage['mode'] = event.mode;
            dataSourcePage['multiSortMeta'] = event.multiSortMeta;
            dataSourcePage['order'] = event.order;

            //Output
            this.sortFunction.emit(event);
            returnEvent = event;
          }
        }
        else {
          if (event['id'] && event['id'] == this.id) {
            event.data = this.dataTable.value;
            dataSourcePage['data'] = event.data;
            dataSourcePage['field'] = event.field;
            dataSourcePage['mode'] = event.mode;
            dataSourcePage['multiSortMeta'] = event.multiSortMeta;
            dataSourcePage['order'] = event.order;
            //Output
            this.sortFunction.emit(event);
            returnEvent = event;
          }
          else {
            let getPage = <any>this.dataTableService.find(this.id);

            if (getPage && getPage['field']) {
              //Output
              this.sortFunction.emit(getPage);
              returnEvent = getPage;
            }
            else {
              //Output
              this.sortFunction.emit(getPage);
              returnEvent = getPage;
            }
          }
        }

        //******* Force current sort header style *******
        this.setHighlightHeaderSingleSortField(returnEvent);
      }
    });
  }

  //******* Force current (single) sort header style *******
  setHighlightHeaderSingleSortField(event) {
    if (this.elRef) {
      let d = document.getElementsByClassName(this.id);

      if (d && d.item(0)) {
        let th = d.item(0).getElementsByTagName('th');

        if (th && th.length > 0) {

          for (let i = 0; i < th.length; i++) {
            let itemHead = th.item(i);
            let getSortThName = itemHead.getAttribute('psortablecolumn');

            if (event && event['field'] && getSortThName == event['field']) {
              if (!itemHead.classList.contains('p-highlight')) itemHead.classList.add('p-highlight');
            }
            else {
              if (itemHead.classList.contains('p-highlight')) itemHead.classList.remove('p-highlight');
            }
          }
        }
      }
    }
  }

  ngOnDestroy() {
    // Unsubscribe when the directive is destroyed
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
