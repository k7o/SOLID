import { Component } from '@angular/core';
import { CommandService } from './contracts/command.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

  providers: [CommandService]
})
export class AppComponent {
  title = 'app';
}
