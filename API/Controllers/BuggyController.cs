﻿using System.Security.Claims;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class BuggyController : BaseApiController
	{
		[HttpGet("unauthorized")]

		public IActionResult GetUnauthorized()
		{
			return Unauthorized();
		}

		[HttpGet("bad-request")]

		public IActionResult GetBadRequest()
		{
			return BadRequest("Not a good request");
		}

		[HttpGet("not-found")]

		public IActionResult GetNotFound()
		{
			return NotFound();
		}

		[HttpGet("internal-error")]

		public IActionResult GetInternalError()
		{
			throw new Exception("This is a test exception");
		}

		[HttpPost("validation-error")]

		public IActionResult GetValidationError(CreateProductDto product)
		{
			return Ok();
		}

		[Authorize]
		[HttpGet("secret")]
		public IActionResult GetSecret()
		{
			var name = User.FindFirst(ClaimTypes.Name)?.Value;

			var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			return Ok("سلام برارم" + name +" ای دی خودتم اینه" + id);
		}
		[Authorize(Roles ="Admin")]
		[HttpGet("admin-secret")]
		public IActionResult GetAdminSecret()
		{
			var name = User.FindFirst(ClaimTypes.Name)?.Value;

			var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			var isAdmin = User.IsInRole("Admin");

			var roles = User.FindFirstValue(ClaimTypes.Role);

			return Ok(new
			{
				name,
				id,
				isAdmin,
				roles
			});
		}

	}
}
