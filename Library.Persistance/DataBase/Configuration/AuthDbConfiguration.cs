using Library.Application.Auth.Enums;
using Library.DataAccess.DataBase.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Library.DataAccess.DataBase.Configuration {
    public static class AuthDbConfiguration {
        public static void Configure( ModelBuilder modelBuilder, AuthorizationOptions authOptions ) {
            modelBuilder
                 .Entity<UserEntity>()
                 .HasMany( t => t.Groups )
                 .WithMany( t => t.Users )
                 .UsingEntity<UserAccessGroup>(
                     l => l.HasOne( e => e.Group ).WithMany( e => e.UserGroups ).HasForeignKey( e => e.GroupId ),
                     r => r.HasOne( e => e.User ).WithMany( e => e.UserGroups ).HasForeignKey( e => e.UserId ),
                     j => {
                         j.HasKey( x => new { x.GroupId, x.UserId } );
                         j.Property( e => e.CreatedOn ).HasDefaultValueSql( "CURRENT_TIMESTAMP" );
                     } );
            modelBuilder
                .Entity<AccessGroupEntity>()
                .HasMany( t => t.Permissions )
                .WithMany( t => t.Groups )
                .UsingEntity<Entities.AccessGroupPermission>(
                    l => l.HasOne( e => e.Permission ).WithMany( e => e.GroupPermissions ).HasForeignKey( e => e.PermissionId ),
                    r => r.HasOne( e => e.Group ).WithMany( e => e.GroupPermissions ).HasForeignKey( e => e.GroupId ),
                    j => {
                        j.HasKey( x => new { x.GroupId, x.PermissionId } );
                        j.Property( e => e.CreatedOn ).HasDefaultValueSql( "CURRENT_TIMESTAMP" );
                    } );

            var permissions = Enum
            .GetValues<PermissionEnum>()
                .Select( x => new PermissionEntity {
                    Id = (int)x,
                    Name = x.ToString(),
                } );
            modelBuilder.Entity<PermissionEntity>().HasData( permissions );

            var groups = Enum
            .GetValues<AccessGroupEnum>()
                .Select( x => new AccessGroupEntity {
                    Id = (int)x,
                    Name = x.ToString(),
                } );
            modelBuilder.Entity<AccessGroupEntity>().HasData( groups );

            var groupPermissions = authOptions.AccessGroupPermission.SelectMany( gp => gp.Permissions
                .Select( p => new Entities.AccessGroupPermission {
                    GroupId = (int)Enum.Parse<AccessGroupEnum>( gp.Group ),
                    PermissionId = (int)Enum.Parse<PermissionEnum>( p )
                } ) )
            .ToArray();

            modelBuilder.Entity<Entities.AccessGroupPermission>().HasData( groupPermissions );

            
        }
    }
}
