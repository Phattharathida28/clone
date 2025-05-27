import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from '@app/core/services/notify.service';
import { User } from '@app/models/su/user';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { filter, switchMap } from 'rxjs';
import { Surt04Service } from './surt04.service';

@Component({
  selector: 'x-surt04',
  templateUrl: './surt04.component.html',
  styles: ``
})
export class Surt04Component {
  users: User[] = []
  resetSerch:string = ''

  constructor(
    private sv: Surt04Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ users }) => this.users = users)
  }

  search(value?: string) {
    this.sv.list(value).subscribe((users: User[]) => this.users = users)
  }

  delete(userId: number) {
    this.md.confirm('message.STD00015').pipe(
      filter(confirm => confirm),
      switchMap(() => this.sv.delete(userId)))
      .subscribe((res: any) => {
        this.search()
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}
