﻿using System;
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
using Microsoft.AspNetCore.JsonPatch;

namespace LMS.API.Controllers
{
    [Route("api/Courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IWorkUnit wu;
        private readonly IMapper mapper;

        public CoursesController(IWorkUnit wu, IMapper mapper)
        {
            this.wu = wu;
            this.mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse([FromQuery] PaginationFilter filter, [FromQuery] bool includeModules = false, string action = null )
        {
            var courses = await wu.CourseRepo.GetAllCourses(!includeModules, action ,filter);
            var model = mapper.Map<IEnumerable<CourseDto>>(courses);

            return Ok(model);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await wu.CourseRepo.GetCourse(id);

            if (course == null)
            {
                ModelState.AddModelError("Title", "Course not available");
                return NotFound(ModelState);
            }

            return course;
        }




        //PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> PutCourse(int id, CourseDto dto)
        {
            var theCourse = await wu.CourseRepo.GetCourse(id);

            if(theCourse is null) return StatusCode(StatusCodes.Status404NotFound);

            mapper.Map(dto, theCourse);

            if (await wu.CourseRepo.SaveAsync())
            {
                return Ok(mapper.Map<CourseDto>(theCourse));
            }
            else
            {
                return StatusCode(500);

            }
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await wu.CourseRepo.GetCourse(id);
            if (course == null)
            {
                ModelState.AddModelError("Title", "Course not available");
                return NotFound(ModelState);
            }

            wu.CourseRepo.Remove(course);
            await wu.CompleteAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return wu.CourseRepo.IsExists(id);
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult<CourseDto>> PatchCourse(int courseId, JsonPatchDocument<CourseDto> patchDocument)
        {
            var course = await wu.CourseRepo.GetCourse(courseId);
            if (course is null)
            {
                ModelState.AddModelError("Title", "Course not available");
                return NotFound(ModelState);
            }

            var model = mapper.Map<CourseDto>(course);
            patchDocument.ApplyTo(model, ModelState);

            if (!TryValidateModel(model))
                return BadRequest(ModelState);
            mapper.Map(model, course);

            if (await wu.CourseRepo.SaveAsync())
                return Ok(mapper.Map<CourseDto>(course));
            else
                return StatusCode(500);

        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseDto dto)
        {

            var exists = wu.CourseRepo.IsTitleExists(dto.Title);
            if (exists)
            {
                ModelState.AddModelError("Title", "Tilte in use");
                return BadRequest(ModelState);
            }


            var eventDay = mapper.Map<Course>(dto);
            await wu.CourseRepo.AddAsync(eventDay);
            if (await wu.CourseRepo.SaveAsync())
            {
                var model = mapper.Map<CourseDto>(eventDay);
                return CreatedAtAction(nameof(GetCourse), new { model.Title }, model);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
