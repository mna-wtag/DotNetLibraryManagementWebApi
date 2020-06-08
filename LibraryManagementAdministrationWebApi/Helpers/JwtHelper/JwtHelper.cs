using DotNetLibraryManagementWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DotNetLibraryManagementWebApi.Helpers
{
    //Helper Class for JWT Token Operations
    public static class JwtHelper
    {
        //Generates JWT on given secrect key and user object
        public static string GenrateJwtTokenForLibraryAdmin(string adminId, string adminRole, string adminName, string secret)
        {
            string tokenString = null;

            //Generate claims
            System.Security.Claims.Claim adminidclaim = new System.Security.Claims.Claim("AdminId", adminId);
            System.Security.Claims.Claim roleClaim = new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, adminRole);

            var userClaims = new System.Security.Claims.Claim[]
                    {
                        new System.Security.Claims.Claim("AdminId", adminId),
                        new System.Security.Claims.Claim("AdminName", adminName),
                         new System.Security.Claims.Claim("AdminRole", adminRole),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, adminRole)
                    };

            /*
            var userClaims = new System.Security.Claims.Claim[]
                    {
                        new System.Security.Claims.Claim("AdminId", admin.AdminId.ToString()),
                        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, admin.AdminLevelNavigation.AdminRole1)
                    };*/

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null,
                null,
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            //write token
            tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
