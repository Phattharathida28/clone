import { Component } from '@angular/core';

@Component({
  selector: 'x-binding',
  templateUrl: './binding.component.html',
  styleUrl: './binding.component.scss'
})
export class BindingComponent {
  interpolation : string = 'interpolation';
  data:string = 'property binding'
  color:string = '';
  state:boolean = false;
  twoway:string= '';
}
