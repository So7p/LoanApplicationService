using System.ComponentModel;

namespace LoanApplicationService.Domain.POCOs.Enums
{
    public enum LoanStatusEnum : byte
    {
        [Description("Опубликована")]
        Published = 1,

        [Description("Снята с публикации")]
        Unpublished = 2,
    }
}
