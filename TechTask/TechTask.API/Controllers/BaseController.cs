using Microsoft.AspNetCore.Mvc;
using TechTask.Persistence.Context;

namespace TechTask.API.Controllers
{
    public class BaseController : Controller
    {
        internal readonly AppDbContext _context;

        public BaseController(AppDbContext context)
        {
            _context = context;
        }
    }
}
