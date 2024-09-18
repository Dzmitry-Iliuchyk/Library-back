using AutoMapper;
using Library.DataAccess.AutoMapper;
using Library.DataAccess.DataBase.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Tests {
    public static class Helpers {

        public static IMapper ConfigureMapper() {
            var mapperExpression = new MapperConfigurationExpression();
            mapperExpression.AddProfile( new DataBaseMapping() );
            var config = new MapperConfiguration( mapperExpression );
            return config.CreateMapper();
        }
        public static AuthorizationOptions AuthOptions() {
            return new AuthorizationOptions() {
                AccessGroupPermission = [ new DataBase.Configuration.AccessGroupPermission() {
                Group = "Admin",
                Permissions = [ "Create", "Delete", "Update", "Read" ] },
                 new DataBase.Configuration.AccessGroupPermission() {
                Group = "User",
                Permissions = [ "Read" ] }]
            };
        }
    }
}
