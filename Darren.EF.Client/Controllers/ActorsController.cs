﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Darren.EF.Data;
using Microsoft.EntityFrameworkCore;
using Darren.EF.Client.Models.Actors;
using Darren.EF.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Darren.EF.Client.Controllers
{
    [Route("actors")]
    public class ActorsController : Controller
    {
        private readonly Database context;

        public ActorsController(Database context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var entities = await context.Actors.ToListAsync();
            var outputModel = entities.Select(t => new {
                t.Id,
                t.Name,
                t.Timestamp
            });
            return Ok(outputModel);
        }

        [HttpGet("{id}", Name = "GetActor")]
        public IActionResult GetItem(int id)
        {
            var entity = context.Actors
                .Where(t => t.Id == id)
                .FirstOrDefault();
            if (entity == null)
                return NotFound();

            var outputModel = new {
                entity.Id,
                entity.Name,
                entity.Timestamp
            };
            return Ok(outputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ActorCreateInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            var entity = new Actor
            {
                Name = inputModel.Name
            };
            this.context.Actors.Add(entity);
            await this.context.SaveChangesAsync();

            var outputModel = new {
                entity.Id,
                entity.Name
            };

            return CreatedAtRoute("GetActor",new { id=outputModel.Id},outputModel);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ActorUpdateInputModel inputModel)
        {
            if (inputModel == null || id != inputModel.Id)
                return BadRequest();

            var entity = new Actor
            {
                Id = inputModel.Id,
                Name = inputModel.Name,
                Timestamp = inputModel.Timestamp
            };

            try
            {
                this.context.Actors.Update(entity);
                this.context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                var inEntry = ex.Entries.Single();
                var dbEntry = inEntry.GetDatabaseValues();
                if (dbEntry == null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Actor was deleted by another user");

                var inModel = inEntry.Entity as Actor;
                var dbModel = dbEntry.ToObject() as Actor;

                var conflicts = new Dictionary<string, string>();
                if (inModel.Name != dbModel.Name)
                    conflicts.Add("Actor", $"Changed from {inModel.Name} to {dbModel.Name}");

                if (inModel.Timestamp != dbModel.Timestamp)
                    conflicts.Add("Timestamp", $"Changed from {Convert.ToBase64String(inModel.Timestamp)} to {Convert.ToBase64String(dbModel.Timestamp)}");

                return StatusCode(StatusCodes.Status412PreconditionFailed, conflicts);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = context.Actors.Where(t => t.Id == id).FirstOrDefault();
            if (entity == null)
                return NotFound();

            this.context.Actors.Remove(entity);
            this.context.SaveChanges();
            return NoContent();
        }
    }
}