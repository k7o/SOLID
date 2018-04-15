import { ICommand } from "./command";

export interface ICommandHandler<TCommand extends ICommand> {
    handle(command : TCommand) : void
}
