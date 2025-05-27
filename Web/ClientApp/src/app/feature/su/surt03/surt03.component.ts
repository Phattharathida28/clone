import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from '@app/core/services/notify.service';
import { Profile } from '@app/models/su/profile';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { filter, switchMap } from 'rxjs';
import { Surt03Service } from './surt03.service';

@Component({
  selector: 'x-surt03',
  templateUrl: './surt03.component.html',
  styles: ``
})
export class Surt03Component {
  profiles: Profile[] = []
  resetSerch:string = ''

  constructor(
    private sv: Surt03Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ profiles }) => this.profiles = profiles)
  }

  search(value?: string) {
    this.sv.list(value).subscribe((profiles: Profile[]) => this.profiles = profiles)
  }

  delete(profileCode: string) {
    this.md.confirm('message.STD00015').pipe(
      filter(confirm => confirm),
      switchMap(() => this.sv.delete(profileCode)))
      .subscribe((res: any) => {
        this.search()
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}
