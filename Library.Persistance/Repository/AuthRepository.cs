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
        private readonly DbSet<RefreshToken> _token;
        private readonly IMapper _mapper;


        public AuthRepository( LibraryDBContext context, IMapper mapper ) {
            _groups = context.Set<AccessGroupEntity>();
            _users = context.Set<UserEntity>();
            _token = context.Set<RefreshToken>();
            _mapper = mapper;
        }
        public async Task RemoveRefreshToken( string refreshToken ) {
            
            var tokenRecord = await _token
                .AsNoTracking()
                .FirstOrDefaultAsync( rt => rt.Token == refreshToken );
            
            if (tokenRecord != null) {
                _token.Remove( tokenRecord );
            }
        }
        public async Task RemoveOldRefreshTokens( Guid userId) {
            
            var tokenRecords = await _token
                .AsNoTracking()
                .Where( rt => rt.UserId == userId)
                .ToListAsync();
            
            if (tokenRecords != null) {
                _token.RemoveRange( tokenRecords );
            }
        }
        public async Task<string?> GetActiveRefreshToken( Guid userId ) {

            var token = await _token.AsNoTracking().FirstOrDefaultAsync( t => t.UserId == userId);
            if (token.ExpiryDate< DateTime.Now) {
                _token.Remove( token );
                return null;
            }
            return token?.Token;
        }
        public async Task SaveRefreshToken( Guid userId, string token ) {

            var refreshToken = new RefreshToken {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = token,
                ExpiryDate = DateTime.UtcNow.AddDays( 7 )
            };
            await _token.AddAsync( refreshToken );
        }
        public async Task AddUserToGroup( User user, AccessGroupEnum group ) {
            var searchedGroup = await _groups
                .AsNoTracking()
                .FirstOrDefaultAsync( r => r.Name == group.ToString() )
                ?? throw new GroupNotFountException();

            var userEntity = MapUserToUserEntity( user );
            userEntity.Groups.Add( searchedGroup );
            _users.Update( userEntity );
        }

        public async Task RemoveUserFromGroup( User user, AccessGroupEnum group ) {
            var searchedGroup = await _groups
                .AsNoTracking()
                .FirstOrDefaultAsync( r => r.Name == group.ToString() )
                ?? throw new GroupNotFountException();
            var userEntity = MapUserToUserEntity( user );
            userEntity.Groups.Remove( searchedGroup );
            _users.Update( userEntity );
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

        private UserEntity MapUserToUserEntity( User user ) {
            UserEntity userEntity;
            if (_users.Local.Any( e => e.Id == user.Id )) {
                userEntity = _users.Local.First( u => u.Id == user.Id );
            } else
                userEntity = _mapper.Map<UserEntity>( user );
            return userEntity;
        }

    }
}
