import type { ILoanApplication } from "@/classes/POCOs/LoanApplication";
import { add, editStatus, get, type IGetLoanApplicationsParams } from "../controllerServices/LoanApplicationsControllerService";
import { getLoanStatusLabel } from "@/helpers/LoanStatusEnumHelper";
import type { LoanStatusEnum } from "@/classes/enums/LoanStatusEnum";

export async function getLoanApplications(params?: IGetLoanApplicationsParams) {
    const data = await get(params || {});

    return data.loanApplications
        .map(item => new LoanApplicationsDirectiryGridDataItem(
            item.id,
            item.status,
            getLoanStatusLabel(item.status),
            item.number,
            item.amount,
            item.termValue,
            item.interestValue,
            new Date(item.createdAt).toLocaleString("ru-RU", {
                    day: "2-digit",
                    month: "2-digit",
                    year: "numeric",
                    hour: "2-digit",
                    minute: "2-digit",
                    second: "2-digit"
                }),
            new Date(item.modifiedAt).toLocaleString("ru-RU", {
                    day: "2-digit",
                    month: "2-digit",
                    year: "numeric",
                    hour: "2-digit",
                    minute: "2-digit",
                    second: "2-digit"
                })
        ));
}

export async function createLoanApplication(item: Pick<ILoanApplication, "amount" | "termValue" | "interestValue">) {
    return await add(item);
}

export async function editLoanApplicationStatus(item: Pick<ILoanApplication, "id" | "status">) {
    return await editStatus(item);
}

export class LoanApplicationsDirectiryGridDataItem implements Pick<ILoanApplication, "id" | "status" | "number" | "amount" | "termValue" | "interestValue"> {
    constructor(
        public id: string,
        public status: LoanStatusEnum,
        public statusText: string,
        public number: string,
        public amount: number,
        public termValue: number,
        public interestValue: number,
        public createdAt: string,
        public modifiedAt: string 
    ) {}
}