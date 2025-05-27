import { Component } from '@angular/core';
import { ListItemGroup } from '@app/models/db/listItemGroup';
import { Dbrt02Service } from './dbrt02.service';
import { ActivatedRoute } from '@angular/router';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { NotifyService } from '@app/core/services/notify.service';
import { filter, switchMap } from 'rxjs';

@Component({
  selector: 'x-dbrt02',
  templateUrl: './dbrt02.component.html'
})
export class Dbrt02Component {
  listItemGroups: ListItemGroup[] = []
  resetSerch: string = ''

  constructor(
    private sv: Dbrt02Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ listItemGroups }) => this.listItemGroups = listItemGroups)
  }

  search(value?: string): void {
    this.sv.list(value).subscribe((listItemGroups: ListItemGroup[]) => this.listItemGroups = listItemGroups)
  }

  delete(listItemGroupCode: string): void {
    this.md.confirm('message.STD00015').pipe(
      filter((confirm: boolean) => confirm),
      switchMap(() => this.sv.delete(listItemGroupCode)),
      switchMap(() => this.sv.list()))
      .subscribe((listItemGroups: ListItemGroup[]) => {
        this.listItemGroups = listItemGroups
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}
