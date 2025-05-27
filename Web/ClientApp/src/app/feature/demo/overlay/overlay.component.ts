import { Component } from '@angular/core';
import { ModalService } from '@app/shared/components/modal/modal.service';
import { ModalComponent, ModalResolveComponent } from './modal/modal.component';
import { NotifyService } from '@app/core/services/notify.service';
import { modalResolver } from './modal/modal.resolver';
import { ModalConfig } from '@app/shared/types/data.types';

@Component({
  selector: 'x-overlay',
  templateUrl: './overlay.component.html'
})
export class OverlayComponent {

  display: boolean = false;

  constructor(
    private modalService: ModalService,
    private notify: NotifyService) { }

  confirm1() {
    this.modalService.confirm("Confirmation").subscribe((res) => {
      if (res) this.notify.success("Confirm !😘")
      else this.notify.info("Cancel !😒")
    })
  }

  openModal(resolve: boolean = false) {
    if (resolve) {
      const modalConfig: ModalConfig = {
        content: ModalResolveComponent,
        size: 30,
        header: "Modal Resolve Component",
        data: {
          x: 1
        },
        resolver: modalResolver,
        resolverParam: {
          y: 1
        },
        maximizable: true,
        closable: false
      }
      this.modalService.openComponent(modalConfig).subscribe((res) => {
        this.notify.info(res)
      })
    }
    else {
      const modalConfig: ModalConfig = {
        content: ModalComponent,
        size: 30,
        header: "Modal Component",
        data: {
          x: 1
        }
      }
      this.modalService.openComponent(modalConfig).subscribe((res) => {
        this.notify.info(res)
      })
    }
  }
}
