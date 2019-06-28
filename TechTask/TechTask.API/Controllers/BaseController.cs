using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TechTask.API.Controllers
{
    public class BaseController : Controller
    {
        internal readonly IMediator _mediator;

        public BaseController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
    }
}
