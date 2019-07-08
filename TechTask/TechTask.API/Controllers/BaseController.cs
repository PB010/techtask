using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechTask.Application.Filters.Email;

namespace TechTask.API.Controllers
{
    [ServiceFilter(typeof(EmailSenderService))]
    public class BaseController : Controller
    {
        internal readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
