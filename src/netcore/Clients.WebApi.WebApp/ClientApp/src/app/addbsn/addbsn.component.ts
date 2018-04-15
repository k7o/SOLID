import { Component } from '@angular/core';
import { ICommand } from '../contracts/command';
import { AddBsnCommand } from './addbsn.command'
import { CommandService } from '../contracts/command.service';


@Component({
    selector: 'addbsn',
    templateUrl: './addbsn.component.html'
})
export class AddBsnComponent {

    constructor(private _commandService: CommandService<AddBsnCommand>) {
    }

    model: AddBsnCommand = new AddBsnCommand();

    submitted = false;

    onSubmit() {
        
        this._commandService.handle(this.model);
        this.submitted = true;
    }
}
