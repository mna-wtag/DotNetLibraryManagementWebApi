using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetLibraryManagementWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DotNetLibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public RequestsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            var requests = _context.Request.Include(r => r.Book).Include(r => r.User);
            return await requests.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{token}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<Request>> GetRequest(String token)
        {
            var request = await _context.Request.FirstOrDefaultAsync(a=> a.RequestToken.Equals(token));

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,SuperAdmin,UpdateAdmin")]
        public async Task<IActionResult> PutRequest(int id, [FromBody]int flag)
        {
            var request = await _context.Request.FirstOrDefaultAsync(a=> a.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            #region Execute SP
            string adminId = null;
            // var identity = HttpContext.User.Identity as ClaimsIdentity;
            var claims = User.Claims;
            if (claims != null)
            {
                adminId = claims.FirstOrDefault(x => x.Type =="AdminId").Value;
                    //identity.Claims.FirstOrDefault(x => x.ClaimType == "AdminId").Value;
            }

            try
            {
                var result = _context.Database.
                            ExecuteSqlCommand(
                           "[dbo].[ProcessBookCheckOutRequest] @requestToken = {0}, @adminId = {1}, @flag = {2}",
                            request.RequestToken,
                            adminId,
                            flag
                            );
            }
            catch (Exception ex)
            {
                throw;
            }
            #endregion

            #region old code
            /*
            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }*/
            #endregion

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.RequestId == id);
        }
    }
}
