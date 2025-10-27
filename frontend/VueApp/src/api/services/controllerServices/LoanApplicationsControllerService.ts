import type { LoanStatusEnum } from "@/classes/enums/LoanStatusEnum";
import type { ILoanApplication } from "@/classes/POCOs/LoanApplication";
import axios from "axios";

export async function get(params: IGetLoanApplicationsParams = {}): Promise<IGetResponse> {
    const response = await axios.post("/api/loanapplications/get", params);
    return response.data;
}

export async function add(item: Pick<ILoanApplication, "amount" | "termValue" | "interestValue">) {
    return await axios.post("/api/loanapplications/add", item);
}

export async function editStatus(item: Pick<ILoanApplication, "id" | "status">) {
    return await axios.post("/api/loanapplications/edit/status", item);
}

export interface IGetLoanApplicationsParams {
    ids?: number[] | null;
    status?: LoanStatusEnum[] | null;
    minAmount?: number | null;
    maxAmount?: number | null;
    minTermValue?: number | null;
    maxTermValue?: number | null;
}

export interface IGetResponse {
    loanApplications: Pick<ILoanApplication, "id" | "status" | "number" | "amount" | "termValue" | "interestValue" | "createdAt" | "modifiedAt">[]
}