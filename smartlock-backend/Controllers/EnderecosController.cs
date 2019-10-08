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
    public class EnderecosController : ApiController
    {
        private EntitiesProduction db = new EntitiesProduction();

        // GET: api/Enderecoes
        public IQueryable<Endereco> GetEndereco()
        {
            return db.Endereco;
        }

        // GET: api/Enderecoes/5
        [ResponseType(typeof(Endereco))]
        public async Task<IHttpActionResult> GetEndereco(int id)
        {
            Endereco endereco = await db.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(endereco);
        }

        // PUT: api/Enderecoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != endereco.EnderecoId)
            {
                return BadRequest();
            }

            db.Entry(endereco).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
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

        // POST: api/Enderecoes
        [ResponseType(typeof(Endereco))]
        public async Task<IHttpActionResult> PostEndereco(Endereco endereco)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Endereco.Add(endereco);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EnderecoExists(endereco.EnderecoId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = endereco.EnderecoId }, endereco);
        }

        // DELETE: api/Enderecoes/5
        [ResponseType(typeof(Endereco))]
        public async Task<IHttpActionResult> DeleteEndereco(int id)
        {
            Endereco endereco = await db.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            db.Endereco.Remove(endereco);
            await db.SaveChangesAsync();

            return Ok(endereco);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnderecoExists(int id)
        {
            return db.Endereco.Count(e => e.EnderecoId == id) > 0;
        }
    }
}