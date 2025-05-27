import { Component } from '@angular/core';
import { ContentDTO, Surt07Service } from './surt07.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'x-surt07',
  templateUrl: './surt07.component.html',
  styles: ``
})
export class Surt07Component {
  contents: ContentDTO[] = []
  constructor(
    private sv: Surt07Service,
    private activatedRoute: ActivatedRoute) {
    this.activatedRoute.data.subscribe(({ contents }) => this.contents = contents)
  }

  search(value: string = '') {
    this.sv.list(value).subscribe((contents: ContentDTO[]) => this.contents = contents)
  }

  view(url: string) {
    window.open(url, "_blank")
  }
}
