import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'modal',
  templateUrl: './modal.component.html'
})
export class ModalComponent implements OnInit {
  @Input() header: string = ''
  @Input() maximizable: boolean = false
  @Input() visible: boolean = false
  @Input() size: number = 20
  @Input() closable: boolean = true
  @Output() onClose = new EventEmitter<void>();

  style: { width: string } = { width: "" }

  ngOnInit(): void {
    this.style = { width: `${this.size}vw` }
  }

  close() {
    this.onClose.emit();
  }
}
