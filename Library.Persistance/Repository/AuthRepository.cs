using AutoMapper;
using Library.Application.Auth.Enums;
using Library.Application.Auth.Interfaces;
using Library.DataAccess.DataBase.Contexts;
using Library.DataAccess.DataBase.Entities;
using Library.DataAccess.Exceptions;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository {
    public class AuthRepository: IAuthRepository {
        private readonly DbSet<AccessGroupEntity> _groups;
        private readonly DbSet<UserEntity> _users;
        private readonly IMapper _mapper;


        public AuthRepository( LibraryDBContext context ) {
            _groups = context.Set<AccessGroupEntity>();
            _users = context.Set<UserEntity>();
        }

        public async Task AddUserToGroup( User user, AccessGroupEnum group ) {
            var searchedGroup = await _groups
                .AsNoTracking()
                .FirstOrDefaultAsync( r => r.Name == group.ToString() )
                ?? throw new GroupNotFountException();
            var userEntity = _mapper.Map<UserEntity>( user );
            searchedGroup.Users?.Add( userEntity );
        }
        public async Task RemoveUserFromGroup( User user, AccessGroupEnum group ) {
            var searchedGroup = await _groups
                .AsNoTracking()
                .FirstOrDefaultAsync( r => r.Name == group.ToString() )
                ?? throw new GroupNotFountException();
            var userEntity = _mapper.Map<UserEntity>( user );
            searchedGroup.Users?.Remove( userEntity );
        }
        public Task<HashSet<AccessGroupEnum>> GetUserGroups( Guid userId ) {
            var groups = _users
                .AsNoTracking()
                .Include( g => g.Groups )
                .SelectMany( x => x.Groups )
                .Select( x => (AccessGroupEnum)x.Id )
                .ToHashSet();
            return Task.FromResult( groups );
        }

        public Task<HashSet<PermissionEnum>> GetUserPermissions( Guid userId ) {
            var groupsQuery = _users
                .AsNoTracking()
                .Include( g => g.Groups )
                .ThenInclude( r => r.Permissions )
                .Where( x => x.Id == userId )
                .Select( x => x.Groups );

            var permissions = groupsQuery
                .SelectMany( x => x )
                .SelectMany( g => g.Permissions )
                .Select( p => (PermissionEnum)p.Id )
                .ToHashSet();
            return Task.FromResult( permissions );
        }
    }
}
