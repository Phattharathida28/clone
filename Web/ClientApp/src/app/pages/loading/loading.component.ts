import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { LoadingService } from './loading.service';
import { BehaviorSubject } from 'rxjs';
@Component({
  selector: 'x-loading',
  templateUrl: './loading.component.html',
  styleUrls: ['./loading.component.scss'],
  animations: [trigger('fadeAnimation', [
    state('void', style({
      opacity: 0
    })),
    transition('void <=> *', animate(200)),
  ])]
})
export class LoadingComponent implements OnInit {

  isLoading = new BehaviorSubject<boolean>(false);

  constructor(private cd: ChangeDetectorRef, public ls: LoadingService) { }

  ngOnInit() {
    this.ls.isLoading$.subscribe(data => {
      const pre = this.isLoading.getValue();
      this.isLoading.next(data);
      if (pre === false && data === true) {
        this.cd.detectChanges();
      }
    })
  }
}
