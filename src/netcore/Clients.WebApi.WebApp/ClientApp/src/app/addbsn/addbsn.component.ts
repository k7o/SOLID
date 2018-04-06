import { Component } from '@angular/core';
import { IAddBsnQuery, AddBsnQuery } from './addbsnquery'
import { AddBsnService } from './addbsn.service';

@Component({
    selector: 'addbsn',
    templateUrl: './addbsn.component.html'
})
export class AddBsnComponent {

    constructor(private _addBsnService: AddBsnService) {
    }

    model: AddBsnQuery = new AddBsnQuery();

    submitted = false;

    onSubmit() {

        this.submitted = true;
        this._addBsnService.addBsn(this.model);
    }
}
