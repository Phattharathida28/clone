import { Component } from '@angular/core';
import { Parameter } from '@app/models/su/parameter';
import { Surt06Service } from './surt06.service';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from '@app/core/services/notify.service';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { filter, switchMap } from 'rxjs';

@Component({
  selector: 'x-surt06',
  templateUrl: './surt06.component.html'
})
export class Surt06Component {
  resetSerch:string = ''
  parameters: Parameter[] = []

  constructor(
    private sv: Surt06Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ list }) => this.parameters = list)
  }

  search(value: string = '') {
    this.sv.list(value).subscribe((parameters: Parameter[]) => {
      this.parameters = parameters
    })
  }

  delete(parameterGroupCode: string, parameterCode: string) {
    this.md.confirm('message.STD00015').pipe(
      filter(confirm => confirm),
      switchMap(() => this.sv.delete(parameterGroupCode, parameterCode)))
      .subscribe((res: any) => {
        this.search()
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}

