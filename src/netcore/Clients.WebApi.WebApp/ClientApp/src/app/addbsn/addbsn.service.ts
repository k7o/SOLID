import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { IAddBsnQuery } from './addbsnquery'

@Injectable()
export class AddBsnService {

    private _addBsnUrl = 'http://localhost:51964/api/command/AddBsn';

    constructor(private _http: HttpClient) { }

    public addBsn(addBsnQuery: IAddBsnQuery): void {

        this._http.get(this._addBsnUrl + "?BsnNummer=" + addBsnQuery.bsnnummer);
    }
}
