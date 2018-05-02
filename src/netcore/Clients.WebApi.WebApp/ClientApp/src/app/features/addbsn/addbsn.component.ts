import { Component } from '@angular/core';
import { ICommand } from '../../contracts/command';
import { AddBsnToWhitelistCommand } from './addbsntowhitelist.command'
import { CommandService } from '../../contracts/command.service';


@Component({
    selector: 'addbsn',
    templateUrl: './addbsn.component.html'
})
export class AddBsnComponent {

    constructor(private _commandService: CommandService<AddBsnToWhitelistCommand>) {
    }

    model: AddBsnToWhitelistCommand = new AddBsnToWhitelistCommand();

    submitted = false;

    onSubmit() {
        
        this._commandService.handle(this.model);
        this.submitted = true;
    }
}
