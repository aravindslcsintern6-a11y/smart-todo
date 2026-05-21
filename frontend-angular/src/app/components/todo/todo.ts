import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-todo',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './todo.html',
  styleUrl: './todo.css'
})
export class Todo {

  constructor(private http: HttpClient) {
    this.loadTodos();
  }

  // Backend API URL
  apiUrl = 'http://localhost:5292/api/todo';

  todos: any[] = [];

  newTodo = {
    title: '',
    description: '',
    priority: 'Low',
    dueDate: '',
    completed: false
  };

  // 🔹 GET ALL TODOS FROM BACKEND
  loadTodos() {
    this.http.get<any[]>(this.apiUrl).subscribe(data => {
      this.todos = data;
    });
  }

  // 🔹 ADD TODO TO DATABASE
  addTodo() {
    this.http.post(this.apiUrl, this.newTodo)
      .subscribe(response => {
        console.log('Saved:', response);
        this.loadTodos(); // refresh list
      });

    this.newTodo = {
      title: '',
      description: '',
      priority: 'Low',
      dueDate: '',
      completed: false
    };
  }

  // 🔹 DELETE TODO FROM DATABASE
  deleteTodo(id: number) {
    this.http.delete(`${this.apiUrl}/${id}`)
      .subscribe(() => {
        this.loadTodos();
      });
  }

  // 🔹 UPDATE TODO IN DATABASE
  updateTodo(todo: any) {
    this.http.put(`${this.apiUrl}/${todo.id}`, todo)
      .subscribe(() => {
        console.log('Updated');
      });
  }
}