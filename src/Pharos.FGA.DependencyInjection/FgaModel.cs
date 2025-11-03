namespace Pharos.FGA.DependencyInjection;

public static class FgaModel
{
    public static class User
    {
        public const string ObjectName = "user";
    }

    public static class Platform
    {
        public const string ObjectName = "platform";
        public const string ActualObject = "platform:pharos";

        public static class Relations
        {
            public const string Employee = "employee";
            public const string God = "god";
            public const string Admin = "admin";
            public const string Support = "support";

            public const string CanManageInstitutions = "can_manage_institutions";
            public const string CanManageBilling = "can_manage_billing";
            public const string CanResetPasswords = "can_reset_passwords";
            public const string CanManageStaff = "can_manage_staff";
        }
    }

    public static class Institution
    {
        public const string ObjectName = "institution";

        public static class Relations
        {
            public const string Platform = "platform";
            public const string Member = "member";
            public const string Admin = "admin";
            public const string Rector = "rector";
            public const string Prorector = "prorector";
            public const string Support = "support";
            public const string Security = "security";
            public const string Faculty = "faculty";
            public const string Viewer = "viewer";

            public const string CanManageFaculty = "can_manage_faculty";
            public const string CanManageStaff = "can_manage_staff";
        }
    }

    public static class Faculty
    {
        public const string ObjectName = "faculty";

        public static class Relations
        {
            public const string Admin = "admin";
            public const string Institution = "institution";
            public const string Methodist = "methodist";
        }
    }

    public static class Department
    {
        public const string ObjectName = "department";

        public static class Relations
        {
            public const string Faculty = "faculty";
            public const string Admin = "admin";
            public const string ProfOrientator = "proforientator";
            public const string Methodist = "methodist";
            public const string Teacher = "teacher";
            public const string Viewer = "viewer";
        }
    }
}
