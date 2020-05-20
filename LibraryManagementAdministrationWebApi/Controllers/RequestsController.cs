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
    public class RequestsController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public RequestsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin,UpdateAdmin")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequest()
        {
            return await _context.Request.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{token}")]
        [Authorize(Roles = "Admin,SuperAdmin,UpdateAdmin")]
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
        [Authorize(Roles ="Admin,SuperAdmin,UpdateAdmin")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.RequestId)
            {
                return BadRequest();
            }

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
            }

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.RequestId == id);
        }
    }
}
