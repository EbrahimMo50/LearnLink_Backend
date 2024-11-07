using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace LearnLink_Backend.Policies.InstructorPolicy
{
    public class InstructorRequirmentHandler(AppDbContext DbContext) : AuthorizationHandler<InstructorRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InstructorRequirment requirement)
        {
            var IdClaim = context.User.Claims.FirstOrDefault(x => x.Type == "Id");
            if (IdClaim == null)
                return Task.CompletedTask;

            var user = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == IdClaim.Value);

            if (user != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
