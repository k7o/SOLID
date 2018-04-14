import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { IQuery} from './query'
import { IResult } from './result'

@Injectable()
export class QueryService<TQuery extends IQuery<TResult>, TResult>  {

    private _queryUrl = 'http://localhost:59922/api/query/';

    constructor(private _http: HttpClient) { }

    public execute(query: TQuery, action: string) : Observable<TResult> {

        return this._http.get<TResult>(this._queryUrl + action);
    }
}
