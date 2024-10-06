using ChemicalDepotManagement.Models;
using ChemicalDepotManagement.Models.ChemicalDepotManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChemicalDepotManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepotController : ControllerBase
    {
        private readonly DepotContext _context;

        public DepotController(DepotContext context)
        {
            _context = context;
        }

        // POST: api/depot/jobs
        [HttpPost("jobs")]
        public async Task<ActionResult<Job>> CreateJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }

        // GET: api/depot/jobs/{id}
        [HttpGet("jobs/{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // POST: api/depot/tickets
        [HttpPost("tickets")]
        public async Task<ActionResult<Ticket>> CreateTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        // GET: api/depot/tickets/{id}
        [HttpGet("tickets/{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }
    }
}
