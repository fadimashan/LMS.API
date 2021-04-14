using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Entities;
using LMS.Data.Data;
using LMS.Core.IRepo;
using AutoMapper;
using LMS.Core.Dto;

namespace LMS.API.Controllers
{
    [Route("api/modules")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IWorkUnit wu;
        private readonly IMapper mapper;

        public ModulesController(IWorkUnit wu, IMapper mapper)
        {
            this.wu = wu;
            this.mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            var module = await wu.ModuleRepo.GetAllModules();
            var model = mapper.Map<IEnumerable<ModuleDto>>(module);
            return Ok(model);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(string title)
        {
            var @module = await wu.ModuleRepo.GetModule(title);
            var dto = mapper.Map<ModuleDto>(@module);

            if (dto == null)
            {
                ModelState.AddModelError("Title", "Module not available");
                return NotFound(ModelState);
            }

            return dto;
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutModule(int id, Module @module)
        //{
        //    if (id != @module.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@module).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ModuleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Module>> PostModule(Module @module)
        //{
        //    _context.Modules.Add(@module);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetModule", new { id = @module.Id }, @module);
        //}

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var @module = await wu.ModuleRepo.GetModuleById(id);
            if (@module == null)
            {
                ModelState.AddModelError("Title", "Module not available");
                return NotFound(ModelState);
            }

            wu.ModuleRepo.Remove(@module);
            await wu.CompleteAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return wu.ModuleRepo.IsExists(id);
        }
    }
}
