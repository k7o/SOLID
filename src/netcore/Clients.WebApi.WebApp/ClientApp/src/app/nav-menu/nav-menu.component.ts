import { Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  tabLinks = [
    { label: 'Home', link: '' },
    { label: 'Counter', link: 'counter' },
    { label: 'Fetch data', link: 'fetch-data' },
    { label: 'Add Bsn', link: 'addbsn' },
    { label: 'List Bsns', link: 'listbsns' },
  ];

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
