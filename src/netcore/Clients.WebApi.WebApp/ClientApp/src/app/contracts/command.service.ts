import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { ICommand } from './command'

@Injectable()
export class CommandService<TCommand extends ICommand> {

    private _commandUrl = 'http://localhost:59922/api/command/';

    constructor(private _http: HttpClient) { }

    public execute(command: TCommand, action: string): void {

        this._http.post<TCommand>(this._commandUrl + action, command)
            .subscribe(data => {
                console.log(data)
            });
    }
}
