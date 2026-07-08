import { Component } from '@angular/core';
import { LogListComponent } from './components/log-list/log-list';
import { LogFormComponent } from './components/log-form/log-form';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [LogListComponent, LogFormComponent],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {}
