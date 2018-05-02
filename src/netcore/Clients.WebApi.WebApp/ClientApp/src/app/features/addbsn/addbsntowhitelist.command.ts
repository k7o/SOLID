import { ICommand } from '../../contracts/command'

export class AddBsnToWhitelistCommand implements ICommand {
    bsnnummer: number;
}