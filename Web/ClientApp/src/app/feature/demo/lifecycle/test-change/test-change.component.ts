import { AfterContentChecked, AfterContentInit, AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, DoCheck, Input, OnChanges, OnDestroy, OnInit, SimpleChanges} from '@angular/core';

@Component({
  selector: 'x-test-change',
  templateUrl: './test-change.component.html',
  styleUrl: './test-change.component.scss'
})
export class TestChangeComponent implements OnInit, OnChanges , DoCheck , AfterContentChecked , AfterViewChecked , AfterContentInit , AfterViewInit , OnDestroy{
  lifeCycleArr : string[] = [];
  onChangeValue:string = '';
  docheckState : boolean = false;
  afterContentCheckedState : boolean = false;
  afterViewCheckedState : boolean = false;
  @Input() inputData:string = '';

  constructor(private cd: ChangeDetectorRef){}

  ngOnInit(): void {
    this.lifeCycleArr.push("ngOnInit");
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.lifeCycleArr.push('ngOnChanges' + '   value => ' +changes['inputData']?.currentValue);
  }

  receivedChanges(event:string): void {
    this.lifeCycleArr.push(event);
  }

  ngDoCheck(): void {
    if(this.lifeCycleArr.filter(f => f == "ngDoCheck")?.length == 0 || this.docheckState) {
        this.lifeCycleArr.push("ngDoCheck")
    }
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.lifeCycleArr.push("ngAfterViewInit")
    } , 1000)
  }

  ngAfterContentInit(): void {
    this.lifeCycleArr.push("ngAfterContentInit")
  }

  ngAfterViewChecked(): void {
    // let x;
    // if(this.lifeCycleArr.filter(f => f == "ngAfterViewChecked")?.length == 0 || this.afterViewCheckedState){
    //     x = setTimeout(() => {
    //       this.lifeCycleArr.push("ngAfterViewChecked")
    //     } , 1000)
    // }else{
    //   x = clearTimeout(x);
    // }
  }

  ngAfterContentChecked(): void {
    if(this.lifeCycleArr.filter(f => f == "ngAfterContentChecked")?.length == 0 || this.afterContentCheckedState) {
      this.lifeCycleArr.push("ngAfterContentChecked")
    }
  }

  ngOnDestroy(): void {
     this.lifeCycleArr.push('ngOnDestroy')
  }

  destroyPage(){
    this.lifeCycleArr.push('ngOnDestroy')
    window.location.reload();
  }

  reset(){
    window.location.reload();
  }
}
