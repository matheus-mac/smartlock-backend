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
    public class PerfisController : ApiController
    {
        private EntitiesProduction db = new EntitiesProduction();

        // GET: api/Perfis
        public IQueryable<Perfil> GetPerfil()
        {
            return db.Perfil;
        }

        // GET: api/Perfis/5
        [ResponseType(typeof(Perfil))]
        public async Task<IHttpActionResult> GetPerfil(int id)
        {
            Perfil perfil = await db.Perfil.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }

            return Ok(perfil);
        }

        // PUT: api/Perfis/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPerfil(int id, Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != perfil.PerfilId)
            {
                return BadRequest();
            }

            db.Entry(perfil).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(id))
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

        // POST: api/Perfis
        [ResponseType(typeof(Perfil))]
        public async Task<IHttpActionResult> PostPerfil(Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Perfil.Add(perfil);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PerfilExists(perfil.PerfilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = perfil.PerfilId }, perfil);
        }

        // DELETE: api/Perfis/5
        [ResponseType(typeof(Perfil))]
        public async Task<IHttpActionResult> DeletePerfil(int id)
        {
            Perfil perfil = await db.Perfil.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }

            db.Perfil.Remove(perfil);
            await db.SaveChangesAsync();

            return Ok(perfil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PerfilExists(int id)
        {
            return db.Perfil.Count(e => e.PerfilId == id) > 0;
        }
    }
}