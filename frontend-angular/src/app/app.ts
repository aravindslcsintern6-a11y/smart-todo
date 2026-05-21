import { Component } from '@angular/core';
import { Todo } from './components/todo/todo';
import { Network } from './components/network/network';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Todo, Network],
  template: `
  <app-todo></app-todo>
  <hr>
  <app-network></app-network>
  `
})

export class App {}