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
            public const string God = "god";
            public const string Admin = "admin";
        }
    }

    public static class Tenant
    {
        public const string ObjectName = "tenant";

        public static class Relations
        {
            public const string Admin = "admin";
            public const string Viewer = "viewer";
        }
    }

    public static class University
    {
        public const string ObjectName = "university";

        public static class Relations
        {
            public const string Admin = "admin";
            public const string Tenant = "tenant";
            public const string Rector = "rector";
            public const string Prorector = "prorector";
            public const string Support = "support";
            public const string Security = "security";
            public const string Faculty = "faculty";
        }
    }

    public static class Faculty
    {
        public const string ObjectName = "faculty";

        public static class Relations
        {
            public const string Admin = "admin";
            public const string University = "university";
            public const string DeputyAdmin = "deputy_admin";
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