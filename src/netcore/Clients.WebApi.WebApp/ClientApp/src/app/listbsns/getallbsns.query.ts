import { IQuery } from '../contracts/query'
import { IResult } from '../contracts/query'
import { BsnResult } from './bsnresult';

export class GetAllBsnsQuery implements IQuery<Array<BsnResult>>
{
}