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

  tabLinks = [
    { label: 'Home', link: '' },
    { label: 'Counter', link: 'counter' },
    { label: 'Fetch data', link: 'fetch-data' },
    { label: 'Add Bsn', link: 'addbsn' },
    { label: 'List Bsns', link: 'listbsns' },
  ];
  
}
