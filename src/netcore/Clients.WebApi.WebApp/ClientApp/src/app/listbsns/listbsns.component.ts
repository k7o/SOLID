import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BsnResult } from './bsnresult'
import { QueryService } from '../contracts/query.service';
import { GetAllBsnsQuery } from './getallbsns.query';

@Component({
  selector: 'app-listbsns',
  templateUrl: './listbsns.component.html',
  styleUrls: ['./listbsns.component.css']

})
export class ListBsnsComponent implements OnInit {

  constructor(private _queryService: QueryService<GetAllBsnsQuery, Array<BsnResult>>) {
  }

  bsns : BsnResult[]

  ngOnInit() {
    this._queryService.execute(new GetAllBsnsQuery(), "GetAllBsns")
          .subscribe(data => {
              this.bsns = { ... data };
            })
  }

}
