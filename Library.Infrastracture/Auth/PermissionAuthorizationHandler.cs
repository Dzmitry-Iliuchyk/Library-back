using Library.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastracture.Auth
{
    public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement> {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler( IServiceScopeFactory serviceScopeFactory ) {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement ) {

            var parsingResult = Guid.TryParse( context.User.Claims
                .FirstOrDefault( x => x.Type == CustumClaimTypes.UserId )?.Value, out Guid userId );

            if (parsingResult) {
                using var scope = _serviceScopeFactory.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var permissions = await unitOfWork.authRepository.GetUserPermissions( userId );
                if (requirement.Permissions.All( p => permissions.Contains( p ) )) {
                    context.Succeed( requirement );
                }
            }
        }
    }
}
