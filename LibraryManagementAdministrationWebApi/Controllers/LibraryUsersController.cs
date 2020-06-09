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
    [Authorize(Roles ="SuperAdmin")]
    public class LibraryUsersController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public LibraryUsersController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/LibraryUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryUser>>> GetLibraryUser()
        {
            return await _context.LibraryUser.ToListAsync();
        }

        // GET: api/LibraryUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryUser>> GetLibraryUser(int id)
        {
            var libraryUser = await _context.LibraryUser.FindAsync(id);

            if (libraryUser == null)
            {
                return NotFound();
            }

            return libraryUser;
        }

        // PUT: api/LibraryUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibraryUser(int id, LibraryUser libraryUser)
        {
            if (id != libraryUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(libraryUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryUserExists(id))
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

        private bool LibraryUserExists(int id)
        {
            return _context.LibraryUser.Any(e => e.UserId == id);
        }
    }
}
