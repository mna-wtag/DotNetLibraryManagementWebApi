using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetLibraryManagementWebApi.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using DotNetLibraryManagementWebApi.Helpers.LibraryManagement.Helpers;

namespace DotNetLibraryManagementWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]
    public class AdministratorsController : ControllerBase
    {
        private readonly LibraryManagementContext _context;

        public AdministratorsController(LibraryManagementContext context)
        {
            _context = context;
        }

        // GET: api/Administrators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAdministrator()
        {
            return await _context.Administrator.ToListAsync();
        }

        // GET: api/Administrators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministrator(int id)
        {
            var administrator = await _context.Administrator.FindAsync(id);

            if (administrator == null)
            {
                return NotFound();
            }

            return administrator;
        }

        // PUT: api/Administrators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(int id, Administrator administrator)
        {
            if (id != administrator.AdminId)
            {
                return BadRequest();
            }

            try
            {
                var admin = await _context.Administrator.FirstOrDefaultAsync(a => a.AdminId == id);
                if (admin.Upassword != administrator.Upassword)
                {
                    admin.Upassword = CryptoHelper.Hash(administrator.Upassword);
                }

                admin.FirstName = administrator.FirstName;
                admin.LastName = administrator.LastName;
                admin.HomeAddress = administrator.LastName;
                admin.City = administrator.City;
                admin.Country = administrator.Country;
                admin.MobileNo = administrator.MobileNo;
                admin.PassportNo = administrator.PassportNo;
                admin.Email = administrator.Email;
                admin.AdminLevel = administrator.AdminLevel;
                admin.AccountStatus = administrator.AccountStatus;
                admin.Nidno = administrator.Nidno;
                admin.Uname = administrator.Uname;
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Administrators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Administrator>> PostAdministrator([Bind("FirstName,LastName,Nidno,PassportNo,Email,MobileNo,HomeAddress,City,Country,Uname,Upassword,AccountStatus,AdminLevel")]Administrator administrator)
        {
            administrator.Upassword = CryptoHelper.Hash(administrator.Upassword);
            _context.Administrator.Add(administrator); 
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrator", new { id = administrator.AdminId }, administrator);
        }

        // DELETE: api/Administrators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administrator>> DeleteAdministrator(int id)
        {
            var administrator = await _context.Administrator.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            _context.Administrator.Remove(administrator);
            await _context.SaveChangesAsync();

            return administrator;
        }

        private bool AdministratorExists(int id)
        {
            return _context.Administrator.Any(e => e.AdminId == id);
        }
    }
}
