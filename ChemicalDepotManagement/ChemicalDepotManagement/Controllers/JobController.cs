using ChemicalDepotManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChemicalDepotManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly DepotContext _context;

        public JobsController(DepotContext context)
        {
            _context = context;
        }

        // GET: api/jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs.Include(j => j.Chemicals).ToListAsync();
        }

        // GET: api/jobs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Chemicals)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // POST: api/jobs
        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(Job job)
        {
            // Check for warehouse capacity and regulatory compliance
            foreach (var chemical in job.Chemicals)
            {
                var warehouse = await _context.Warehouses.FindAsync(chemical.WarehouseId);
                if (warehouse == null)
                {
                    return BadRequest("Warehouse not found.");
                }

                // Check if the warehouse can accommodate the new chemicals
                var currentStock = warehouse.Chemicals.Sum(c => c.Quantity);
                if (currentStock + chemical.Quantity > warehouse.Capacity)
                {
                    return BadRequest($"Not enough capacity in warehouse {warehouse.Id} for chemical {chemical.Name}.");
                }

                // Check for regulations (e.g., class A and B cannot be in the same warehouse)
                var conflictingChemicals = warehouse.Chemicals
                    .Where(c => (c.Class == "A" && chemical.Class == "B") ||
                                 (c.Class == "B" && chemical.Class == "A"));

                if (conflictingChemicals.Any())
                {
                    return BadRequest($"Warehouse {warehouse.Id} cannot store chemicals of class A and B together.");
                }

                // Add the chemical to the warehouse
                warehouse.Chemicals.Add(chemical);
                chemical.Warehouse = warehouse; // Set the warehouse for the chemical
            }

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job);
        }

        // PUT: api/jobs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // DELETE: api/jobs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
