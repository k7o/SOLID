import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { IQuery } from './query'

@Injectable()
export class QueryService {

    private _queryUrl = 'http://localhost:59922/api/query/';

    constructor(private _http: HttpClient) { }

    public execute(query: IQuery, action: string): void {

        this._http.get(this._queryUrl + action)
            .subscribe(data => {
                console.log(data)
            });
    }
}
