import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Observable, zip, switchMap, map } from 'rxjs';
import { ModalConfig } from '@app/shared/types/data.types';
import { ConfirmComponent } from '../confirm/confirm.component';

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  private modal: DynamicDialogRef;

  constructor(private ds: DialogService, private translate: TranslateService) { }

  confirm(message: string) {
    return this.translate.get("label.ALL.PleaseConfirm").pipe(
      switchMap((translate) => {
        this.modal = this.ds.open(ConfirmComponent, {
          data: { message },
          header: translate,
          baseZIndex: 10000,
          maximizable: false,
          position: "top",
          width: `380px`
        })

        return this.modal.onClose
      })
    )
  }

  openComponent(config: ModalConfig): Observable<any> {
    if (config.resolver) {
      return zip(config.resolver.resolve(config.resolverParam), this.translate.get(config.header)).pipe(
        map((res) => ({ resolved: res[0], header: res[1] })),
        switchMap(({ header, resolved }) => {
          this.modal = this.ds.open(config.content, {
            data: { ...{ param: config.data }, ...{ resolved } },
            header: header,
            width: `${config.size}%`,
            baseZIndex: config.baseZIndex || 10000,
            maximizable: config.maximizable ?? false,
            closable: config.closable ?? true
          });

          return this.modal.onClose;
        })
      )
    }
    else {
      return this.translate.get(config.header).pipe(
        switchMap((translated) => {
          this.modal = this.ds.open(config.content, {
            data: config.data,
            header: translated,
            width: `${config.size}%`,
            baseZIndex: config.baseZIndex || 10000,
            maximizable: config.maximizable ?? false,
            closable: config.closable ?? true
          });

          return this.modal.onClose;
        })
      )
    }
  }
}
