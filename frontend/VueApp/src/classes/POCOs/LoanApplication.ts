import type { LoanStatusEnum } from "../enums/LoanStatusEnum";

export interface ILoanApplication {
    id: string,
    status: LoanStatusEnum,
    number: string,
    amount: number,
    termValue: number,
    interestValue: number,
    createdAt: Date,
    modifiedAt: Date
}