using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using SRLearningServer.Data;

namespace SRLearningServer.Components.Authorization
{
    public class IsAdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ApplicationUser>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ApplicationUser resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
