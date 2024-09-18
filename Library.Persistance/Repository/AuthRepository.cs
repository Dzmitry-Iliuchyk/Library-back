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
                .FirstOrDefaultAsync( rt => rt.Token == refreshToken );
            
            if (tokenRecord != null) {
                _token.Remove( tokenRecord );
            }
        }
        public async Task RemoveAllRefreshTokens( Guid userId) {
            
            var tokenRecords = await _token
                .Where( rt => rt.UserId == userId)
                .ToListAsync();
            
            if (tokenRecords != null) {
                _token.RemoveRange( tokenRecords );
            }
        }
        public async Task<string?> GetActiveRefreshToken( Guid userId ) {

            var token = await _token.AsNoTracking().FirstOrDefaultAsync( t => t.UserId == userId);
            if (token?.ExpiryDate> DateTime.UtcNow) { 
                 return token?.Token;
            }
           return null;
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
        public async Task AddUserToGroup( Guid userId, AccessGroupEnum group ) {
            var searchedGroup = await _groups
                .FirstOrDefaultAsync( r => r.Name == group.ToString() )
                ?? throw new GroupNotFountException();
            var userEntity = await _users
               .FirstOrDefaultAsync( u => u.Id == userId );
            userEntity.Groups.Add( searchedGroup );
            _users.Update( userEntity );
        }

        public async Task RemoveUserFromGroup( Guid userId, AccessGroupEnum group ) {
            var searchedGroup = await _groups
                .Include(u=>u.Users)
                .FirstOrDefaultAsync( r => r.Name == group.ToString() )
                ?? throw new GroupNotFountException();
            var userEntity = await _users
                .Include(u=>u.Groups)
                .FirstOrDefaultAsync( u => u.Id == userId );

            if (userEntity.Groups.Contains( searchedGroup )) {
                userEntity.Groups.Remove( searchedGroup );
                _users.Update( userEntity );
            }
        }
        public Task<HashSet<AccessGroupEnum>> GetUserGroups( Guid userId ) {
            var groups = _users
                .AsNoTracking()
                .Where(u=>u.Id == userId)
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
