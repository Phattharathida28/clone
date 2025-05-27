import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { SortEvent, SortMeta } from "./table-server.interface";

@Injectable()
export class TableServerService {
    sortFunction$: BehaviorSubject<SortEvent | null> = new BehaviorSubject(null);
    sortFunction = this.sortFunction$.asObservable();
    onSort$: BehaviorSubject<SortMeta | SortMeta[] | null> = new BehaviorSubject(null);
    onSort = this.onSort$.asObservable();
    changePage$ : BehaviorSubject<any> = new BehaviorSubject(null); // Set the current page (pageNumber is 1-based)
    changePage = this.changePage$.asObservable();

    dataTablePage$: BehaviorSubject<any[]> = new BehaviorSubject<any[]>([]);
    dataTablePage = this.dataTablePage$.asObservable();

    constructor() {
    }

    getDataTablePage = () => this.dataTablePage$.getValue();

    push(item: any) {
        const currentData = this.dataTablePage$.value;
        const newData = [...currentData, item];
        this.dataTablePage$.next(newData);
    }

    find = (id) => this.getDataTablePage().find(x=> x['id'] == id)

}
