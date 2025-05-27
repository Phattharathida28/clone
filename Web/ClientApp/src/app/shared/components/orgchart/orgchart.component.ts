import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'orgchart',
  templateUrl: './orgchart.component.html',
  styleUrl: './orgchart.component.scss'
})
export class OrgchartComponent implements OnInit{
  selectedNodes!: TreeNode[];
  dragedEmployee:TreeNode;
  teamForSave = []
  teamForDelete = []
  @Input() data: TreeNode[] = [
//     {
//       expanded: true,
//       type: 'person',
//       data: {
//           image: '../../../../assets/layout/images/man_logo.png',
//           name: 'Anuwat',
//           title: 'Project Manager'
//       },
//       children: [
//           {
//               expanded: true,
//               type: 'person',
//               data: {
//                   image: '../../../../assets/layout/images/man_logo.png',
//                   name: 'Phasit',
//                   title: 'System Analysis'
//               },
//               children: [
//                 {
//                     expanded: true,
//                     type: 'person',
//                     data: {
//                         image: '../../../../assets/layout/images/man_logo.png',
//                         name: 'Nattaphong',
//                         title: 'Software Developer'
//                     },
//                     children: []
//                 },
//                 {
//                     expanded: true,
//                     type: 'person',
//                     data: {
//                         image: '../../../../assets/layout/images/women_logo.png',
//                         name: 'Chayutra',
//                         title: 'Software Developer'
//                     },
//                     children: []
//                 },
//                 {
//                     expanded: true,
//                     type: 'person',
//                     data: {
//                         image: '../../../../assets/layout/images/man_logo.png',
//                         name: 'Nattanon',
//                         title: 'Software Developer'
//                     },
//                     children: []
//                 },
//                 {
//                     expanded: true,
//                     type: 'person',
//                     data: {
//                         image: '../../../../assets/layout/images/man_logo.png',
//                         name: 'Kitsakron',
//                         title: 'Software Developer'
//                     },
//                     children: []
//                 },
//               ]
//           },
//           {
//               expanded: true,
//               type: 'person',
//               data: {
//                   image: '../../../../assets/layout/images/man_logo.png',
//                   name: 'Jakarin',
//                   title: 'System Analysis'
//               },
//               children: [
//                 {
//                     expanded: true,
//                     type: 'person',
//                     data: {
//                         image: '../../../../assets/layout/images/women_logo.png',
//                         name: 'Sangnapha',
//                         title: 'Software Developer'
//                     },
//                     children: [
                        
//                     ]
//                 },
//               ]
//           }
//       ]
//   }
  ]
  @Input() employees:TreeNode[] = [
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/man_logo.png',
        name: 'Anuwat',
        title: 'Project Manager',
        userId : 1,
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/women_logo.png',
        name: 'Sangnapha',
        title: 'Software Developer',
        userId : 2,
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/man_logo.png',
        name: 'Jakarin',
        title: 'System Analysis',
        userId : 3
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/man_logo.png',
        name: 'Nattaphong',
        title: 'Software Developer',
        userId : 4
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/women_logo.png',
        name: 'Chayutra',
        title: 'Software Developer',
        userId : 5
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/man_logo.png',
        name: 'Nattanon',
        title: 'Software Developer',
        userId : 6
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/man_logo.png',
        name: 'Kitsakron',
        title: 'Software Developer',
        userId : 6
      },
      children: [],
    },
    {
      expanded: true,
      type: 'person',
      data: {
        image: '../../../../assets/layout/images/man_logo.png',
        name: 'Phasit',
        title: 'System Analysis',
        userId : 7
      },
      children: [],
    },
  ]

  @Output() getTeam:EventEmitter<TreeNode[]> = new EventEmitter();

ngOnInit(): void {
  this.checkData(this.data)
  // if (this.data.length > 0) this.checkData(this.data)
  // else if (this.employees.length > 0) this.checkData(this.employees)
}

dragStart(event){
    this.dragedEmployee = event
  }

  drop(event:TreeNode = null){
    if(event){
        this.checkData(this.data)
        event.children.unshift(this.dragedEmployee)
        if(this.dragedEmployee != null) this.employees = this.employees.filter(f => f.data.name != this.dragedEmployee.data.name)
    }else if(this.data.length == 0){
        this.data.push(this.dragedEmployee)
        if(this.dragedEmployee != null) this.employees = this.employees.filter(f => f.data.name != this.dragedEmployee.data.name)
    }
    this.changeDataForSave(this.data);
    this.getTeam.emit(this.teamForSave);
    this.dragedEmployee = null;
  }

  changeDataForSave(event:TreeNode[]){
    let data = [...event]
    data.forEach((element) => {
      if(element.children.length > 0){
        element.children.map(m => m['headUser'] = element.data.userId)
        this.changeDataForSave(element.children);
        return;
      }
      else {
        let check = this.teamForSave.filter(f => f.data.name === element.data.name)
        element.children = []
        if(check.length === 0 || this.teamForSave.length === 0) this.teamForSave.unshift(element)
      }
    })
  }

  changeDataForDelete(event:TreeNode[]){
    let data = [...event]
    data.forEach((element) => {
      if(element.children.length > 0){
        element.children.map(m => m['headUser'] = element.data.userId)
        this.teamForDelete.unshift(element)
        this.changeDataForDelete(element.children);
      }else{
        this.teamForDelete.unshift(element)
      }
    })
  }
  
  dropEmployee(){
    this.teamForDelete = []
    let check = this.employees.filter(f => f.data.name == this.dragedEmployee?.data?.name)
    if(check.length == 0 && this.dragedEmployee){
      this.checkEmployees(this.data)
      let data = [this.dragedEmployee]
      if(this.dragedEmployee.children.length > 0) {
        this.dragedEmployee.children.map(m => {
          data.push(m)
        });
      }
      this.employees.push(...data);
      this.changeDataForDelete(this.data);
      this.getTeam.emit(this.teamForDelete);
    }
    this.dragedEmployee = null;
  }
  
  checkData(data){
    data.forEach((element) => {
      if(element?.children?.length > 0) {
        if(element.children.filter(f=> f.data.name === this.dragedEmployee?.data?.name)){
          element.children = element.children.filter(f => f.data.name !== this.dragedEmployee.data.name)
        }
        this.checkData(element.children)
      }
    })
  }

  checkEmployees(data){
    if (this.data[0].data.name === this.dragedEmployee.data.name){
      this.data = []
      return ;
    }
    data.forEach((element) => {
        if(element?.children?.length > 0) {
            if(element.children.filter(f=> f.data.name === this.dragedEmployee?.data?.name)){
                element.children = element.children.filter(f => f.data.name !== this.dragedEmployee.data.name)
            }
            this.checkData(element.children)
        }else{
          this.data.pop();
        }
    })
  }
}
