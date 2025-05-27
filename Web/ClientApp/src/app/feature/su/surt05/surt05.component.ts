import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotifyService } from '@app/core/services/notify.service';
import { Message } from '@app/models/su/message';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { Surt05Service } from './surt05.service';
import { filter, switchMap } from 'rxjs';

@Component({
  selector: 'x-surt05',
  templateUrl: './surt05.component.html',
})
export class Surt05Component {
  messages: Message[] = []
  resetSerch:string = ''

  constructor(
    private sv: Surt05Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ messages }) => {
      this.messages = messages
    })
  }

  search(value?: string) {
    this.sv.list(value).subscribe((message: Message[]) => this.messages = message)
  }


  delete(messageCode: string) {
    this.md.confirm('message.STD00015').pipe(
      filter(confirm => confirm),
      switchMap(() => this.sv.delete(messageCode)))
      .subscribe((res: any) => {
        this.search()
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}
