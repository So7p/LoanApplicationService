import { LoanStatusEnum } from "@/classes/enums/LoanStatusEnum"

export function getLoanStatusLabel(status: LoanStatusEnum): string {
  switch (status) {
    case LoanStatusEnum.Published:
      return 'Опубликована'
    case LoanStatusEnum.Unpublished:
      return 'Снята с публикации'
  }
}