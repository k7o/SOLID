import { HttpClient } from '@angular/common/http';
import { Component, Injectable } from '@angular/core';
import { ICommand } from './command'
import { ICommandHandler } from './command.handler';
import { environment } from '../../environments/environment'

@Injectable()
export class CommandService<TCommand extends ICommand> implements ICommandHandler<TCommand> {

    private _commandUrl = environment.commandurl;

    constructor(private _http: HttpClient) { }

    public handle(command: TCommand): void {
        let action = command.constructor.name.replace('Command', '');
        this._http.post<TCommand>(this._commandUrl + action, command)
            .subscribe(data => {
                console.log(data)
            });
    }
}
