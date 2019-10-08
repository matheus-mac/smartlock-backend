using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using smartlock_backend.Models;

namespace smartlock_backend.Controllers
{
    public class InvasoesController : ApiController
    {
        private EntitiesProduction db = new EntitiesProduction();

        // GET: api/Invasoes
        public IQueryable<Invasoes> GetInvasoes()
        {
            return db.Invasoes;
        }

        // GET: api/Invasoes/5
        [ResponseType(typeof(Invasoes))]
        public async Task<IHttpActionResult> GetInvasoes(int id)
        {
            Invasoes invasoes = await db.Invasoes.FindAsync(id);
            if (invasoes == null)
            {
                return NotFound();
            }

            return Ok(invasoes);
        }

        // PUT: api/Invasoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvasoes(int id, Invasoes invasoes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invasoes.InvasoesId)
            {
                return BadRequest();
            }

            db.Entry(invasoes).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvasoesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Invasoes
        [ResponseType(typeof(Invasoes))]
        public async Task<IHttpActionResult> PostInvasoes(Invasoes invasoes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invasoes.Add(invasoes);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvasoesExists(invasoes.InvasoesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = invasoes.InvasoesId }, invasoes);
        }

        // DELETE: api/Invasoes/5
        [ResponseType(typeof(Invasoes))]
        public async Task<IHttpActionResult> DeleteInvasoes(int id)
        {
            Invasoes invasoes = await db.Invasoes.FindAsync(id);
            if (invasoes == null)
            {
                return NotFound();
            }

            db.Invasoes.Remove(invasoes);
            await db.SaveChangesAsync();

            return Ok(invasoes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvasoesExists(int id)
        {
            return db.Invasoes.Count(e => e.InvasoesId == id) > 0;
        }
    }
}