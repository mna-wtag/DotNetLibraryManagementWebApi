using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetLibraryManagementWebApi.Models;
using Microsoft.AspNetCore.Authorization;


namespace DotNetLibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryUserRegistrationRequestsController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public LibraryUserRegistrationRequestsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/LibraryUserRegistrationRequests
        [HttpGet]
        //[Authorize(Roles = "Admin,SuperAdmin,UpdateAdmin")]
        public async Task<ActionResult<IEnumerable<LibraryUserRegistrationRequest>>> GetLibraryUserRegistrationRequest()
        {
            return await _context.LibraryUserRegistrationRequest.ToListAsync();
        }

        // GET: api/LibraryUserRegistrationRequests/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<LibraryUserRegistrationRequest>> GetLibraryUserRegistrationRequest(int id)
        {
            var libraryUserRegistrationRequest = await _context.LibraryUserRegistrationRequest.FindAsync(id);

            if (libraryUserRegistrationRequest == null)
            {
                return NotFound();
            }

            return libraryUserRegistrationRequest;
        }

        // PUT: api/LibraryUserRegistrationRequests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin,UpdateAdmin")]
        public async Task<IActionResult> PutLibraryUserRegistrationRequest(int id, [FromBody] int flag)
        {
            var request = await _context.Request.FirstOrDefaultAsync(a => a.RequestId == id);
            if (request == null || flag == 0)
            {
                return NotFound();
            }

            string adminId = null;
            var claims = User.Claims;
            if (claims != null)
            {
                adminId = claims.FirstOrDefault(x => x.Type == "AdminId").Value;
            }

            try
            {
                var result = _context.Database.
                            ExecuteSqlCommand(
                           "[dbo].[InitiateLibraryUserAccount] @userRequestId = {0}, @adminId = {1}",
                           request.RequestId,
                           adminId
                           );
            }
            catch (Exception ex)
            {
                throw;
            }
            return NoContent();
        }

        private bool LibraryUserRegistrationRequestExists(int id)
        {
            return _context.LibraryUserRegistrationRequest.Any(e => e.RequestId == id);
        }
    }
}
