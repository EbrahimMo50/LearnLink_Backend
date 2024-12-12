using LearnLink_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace LearnLink_Backend.Policies.AdminPolicy
{
    public class AdminRequirmentHandler(AppDbContext DbContext) : AuthorizationHandler<AdminRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirment requirement)
        {
            var IdClaim = context.User.Claims.FirstOrDefault(x => x.Type == "id");

            Console.WriteLine("hello world");
            Console.WriteLine(IdClaim);


            if (IdClaim == null)
                return Task.CompletedTask;

            var user = DbContext.Admins.FirstOrDefault(x => x.Id.ToString() == IdClaim.Value);

            if (user != null)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
