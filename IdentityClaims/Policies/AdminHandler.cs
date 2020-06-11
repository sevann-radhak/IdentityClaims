using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace IdentityClaims.Policies
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
