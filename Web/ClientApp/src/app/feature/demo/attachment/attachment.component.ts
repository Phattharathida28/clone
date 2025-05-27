import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { FileType } from '@app/shared/types/data.types';

@Component({
  selector: 'x-attachment',
  templateUrl: './attachment.component.html',
  styles: ``
})
export class AttachmentComponent {
  imageOnly: FileType = FileType.ALL_IMAGE
  application: FileType[] = [FileType.DOCX, FileType.XLSX, FileType.PDF, FileType.PPTX]
  form: FormGroup = new FormGroup({
    all: new FormControl(),
    image: new FormControl(),
    application: new FormControl()
  })
}
