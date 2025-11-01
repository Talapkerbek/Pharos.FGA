namespace Pharos.FGA.DependencyInjection;

public static class FgaModel
{
    public static class Types
    {
        public const string User = "user";
        public const string Platform = "platform";
        public const string Tenant = "tenant";
        public const string University = "university";
        public const string Faculty = "faculty";
    }

    public static string PlatformName = "pharos";
    
    public static class PlatformRelations
    {
        public const string God = "god";
        public const string Admin = "admin";
    }

    public static class TenantRelations
    {
        public const string Admin = "admin";
        public const string Viewer = "viewer";
    }

    public static class UniversityRelations
    {
        public const string Admin = "admin";
        public const string Tenant = "tenant";
        public const string Viewer = "viewer";
    }

    public static class FacultyRelations
    {
        public const string Admin = "admin";
        public const string ProfOrientator = "proforientator";
        public const string University = "university";
        public const string Viewer = "viewer";
    }
}
