namespace SNB.Repository.Migrations
{
    using SNB.Common;
    using SNB.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SNB.Repository.SNBDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SNB.Repository.SNBDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            #region role seed
            var seedRoles = new List<UserRole>
            {
                new UserRole {RoleName = "Administrator", Status = (int)EnumUserRoleStatus.Super_Administrator},
                new UserRole {RoleName = "Admin", Status = (int)EnumUserRoleStatus.General_User},
                new UserRole {RoleName = DefaultValue.Role.Tenant, Status = (int)EnumUserRoleStatus.General_User},
                new UserRole {RoleName = DefaultValue.Role.Landlord, Status = (int)EnumUserRoleStatus.General_User},
            };
            seedRoles.ForEach(s => context.UserRoles.AddOrUpdate(r => r.RoleName, s));
            context.SaveChanges();

            var seedRolePermissionList = new List<UserRolePermission>
            {
                new UserRolePermission{RoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Administrator").Id, Permission = "Global_SupAdmin"},

            };
            seedRolePermissionList.ForEach(s => context.UserRolePermissions.AddOrUpdate(u => u.Permission, s));
            context.SaveChanges();
            #endregion

            #region user seed
            var users = new List<User>
            {
                new User { FullName="Administrator",UserName = "admin",Password = "827ccb0eea8a706c4c34a16891f84e7b",RoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Administrator").Id,SupUser = true,LastPassChangeDate = DateTime.Now,PasswordChangedCount=1,Status=(int)EnumUserStatus.Super_Administrator}, //12345
                new User { FullName="Development",UserName = "dev",Password = "827ccb0eea8a706c4c34a16891f84e7b",RoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Administrator").Id,SupUser = true,LastPassChangeDate = DateTime.Now,PasswordChangedCount=1,Status=(int)EnumUserStatus.Super_Administrator}, //12345

            };
            users.ForEach(s => context.Users.AddOrUpdate(u => u.UserName, s));
            context.SaveChanges();
            #endregion

            #region PropertyType seed
            var propertyTypes = new List<PropertyType>
            {
                new PropertyType{ TypeName = "Apartment", IsSeatingType = false },
                new PropertyType{ TypeName = "Hostel", IsSeatingType = true },
                new PropertyType{ TypeName = "Mess", IsSeatingType = true },
            };
            propertyTypes.ForEach(s => context.PropertyTypes.AddOrUpdate(u => u.TypeName, s));
            context.SaveChanges();
            #endregion

            #region SeatingType seed
            //var seatingTypes = new List<SeatingType>
            //{
            //    new SeatingType{ TypeName = "Apartment" },
            //    new SeatingType{ TypeName = "Room" },
            //    new SeatingType{ TypeName = "Seat" },
            //};
            //seatingTypes.ForEach(s => context.SeatingTypes.AddOrUpdate(u => u.TypeName, s));
            //context.SaveChanges();
            #endregion

            #region District seed
            var districts = new List<District>
            {
                new District{ DistrictName = "Dhaka" },
                new District{ DistrictName = "Rajshahi" },
                new District{ DistrictName = "Rangpur" },
            };
            districts.ForEach(s => context.Districts.AddOrUpdate(u => u.DistrictName, s));
            context.SaveChanges();
            #endregion

            #region Area seed
            var areas = new List<Area>
            {
                new Area{ AreaName = "Dhanmondi", DistrictId = context.Districts.FirstOrDefault(x => x.DistrictName == "Dhaka").Id },
                new Area{ AreaName = "Mohakhali", DistrictId = context.Districts.FirstOrDefault(x => x.DistrictName == "Dhaka").Id },
                new Area{ AreaName = "Mirpur", DistrictId = context.Districts.FirstOrDefault(x => x.DistrictName == "Dhaka").Id },
                new Area{ AreaName = "Rangpur City", DistrictId = context.Districts.FirstOrDefault(x => x.DistrictName == "Rangpur").Id },
                new Area{ AreaName = "Rajshahi City", DistrictId = context.Districts.FirstOrDefault(x => x.DistrictName == "Rajshahi").Id },
            };
            areas.ForEach(s => context.Areas.AddOrUpdate(u => u.AreaName, s));
            context.SaveChanges();
            #endregion
        }
    }
}
