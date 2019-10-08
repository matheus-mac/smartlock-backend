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
    public class AcessosController : ApiController
    {
        private EntitiesProduction db = new EntitiesProduction();

        // GET: api/Acessos
        public IQueryable<Acessos> GetAcessos()
        {
            return db.Acessos;
        }

        // GET: api/Acessos/5
        [ResponseType(typeof(Acessos))]
        public async Task<IHttpActionResult> GetAcessos(string id)
        {
            Acessos acessos = await db.Acessos.FindAsync(id);
            if (acessos == null)
            {
                return NotFound();
            }

            return Ok(acessos);
        }

        // PUT: api/Acessos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAcessos(string id, Acessos acessos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != acessos.AcessosId)
            {
                return BadRequest();
            }

            db.Entry(acessos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcessosExists(id))
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

        // POST: api/Acessos
        [ResponseType(typeof(Acessos))]
        public async Task<IHttpActionResult> PostAcessos(Acessos acessos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acessos.Add(acessos);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AcessosExists(acessos.AcessosId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = acessos.AcessosId }, acessos);
        }

        // DELETE: api/Acessos/5
        [ResponseType(typeof(Acessos))]
        public async Task<IHttpActionResult> DeleteAcessos(string id)
        {
            Acessos acessos = await db.Acessos.FindAsync(id);
            if (acessos == null)
            {
                return NotFound();
            }

            db.Acessos.Remove(acessos);
            await db.SaveChangesAsync();

            return Ok(acessos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AcessosExists(string id)
        {
            return db.Acessos.Count(e => e.AcessosId == id) > 0;
        }
    }
}