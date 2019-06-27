using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using TechTask.Application.Interfaces;

namespace TechTask.API.Controllers
{
    public class BaseController : Controller
    {
        private readonly IMediator _mediator;
        internal readonly ITokenAuthenticationService _service;

        public BaseController(IMediator mediator, ITokenAuthenticationService service)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}
