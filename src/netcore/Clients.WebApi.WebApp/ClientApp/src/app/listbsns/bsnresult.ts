import { IResult } from '../contracts/result'

export interface BsnResult extends IResult {
    bsnnummer: number;
}