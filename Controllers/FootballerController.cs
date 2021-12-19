using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiFutbolistas.Context;
using WebApiFutbolistas.Models;

namespace WebApiFutbolistas.Controllers
{
    [Route("API/[controller]")]
    public class FootballerController : Controller
    {
        private readonly AppDbContext context;
        public FootballerController(AppDbContext context)
        {
            this.context = context;
        }
        //GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.Footballer.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //GET api/<controller>/5
        [HttpGet("{id}", Name="GetFutbolista")]
        public ActionResult Get(int id)
        {
            try
            {
                var footballPlayer = context.Footballer.FirstOrDefault(f => f.id == id);
                return Ok(footballPlayer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Footballer footballer)
        {
            try
            {
                context.Footballer.Add(footballer);
                context.SaveChanges();
                return CreatedAtRoute("GetFutbolista", new { id= footballer.id }, footballer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //PUT api/<controller>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Footballer footballer)
        {
            try
            {
                if (footballer.id == id)
                {
                    context.Entry(footballer).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetFutbolista", new { id = footballer.id }, footballer);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var futbolista = context.Footballer.FirstOrDefault(f => f.id == id);
                if (futbolista != null)
                {
                    context.Footballer.Remove(futbolista);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}