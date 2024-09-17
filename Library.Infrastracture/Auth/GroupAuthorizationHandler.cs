using Library.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastracture.Auth
{
    public class GroupAuthorizationHandler : AuthorizationHandler<GroupRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GroupAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GroupRequirement requirement)
        {

            var parsingResult = Guid.TryParse(context.User.Claims
                .FirstOrDefault(x => x.Type == CustumClaimTypes.UserId)?.Value, out Guid userId);
            if (parsingResult)
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var groups = await service.authRepository.GetUserGroups(userId);
                if (requirement.Group.Any(r => groups.Contains(r)))
                {
                    context.Succeed(requirement);
                }
            }
            //context.Fail(new AuthorizationFailureReason(this, "User doesn't have required role!"));
        }
    }
}
