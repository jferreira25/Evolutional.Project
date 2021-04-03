using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Evolutional.Project.Controllers
{
    public abstract class BaseController<T> : Controller
    {
        protected IMediator MediatorService { get; }

        protected BaseController(IMediator mediatorService)
        {
            MediatorService = mediatorService;

        }
      
        protected virtual async Task<IActionResult> GenerateResponseAsync(Func<Task> func, HttpStatusCode responseCode)
        {
            try
            {
                await func();

                return StatusCode((int)responseCode);
            }
            catch
            {
                throw;
            }
        }

        protected virtual async Task<IActionResult> GenerateResponseAsync<TDataObject>(Func<Task<TDataObject>> func)
        {
            return await GenerateResponseAsync(func, HttpStatusCode.OK);
        }
    
        protected virtual async Task<IActionResult> GenerateResponseAsync<TDataObject>(Func<Task<TDataObject>> func, HttpStatusCode responseCode)
        {
            try
            {
                var response = await func();

                return StatusCode((int)responseCode, new
                {
                    data = response
                   
                });
            }
            catch (Exception ex)
            {
                return HandleExceptionResult(ex);
            }

        }
        private IActionResult HandleExceptionResult(Exception ex)
        {
            var notifications = new List<string>();
            var statusCode = 500;
            switch (ex.Message)
            {
                case "401":
                    statusCode = 401;
                    notifications.Add("unauthorized - Usuário ou senha inválidos");
                    break;
                default:
                    break;
            }

            return StatusCode((int)statusCode, new { notifications });
        }
    }
}