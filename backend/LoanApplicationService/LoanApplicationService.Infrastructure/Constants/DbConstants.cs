namespace LoanApplicationService.Infrastructure.Constants
{
    public static class DbConstants
    {
        public static class Schemas
        {
            public const string loans = nameof(loans);
        }

        public static class Tables
        {
            public const string LoanApplications = nameof(LoanApplications);
        }

        public static class Columns
        {
            public static class Common
            {
                public const string id = nameof(id);
            }

            public static class LoanApplications
            {
                public const string id = Common.id;
                public const string status = nameof(status);
                public const string number = nameof(number);
                public const string amount = nameof(amount);
                public const string term_value = nameof(term_value);
                public const string interest_value = nameof(interest_value);
                public const string created_at = nameof(created_at);
                public const string modified_at = nameof(modified_at);
            }
        }
    }
}
