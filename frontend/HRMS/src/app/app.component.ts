import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf, NgFor, NgClass, NgStyle } from '@angular/common';

@Component({
  imports: [RouterOutlet, NgIf, NgFor, NgClass, NgStyle],
  selector: 'app-root',
  styleUrl: './app.component.css',
  templateUrl: './app.component.html',
})
export class App {
  public name:string="hello, my name is mohammad";
  
  students =[
    {id:0, name:"stu0", mark:39},
    {id:1, name:"stu1", mark:63},
    {id:2, name:"stu2", mark:54},
    {id:3, name:"stu3", mark:79},
    {id:4, name:"stu4", mark:29},
    {id:5, name:"stu5", mark:49},
  ]
  

  temp(){

    
    let num : number;
    num=3;
  }


}
