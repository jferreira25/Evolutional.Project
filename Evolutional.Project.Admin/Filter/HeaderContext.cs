
using Evolutional.Project.Admin.Core;
using Evolutional.Project.Admin.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Evolutional.Project.Admin.Filter
{
    public class HeaderContext : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var token = context.HttpContext.GetAuthorizationTokenFromRequest();

                if (!TokenValidator.Validate(token))
                    throw new Exception("401|unauthorized - Usuário ou senha inválidos");
               
            }
            catch (Exception ex)
            {
                context.Result = new UnauthorizedResult();
                Console.WriteLine($"Erro na obteção de header para o context. {ex.Message}");
            }

            base.OnActionExecuting(context);
        }

    }
}
