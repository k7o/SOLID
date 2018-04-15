import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { IQuery} from './query'
import { IResult } from './result'

import { environment } from '../../environments/environment'

@Injectable()
export class QueryService<TQuery extends IQuery<TResult>, TResult>  {

    private _queryUrl = environment.queryurl;

    constructor(private _http: HttpClient) { }

    public handle(query: TQuery) : Observable<TResult> {

        let action = query.constructor.name.replace('Query', '');
        return this._http.get<TResult>(this._queryUrl + action);
    }
}
