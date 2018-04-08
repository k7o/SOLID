import { ICommand } from '../contracts/command'

export class AddBsnCommand implements ICommand {
    bsnnummer: number;
}