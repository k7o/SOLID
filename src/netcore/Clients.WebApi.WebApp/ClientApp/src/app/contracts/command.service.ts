import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { ICommand } from './command'

@Injectable()
export class CommandService {

    private _commandUrl = 'http://localhost:59922/api/command/';

    constructor(private _http: HttpClient) { }

    public execute(command: ICommand, action: string): void {

        this._http.post(this._commandUrl + action, command) // + "?BsnNummer=" + addBsnQuery.bsnnummer)
            .subscribe(data => {
                console.log(data)
            });
    }
}
