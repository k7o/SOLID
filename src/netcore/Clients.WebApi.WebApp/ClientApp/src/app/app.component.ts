import { Component } from '@angular/core';
import { CommandService } from './contracts/command.service'
import { QueryService } from './contracts/query.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

  providers: [CommandService, QueryService]
})
export class AppComponent {
  title = 'app';
}
