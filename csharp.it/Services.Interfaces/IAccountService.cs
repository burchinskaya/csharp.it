﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using csharp.it.Models;
using Microsoft.AspNetCore.Identity;

namespace csharp.it.Services.Interfaces
{
	public interface IAccountService
	{
		Task<IdentityResult> Register(UserRegistration model);
		Task<JwtSecurityToken> Authenticate(UserAuthorization model);
		Task<User> GetCurrentUserAsync();
		Task<IEnumerable<User>> GetUsersOfRole(string role);
		Task<IEnumerable<string>> GetCurrentUserRoles();
		Task<IEnumerable<string>> GetRolesByUserEmail(string email);
	}
}
