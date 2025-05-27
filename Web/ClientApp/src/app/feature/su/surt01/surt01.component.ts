import { Component } from '@angular/core';
import { Surt01Service } from './surt01.service';
import { ActivatedRoute } from '@angular/router';
import { Program } from '@app/models/su/program';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { NotifyService } from '@app/core/services/notify.service';
import { filter, switchMap } from 'rxjs';

@Component({
  selector: 'x-surt01',
  templateUrl: './surt01.component.html'
})
export class Surt01Component {
  programs: Program[] = []
  resetSerch = ''
  constructor(
    private sv: Surt01Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ programs }) => this.programs = programs)
  }

  search(value?: string) {
    this.sv.list(value).subscribe((programs: Program[]) => this.programs = programs)
  }

  delete(programCode: string) {
    this.md.confirm('message.STD00015').pipe(
      filter(confirm => confirm),
      switchMap(() => this.sv.delete(programCode)))
      .subscribe((res: any) => {
        this.search()
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}
