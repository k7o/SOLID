import { Component } from '@angular/core';
import { ICommand } from '../contracts/command';
import { AddBsn } from './addbsncommand'
import { CommandService } from '../contracts/command.service';

@Component({
    selector: 'addbsn',
    templateUrl: './addbsn.component.html'
})
export class AddBsnComponent {

    constructor(private _commandService: CommandService) {
    }

    model: AddBsn = new AddBsn();

    submitted = false;

    onSubmit() {

        this.submitted = true;
        this._commandService.execute(this.model, "AddBsn");
    }
}
