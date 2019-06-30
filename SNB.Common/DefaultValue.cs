using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNB.Common
{
    public static class DefaultValue 
    {
        public static string CompanyFullName => "Shopno Nibash";
        public static string CompanyShortName => "SNB";
        public static string DeveloperCompanyFullName => "Codenovo Private Limited";
        public static string DeveloperCompanyShortName => "Codenovo";
        public static string UserPassword => "12345";
        public static string UserResetPassword => "12345";
        public static UserType UserType => new UserType();
        public static Role Role => new Role();
    }

    public class UserType
    {
        public string Tenant => "Tenant";
        public string Landlord => "Landlord";
    }

    public class Role
    {
        public string Tenant => "Tenant";
        public string Landlord => "Landlord";
    }
}
