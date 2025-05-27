import { Component } from '@angular/core';
import { Language } from '@app/models/db/language';
import { Dbrt01Service } from './dbrt01.service';
import { ActivatedRoute } from '@angular/router';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { NotifyService } from '@app/core/services/notify.service';
import { filter, switchMap } from 'rxjs';

@Component({
  selector: 'x-dbrt01',
  templateUrl: './dbrt01.component.html'
})
export class Dbrt01Component {
  languages: Language[] = []
  resetSerch: string = ''

  constructor(
    private sv: Dbrt01Service,
    private activatedRoute: ActivatedRoute,
    private md: ModalService,
    private ms: NotifyService) {
    this.activatedRoute.data.subscribe(({ languages }) => this.languages = languages)
  }

  search(value?: string): void {
    this.sv.list(value).subscribe((languages: Language[]) => this.languages = languages)
  }

  delete(languageCode: string): void {
    this.md.confirm('message.STD00015').pipe(
      filter((confirm: boolean) => confirm),
      switchMap(() => this.sv.delete(languageCode)),
      switchMap(() => this.sv.list()))
      .subscribe((languages: Language[]) => {
        this.languages = languages
        this.resetSerch = ''
        this.ms.success('message.STD00016');
      })
  }
}