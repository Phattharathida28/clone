import { Component } from '@angular/core';
import { NotifyService } from '@app/core/services/notify.service';
import { NotifyPosition } from '@app/shared/types/data.types';

@Component({
  selector: 'x-message',
  templateUrl: './message.component.html',
  styles: ``
})
export class MessageComponent {

  constructor(private notify: NotifyService) { }

  showInfoViaToast(pos?: NotifyPosition) {
    this.notify.info("Earth Rock 🤘", null, pos)
  }

  showWarnViaToast(pos?: NotifyPosition) {
    this.notify.warning("There are unsaved changes ⚠️", null, pos)
  }

  showErrorViaToast(pos?: NotifyPosition) {
    this.notify.error("Validation failed 😡", null, pos)
  }

  showSuccessViaToast(pos?: NotifyPosition) {
    this.notify.success("Message sent ❤️", null, pos)
  }
}
