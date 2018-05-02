import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BsnResult } from './bsnresult'
import { QueryService } from '../../contracts/query.service';
import { GetWhitelistedBsnsQuery } from './getwhitelistedbsns.query'

@Component({
  selector: 'app-listbsns',
  templateUrl: './listbsns.component.html',
  styleUrls: ['./listbsns.component.css']

})
export class ListBsnsComponent implements OnInit {

  constructor(private _queryService: QueryService<GetWhitelistedBsnsQuery, BsnResult[]>) {
  }

  bsns : BsnResult[]

  ngOnInit() {
    
    this._queryService.handle(new GetWhitelistedBsnsQuery())
          .subscribe(data => {
              this.bsns = data;
            })
  }

}
