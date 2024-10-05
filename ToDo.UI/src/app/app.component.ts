import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, HttpClientModule, FormsModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'ToDo.UI';

  tasks:any=[];
  newTask = "";

  APIURL="https://localhost:7117/";

  constructor (private http:HttpClient) {}

ngOnInit(){
  this.getTasks();
}

addTask(){
  let body=new FormData();
  body.append("task", this.newTask);
  this.http.post(this.APIURL+"addTask",body).subscribe((res)=>{
    alert(res);
    this.newTask="";
    this.getTasks();
  })
}

  getTasks(){
    this.http.get(this.APIURL+"getTasks").subscribe((res)=>{
    this.tasks = res;
  })
  }

  deleteTask(id:any){  
    let body=new FormData();
    body.append("id", id);
    this.http.post(this.APIURL+"deleteTask",body).subscribe((res)=>{
      alert(res);
      this.getTasks();
  })
  }
  
}
