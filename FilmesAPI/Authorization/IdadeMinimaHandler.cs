using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FilmesAPI.Authorization
{
    public class IdadeMinimaHandler : AuthorizationHandler<IdadeMinimaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinimaRequirement requirement)
        {
            if (!context.User.HasClaim(claim => claim.Type == ClaimTypes.DateOfBirth)) return Task.CompletedTask;

            DateTime dataNascimento = Convert.ToDateTime(context.User.FindFirst(claim => 
                claim.Type == ClaimTypes.DateOfBirth
                )?.Value);
            int idadeObtida = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento > DateTime.Today.AddYears(-idadeObtida)) idadeObtida--;
            
            if (idadeObtida >= requirement.IdadeMinima) context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}