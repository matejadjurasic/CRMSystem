﻿using CRMSystemAPI.Models.DatabaseModels;
using CRMSystemAPI.Models.DataTransferModels.AuthTransferModels;
using CRMSystemAPI.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var authResponse = await _authService.AuthenticateUserAsync(loginModel);
            if (authResponse == null)
            {
                return Unauthorized("Invalid email or password");
            }
            return Ok(authResponse);
        }
    }
}
