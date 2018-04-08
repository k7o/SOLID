import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { IQuery, IResult } from './query'
import { BsnResult } from '../listbsns/bsnresult';

@Injectable()
export class QueryService<TResult extends IResult>  {

    private _queryUrl = 'http://localhost:59922/api/query/';

    constructor(private _http: HttpClient) { }

    public execute(query: IQuery<TResult>, action: string) : Observable<TResult> {

        return this._http.get<TResult>(this._queryUrl + action);
    }
}
