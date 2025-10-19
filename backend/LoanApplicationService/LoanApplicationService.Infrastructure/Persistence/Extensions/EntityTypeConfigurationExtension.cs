using Microsoft.EntityFrameworkCore;
using LoanApplicationService.Domain.POCOs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoanApplicationService.Infrastructure.Constants;

namespace LoanApplicationService.Infrastructure.Persistence.Extensions
{
    public static class EntityTypeConfigurationExtension
    {
        public static void RegisterLoanApplications<T>(this EntityTypeBuilder<T> config) where T : LoanApplication
        {
            config.ToTable(DbConstants.Tables.LoanApplications, DbConstants.Schemas.loans);

            config.Property(x => x.Id)
                .HasColumnName(DbConstants.Columns.Common.id)
                .IsRequired();

            config.Property(x => x.Status)
                .HasColumnName(DbConstants.Columns.LoanApplications.status)
                .IsRequired();

            config.Property(x => x.Number)
                .HasColumnName(DbConstants.Columns.LoanApplications.number)
                .IsRequired();

            config.Property(x => x.Amount)
                .HasColumnName(DbConstants.Columns.LoanApplications.amount)
                .IsRequired();

            config.Property(x => x.TermValue)
                .HasColumnName(DbConstants.Columns.LoanApplications.term_value)
                .IsRequired();

            config.Property(x => x.InterestValue)
                .HasColumnName(DbConstants.Columns.LoanApplications.interest_value)
                .IsRequired();

            config.Property(x => x.CreatedAt)
                .HasColumnName(DbConstants.Columns.LoanApplications.created_at)
                .IsRequired();

            config.Property(x => x.ModifiedAt)
                .HasColumnName(DbConstants.Columns.LoanApplications.modified_at)
                .IsRequired();
        }
    }
}
