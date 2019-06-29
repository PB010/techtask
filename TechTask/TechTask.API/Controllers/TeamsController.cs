using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace TechTask.API.Controllers
{
    public class TeamsController : BaseController
    {
        public TeamsController(IMediator mediator) : base(mediator)
        {}
    }
}
