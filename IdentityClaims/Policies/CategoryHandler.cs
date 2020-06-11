using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityClaims.Policies
{
    public class CategoryHandler : AuthorizationHandler<CategoryRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CategoryRequirement requirement)
        {
            if (context.User.Claims.Any(c => c.Type == "category" && c.Value == "4"))
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
