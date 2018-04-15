import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { ICommand } from './command'

@Injectable()
export class CommandService<TCommand extends ICommand> {

    private _commandUrl = 'http://localhost:50384/api/command/';

    constructor(private _http: HttpClient) { }

    public handle(command: TCommand): void {

        let action = command.constructor.name.replace('Command', '');
        this._http.post<TCommand>(this._commandUrl + action, command)
            .subscribe(data => {
                console.log(data)
            });
    }
}
