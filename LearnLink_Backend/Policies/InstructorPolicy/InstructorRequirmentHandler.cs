using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace LearnLink_Backend.Policies.InstructorPolicy
{
    //to pass the admin adjust the search to also pass the users from table admin rendaring them able to do functions of the instructor
    public class InstructorRequirmentHandler(AppDbContext DbContext) : AuthorizationHandler<InstructorRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InstructorRequirment requirement)
        {
            var IdClaim = context.User.Claims.FirstOrDefault(x => x.Type == "id");
            if (IdClaim == null)
                return Task.CompletedTask;

            var user = DbContext.Instructors.FirstOrDefault(x => x.Id.ToString() == IdClaim.Value);

            if (user != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
