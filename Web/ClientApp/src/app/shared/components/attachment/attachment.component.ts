import { Component, ElementRef, Input, OnInit, Optional, Self, ViewChild } from '@angular/core';
import { BaseFormField } from '../base-form';
import { filter } from 'rxjs';
import { Content, ContentUploadService } from '../../services/content-upload.service';
import { AttachmentType, Category, FileType } from '@app/shared/types/data.types';
import { NgControl } from '@angular/forms';

@Component({
  selector: 'attachment',
  templateUrl: './attachment.component.html'
})
export class AttachmentComponent extends BaseFormField implements OnInit {

  @Input() category: Category;
  @Input() type: AttachmentType = AttachmentType.File
  @Input() accept: FileType[] | FileType = null
  @Input() helpText: string = "";
  @Input() axis: "horizontal" | "vertical" = "vertical"
  @Input() helpTextClass: string = ''
  fileType: string = ""
  content: Content = { id: null, name: null, path: null } as Content
  @ViewChild("input") inputFile: ElementRef

  constructor(@Optional() @Self() override controlDir: NgControl, public cs: ContentUploadService) {
    super(controlDir)
  }

  ngOnInit(): void {
    this.fileType = Array.isArray(this.accept) ? this.accept.join(', ') : this.accept
  }

  override writeValue(value: number): void {
    if (value) this.cs.getContent(value).subscribe((content: Content) => this.content = content)
    else this.value = value
  }

  view() {
    window.open(this.content.path as string, "_blank")
  }

  add(event: Event) {
    const file: File = event.target["files"][0]
    const fileType: string = file ? file["type"] : null
    const allFileType: string[] = Object.values(FileType).filter(f => f != FileType.ALL_IMAGE).filter(f => f != FileType.ALL_APPLICATION)

    if (allFileType.some(s => s == fileType)) {
      this.cs.upload(file, AttachmentType.File, this.category).pipe(
        filter(f => f["path"])
      ).subscribe((conten: Content) => {
        this.value = conten.id
        this.content = conten
        this.onChange(this.value)
      })
    }
  }

  delete() {
    this.value = null
    this.inputFile.nativeElement.value = null
    this.content = { id: null, name: null, path: null } as Content
    this.onChange(null)
  }
}