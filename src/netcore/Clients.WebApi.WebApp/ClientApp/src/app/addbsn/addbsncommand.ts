import { ICommand } from '../contracts/command'

export class AddBsn implements ICommand {
    bsnnummer: number;
}