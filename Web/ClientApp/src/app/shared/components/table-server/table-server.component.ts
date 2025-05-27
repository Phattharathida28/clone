import {
  Component,
  ContentChild,
  Input,
  SimpleChanges,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { PaginatedDataSource } from './server-datasource';
import { PaginatorState } from 'primeng/paginator/paginator.interface';
import { SortEvent } from './table-server.interface';
import { Paginator } from 'primeng/paginator';
import { TableServerService } from './table-server.service';
import { Subject, Subscription, takeUntil } from 'rxjs';
import { SubscriptionDisposer } from '../subscription-disposer';
import { Guid } from 'guid-typescript';
import { Router } from '@angular/router';
import { Table } from 'primeng/table';

//For demo table server
@Component({
  selector: 'datatable [server]',
  templateUrl: './table-server.component.html'
})
export class TableServerComponent<T> extends SubscriptionDisposer {
  @Input() currentPageReportTemplate = 'Showing {first} - {last} of {totalRecords} entries';
  @Input() rowsPerPageOptions: number[] = [10, 25, 50, 100];
  @Input() breakpoint = '960px';
  @Input() tableStyle: any;
  @Input() styleClass: string = "p-datatable-gridlines";
  @Input() value: PaginatedDataSource<any, any>;
  @Input() paginator: boolean = true;
  @Input() rowHover: boolean = true;
  @Input() rows = 10; //Data count to display per page.
  @Input() loading = false;
  @Input() globalFilterFields = [];
  @Input() responsiveLayout = 'scroll'; //stack,scroll
  @Input() expandedRowKeys: any;
  @Input() scrollable = true;
  @Input() sortMode: any = 'single'; //multiple
  @Input() dataKey;
  @Input() class;


  //Select Items
  selectedDataItems;
  dataSource;
  selectItems;

  //Custom template
  @ViewChild('header') headerElement: any;
  @ContentChild('header') headerTemplate: TemplateRef<any>;
  @ContentChild('body') bodyTemplate: TemplateRef<any>;
  @ContentChild('footer') footerTemplate: TemplateRef<any>;
  @ContentChild('caption') captionTemplate: TemplateRef<any>;
  @ContentChild('rowexpansion') rowexpansionTemplate: TemplateRef<any>;
  @ContentChild('pSortableColumn') sortTemplate: TemplateRef<any>;

  //Paginator
  @ViewChild('paginatorTemplate') paginatorTemplate: Paginator;
  private subscription: Subscription;
  unsubscribe = new Subject();
  currentPage = 0;
  paginationIsRegetServer = true;
  timer;

  @ViewChild('dataTable') dataTable: Table;
  id = Guid.raw().toString();

  constructor(
    private dataTableService: TableServerService,
    private router: Router
  ) {
    super();
    this.subscription = new Subscription();

    this.dataTableService.changePage.subscribe((page) => {
      if (page) {
        if (
          this.paginatorTemplate &&
          (isNaN(this.paginatorTemplate.paginatorState['page']) ||
            this.paginatorTemplate.getPage() != this.currentPage)
        ) {
          if (this.timer) clearTimeout(this.timer);
          this.timer = setTimeout(() => {
            let itemState = {
              page: page['number'],

              rows: page['size'],

              totalRecords: page['count'],
            };

            this.paginationIsRegetServer = false;

            this.onPageChange(itemState, this.paginationIsRegetServer);

            setTimeout(() => {
              this.paginationIsRegetServer = true;
            });
          });
        }
      }
    });

    this.id = router.url;
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['value'] || this.value || this.value.page$) {
      this.subscription = this.value.page$
        .pipe(takeUntil(this.unsubscribe))
        .subscribe((page) => {
          if (page && this.currentPage != page['number']) {
            this.currentPage = page['number'];

            this.dataTableService.changePage$.next(page);
          }
        });
    }
  }

  ngAfterContentInit() { }

  ngAfterViewInit() { }

  onPageChange(event: PaginatorState, reGet = false) {
    this.currentPage = event.page;
    if (
      this.paginatorTemplate &&
      this.paginatorTemplate.getPage() != event.page
    ) {
      this.paginatorTemplate.changePage(event.page);
    }

    if (reGet) {
      this.value.fetch(event);
    }
  }

  sortFunction(event: SortEvent | any) {
    if (event['id'] == this.id) {
      this.value.sortBy(event);
    }
  }
}
