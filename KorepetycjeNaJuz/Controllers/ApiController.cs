using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KorepetycjeNaJuz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApiController : ControllerBase
	{
		[NonAction]
		public virtual StatusCodeResult InternalServerError()
		{
			return StatusCode((int)HttpStatusCode.InternalServerError);
		}
	}
}