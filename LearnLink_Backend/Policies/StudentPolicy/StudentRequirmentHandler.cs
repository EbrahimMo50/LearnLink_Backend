using LearnLink_Backend.Entities;
using Microsoft.AspNetCore.Authorization;

namespace LearnLink_Backend.Policies.StudentPolicy
{
    public class StudentRequirmentHandler(AppDbContext DbContext) : AuthorizationHandler<StudentRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentRequirment requirement)
        {
            var IdClaim = context.User.Claims.FirstOrDefault(x => x.Type == "id");
            if(IdClaim== null)
                return Task.CompletedTask;

            var user = DbContext.Students.FirstOrDefault(x => x.Id.ToString() == IdClaim.Value);

            if (user != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
