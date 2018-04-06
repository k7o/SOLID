import { Component } from '@angular/core';
import { AddBsnService } from './addbsn/addbsn.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],

  providers: [AddBsnService]
})
export class AppComponent {
  title = 'app';
}
