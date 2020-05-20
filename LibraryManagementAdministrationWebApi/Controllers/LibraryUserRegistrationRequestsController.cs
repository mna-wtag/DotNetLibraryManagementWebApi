using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAdministrationWebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementAdministrationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibraryUserRegistrationRequestsController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public LibraryUserRegistrationRequestsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/LibraryUserRegistrationRequests
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin,UpdateAdmin")]
        public async Task<ActionResult<IEnumerable<LibraryUserRegistrationRequest>>> GetLibraryUserRegistrationRequest()
        {
            return await _context.LibraryUserRegistrationRequest.ToListAsync();
        }

        // GET: api/LibraryUserRegistrationRequests/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin,UpdateAdmin")]
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
        public async Task<IActionResult> PutLibraryUserRegistrationRequest(int id, LibraryUserRegistrationRequest libraryUserRegistrationRequest)
        {
            if (id != libraryUserRegistrationRequest.RequestId)
            {
                return BadRequest();
            }

            _context.Entry(libraryUserRegistrationRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryUserRegistrationRequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool LibraryUserRegistrationRequestExists(int id)
        {
            return _context.LibraryUserRegistrationRequest.Any(e => e.RequestId == id);
        }
    }
}
