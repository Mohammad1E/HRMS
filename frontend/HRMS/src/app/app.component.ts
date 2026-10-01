import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf, NgFor, NgClass, NgStyle } from '@angular/common';
import { FormsModule, FormGroup,FormControl,ReactiveFormsModule, Validators } from '@angular/forms';
import {CommonModule} from '@angular/common';

@Component({
  imports: [RouterOutlet, NgIf, NgFor, NgClass, NgStyle, FormsModule, ReactiveFormsModule, CommonModule],
  selector: 'app-root',
  styleUrl: './app.component.css',
  templateUrl: './app.component.html',
})
export class App {


  form = new FormGroup({
    name:new FormControl(null,[Validators.required, Validators.minLength(3)]),
    email:new FormControl(null,[Validators.required, Validators.email]),
    phone:new FormControl(null,[Validators.required, Validators.minLength(9), Validators.maxLength(10)]),
    age:new FormControl(null,[Validators.required, Validators.min(18), Validators.max(60)]),
    courseId:new FormControl(1,[Validators.required]),
  });

  courses = [
    {id:1, name:"Asp.Net"},
    {id:2, name:"Angular"},
    {id:3, name:"Python"},
    {id:4, name:"Java"},
  ];


  resetForm(){
    this.form.reset({
      courseId:1
    });
  }


  submitForm(){
    if(this.form.valid){
      alert(`Welcome ${this.form.value.name} to our course ${this.courses.find(c=>c.id==this.form.value.courseId)?.name}`);
    }
  }

  

  // public name:string="hello, my name is mohammad";
  
  // students =[
  //   {id:0, name:"stu0", mark:39},
  //   {id:1, name:"stu1", mark:63},
  //   {id:2, name:"stu2", mark:54},
  //   {id:3, name:"stu3", mark:79},
  //   {id:4, name:"stu4", mark:29},
  //   {id:5, name:"stu5", mark:49},
  // ]
  

  // temp(){

    
  //   let num : number;
  //   num=3;
  // }

  // name:string="mohammad";

  // currentIndex:number=0;
  // out:boolean=true;
  //  next(){
  //   if (this.currentIndex < this.images.length - 1) {
  //   this.currentIndex++;
  //   this.out=true;
  //   }
  //   else
  //       this.out=false;
  //  }


  //  previous(){
  //   if (this.currentIndex > 0) {this.currentIndex--; this.out=true;}
  //     else
  //       this.out=false;
  //      }

  // images = [
  //   "https://www.pixelstalk.net/wp-content/uploads/2016/08/Beautiful-Bridge-And-Photos.jpg?w=2210&quality=70",
  //   "https://t3.ftcdn.net/jpg/02/70/35/00/360_F_270350073_WO6yQAdptEnAhYKM5GuA9035wbRnVJSr.jpg",
  //   "https://images.pexels.com/photos/26151151/pexels-photo-26151151/free-photo-of-night-sky-filled-with-stars-reflecting-in-the-lake.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500"
  // ];


  // form = new FormGroup({
  //   name:new FormControl('employee'),
  //   email:new FormControl(''),
  //   password:new FormControl(''),
  // });


}
