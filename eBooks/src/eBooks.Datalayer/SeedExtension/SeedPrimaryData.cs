#region MyRegion

//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using eBooks.Domain.Entities.Checks;
//using eBooks.Domain.Entities.Identity;

//namespace eBooks.Datalayer.SeedExtension
//{
//    public static class SeedPrimaryData
//    {
//        public static void SeedBasicInformation(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<User>().HasData(new User()
//            {
//                Id = Guid.Parse("75D1AF4B-E9D0-44E9-957C-F3B5E023B789"),
//                Name = "مدیریت",
//                Family = "سایت",
//                CreateDateTime = DateTime.Now,
//                IsDelete = false,
//                IsConstantData = true,
//                UserName = "2850866660",
//                NormalizedUserName = "2850866660",
//                Email = "hpoudat@yahoo.com",
//                NormalizedEmail = "HPOUDAT@YAHOO.COM",
//                PasswordHash = new PasswordHasher<object>().HashPassword(null, "Aa123456789"),
//                SecurityStamp = Guid.NewGuid().ToString(),
//                PhoneNumber = "00123456789",
//                PhoneNumberConfirmed = true
//            });

//            modelBuilder.Entity<Role>().HasData(new Role()
//            {
//                Id = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                Name = "مدیریت",
//                NormalizedName = "مدیریت",
//                CreateDateTime = DateTime.Now,
//                IsConstantData = true
//            });

//            modelBuilder.Entity<UserRole>().HasData(new UserRole()
//            {
//                UserId = Guid.Parse("75D1AF4B-E9D0-44E9-957C-F3B5E023B789"),
//                RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814")
//            });

//            modelBuilder.Entity<Permission>().HasData(new List<Permission>()
//            {
//                #region Management

//                new Permission
//                {
//                    Id = Guid.Parse("640c1aca-f52c-4e66-8a8b-c9a575304bf2"),
//                    Name = "AccessMenuManagement",
//                    Description = "منوی مدیریت ",
//                },

//                #endregion Management

//                #region Manage Users

//                new Permission
//                {
//                    Id = Guid.Parse("0302A02D-0EB3-4082-9FFA-50D6CD606D8C"),
//                    Name = "AccessManageUsers",
//                    Description = "مشاهده کاربران",
//                    ParentId =Guid.Parse("640c1aca-f52c-4e66-8a8b-c9a575304bf2")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("8558DAF3-4397-4B7F-992A-891A503EF5A7"),
//                    Name = "AccessAddUser",
//                    Description = "افزودن کاربر",
//                    ParentId =Guid.Parse("0302A02D-0EB3-4082-9FFA-50D6CD606D8C")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("84A4D18B-CE1F-4D79-B604-F65DD9AE4709"),
//                    Name = "AccessEditUser",
//                    Description = "ویرایش کاربر",
//                    ParentId =Guid.Parse("0302A02D-0EB3-4082-9FFA-50D6CD606D8C")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("433CCE34-517B-4668-AE3D-C614CA9F0034"),
//                    Name = "AccessDeleteUser",
//                    Description = "حذف کاربر",
//                    ParentId =Guid.Parse("0302A02D-0EB3-4082-9FFA-50D6CD606D8C")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("13228C3A-B313-4927-86E7-EF5B4609FB89"),
//                    Name = "AccessReactivateUser",
//                    Description = "بازآوردن کاربر",
//                    ParentId =Guid.Parse("0302A02D-0EB3-4082-9FFA-50D6CD606D8C")
//                },

//                #endregion Manage Users

//                #region Manage Roles

//                new Permission
//                {
//                    Id = Guid.Parse("E1ECD91A-4657-4897-B983-36D1900AE556"),
//                    Name = "AccessManageRoles",
//                    Description = "مشاهده گروه کاربران",
//                    ParentId =Guid.Parse("640c1aca-f52c-4e66-8a8b-c9a575304bf2")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("A7533856-1E93-4B55-91F9-3A25437B9EE9"),
//                    Name = "AccessAddRole",
//                    Description = "افزودن گروه کاربران",
//                    ParentId = Guid.Parse("E1ECD91A-4657-4897-B983-36D1900AE556")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("EA72670E-6FD7-4871-A690-AF79765AAAC3"),
//                    Name = "AccessEditRole",
//                    Description = "ویرایش گروه کاربران",
//                    ParentId = Guid.Parse("E1ECD91A-4657-4897-B983-36D1900AE556")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("82979160-A467-4B24-BC0E-96CEDDD353F2"),
//                    Name = "AccessDeleteRole",
//                    Description = "حذف گروه کاربران",
//                    ParentId = Guid.Parse("E1ECD91A-4657-4897-B983-36D1900AE556")
//                },

//                #endregion Manage Roles

//                #region Manage Slider

//                new Permission
//                {
//                    Id = Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22"),
//                    Name = "AccessManageSlider",
//                    Description = "مشاهده اسلایدر",
//                    ParentId =Guid.Parse("640c1aca-f52c-4e66-8a8b-c9a575304bf2")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("3eec4578-4d90-4104-8032-f2b83ae1cc7e"),
//                    Name = "AccessAddSlider",
//                    Description = "افزودن  اسلایدر",
//                    ParentId =Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("ca75edd0-f701-4674-a7b5-ad66861693c4"),
//                    Name = "AccessEditSlider",
//                    Description = "ویرایش  اسلایدر",
//                    ParentId =Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("24fdc20a-a0c9-4fe8-a816-35a6f106865b"),
//                    Name = "AccessDeleteSlider",
//                    Description = "حذف اسلایدر",
//                    ParentId =Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("5e40f0c6-fcda-4166-ade2-b6da55e3cfbc"),
//                    Name = "AccessActivateSlider",
//                    Description = "فعال سازی اسلایدر",
//                    ParentId =Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22")
//                },

//                new Permission
//                {
//                    Id = Guid.Parse("bb1d45c9-a650-4c9a-ac40-f08a826fa1a4"),
//                    Name = "AccessDeActivateSlider",
//                    Description = "غیر فعال سازی اسلایدر",
//                    ParentId =Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22")
//                },

//                #endregion Manage Slider

//                #region Manage ContactUs

//                new Permission
//                {
//                    Id = Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e"),
//                    Name = "AccessManageContactUs",
//                    Description = "مشاهده تماس با ما",
//                    ParentId =Guid.Parse("640c1aca-f52c-4e66-8a8b-c9a575304bf2")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("a7b68cb4-5b28-4552-b7e6-c28710e5809f"),
//                    Name = "AccessAddContactUs",
//                    Description = "افزودن  تماس با ما",
//                    ParentId =Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("7d1ccaee-9a8e-43f2-ac14-02e8bfa3d79b"),
//                    Name = "AccessEditContactUs",
//                    Description = "ویرایش  تماس با ما",
//                    ParentId =Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("82021fdd-c80e-4d70-8fe0-649c88e145dc"),
//                    Name = "AccessDeleteContactUs",
//                    Description = "حذف  تماس با ما",
//                    ParentId =Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("7d975cd1-5c9e-4882-9552-6cf149633b49"),
//                    Name = "AccessActivateContactUs",
//                    Description = "فعال سازی تماس با ما",
//                    ParentId =Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e")
//                },
//                new Permission
//                {
//                    Id = Guid.Parse("7e332248-8a55-4e83-bd6e-fa5bd128be93"),
//                    Name = "AccessDeactivateContactUs",
//                    Description = "غیر فعال سازی تماس با ما",
//                    ParentId =Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e")
//                },

//                #endregion Manage ContactUs
//            });

//            modelBuilder.Entity<PermissionRole>().HasData(new List<PermissionRole>
//            {
//                #region Management

//                new PermissionRole()
//                {
//                    RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                    PermissionId = Guid.Parse("640c1aca-f52c-4e66-8a8b-c9a575304bf2")
//                },

//                #endregion Management

//                #region Users

//                new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("0302A02D-0EB3-4082-9FFA-50D6CD606D8C")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("8558DAF3-4397-4B7F-992A-891A503EF5A7")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("84A4D18B-CE1F-4D79-B604-F65DD9AE4709")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("433CCE34-517B-4668-AE3D-C614CA9F0034")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("13228C3A-B313-4927-86E7-EF5B4609FB89")
//               },

//                #endregion Users

//                #region Roles

//                new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("E1ECD91A-4657-4897-B983-36D1900AE556")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("A7533856-1E93-4B55-91F9-3A25437B9EE9")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("EA72670E-6FD7-4871-A690-AF79765AAAC3")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("82979160-A467-4B24-BC0E-96CEDDD353F2")
//               },

//                #endregion Roles

//               #region Slider

//                new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("487343ae-89e0-42ed-a164-78ba78a5ad22")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("3eec4578-4d90-4104-8032-f2b83ae1cc7e")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("ca75edd0-f701-4674-a7b5-ad66861693c4")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("24fdc20a-a0c9-4fe8-a816-35a6f106865b")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("5e40f0c6-fcda-4166-ade2-b6da55e3cfbc")
//               },

//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("bb1d45c9-a650-4c9a-ac40-f08a826fa1a4")
//               },

//                #endregion Slider

//               #region ContactUs

//                new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("73062b74-4c8d-4265-8050-5c28d382d77e")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("a7b68cb4-5b28-4552-b7e6-c28710e5809f")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("7d1ccaee-9a8e-43f2-ac14-02e8bfa3d79b")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("82021fdd-c80e-4d70-8fe0-649c88e145dc")
//               },
//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("7d975cd1-5c9e-4882-9552-6cf149633b49")
//               },

//               new PermissionRole()
//               {
//                   RoleId = Guid.Parse("4E05645C-4C62-4BE7-A55B-3674D3FA3814"),
//                   PermissionId = Guid.Parse("7e332248-8a55-4e83-bd6e-fa5bd128be93")
//               },

//               #endregion ContactUs
//            });
//        }
//    }
//}

#endregion

using eBooks.DataLayer.Context;
using eBooks.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace eBooks.Datalayer.SeedExtension
{
    public static class DbInitializer
    {
        public static void Seed(this BestDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Roles.Any()) return;

            var adminRole = new Role()
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Manager",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                IsConstantData = true
            };
            using var transaction = context.Database.BeginTransaction();
            context.Roles.Add(adminRole);
            context.SaveChanges();

            var adminUser = new User()
            {
                Name = "admin",
                Family = "  ",
                UserName = "1122009988",
                NormalizedUserName = "1122009988",
                PhoneNumber = "09144444444",
                PhoneNumberConfirmed = true,
                PasswordHash = new PasswordHasher<object>().HashPassword(null, "Admin1377"),
                SecurityStamp = Guid.NewGuid().ToString(),
                IsConstantData = true,
            };

            context.Users.Add(adminUser);
            context.SaveChanges();

            var useRole = new UserRole
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id,
            };
            context.UserRoles.Add(useRole);
            context.SaveChanges();
            transaction.Commit();
        }
    }
}