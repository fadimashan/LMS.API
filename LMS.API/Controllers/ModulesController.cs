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
        //public async Task<IActionResult> PutModule(int id, Module @module)
        [HttpPut("{name}")]
        public async Task<ActionResult<ModuleDto>> PutCourse(string name, ModuleDto dto)
        {
            var module = await wu.ModuleRepo.GetModule(name);

            if (module is null) return StatusCode(StatusCodes.Status404NotFound);

            mapper.Map(dto, module);

            if (await wu.ModuleRepo.SaveAsync())
            {
                return Ok(mapper.Map<ModuleDto>(module));
            }
            else
            {
                return StatusCode(500);
            }
        }


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
