import { IQuery } from '../contracts/query'
import { IResult } from '../contracts/result'
import { BsnResult } from './bsnresult';

export class GetAllBsnsQuery implements IQuery<Array<BsnResult>>
{
}