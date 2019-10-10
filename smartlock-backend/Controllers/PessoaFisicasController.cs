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
    public class PessoaFisicasController : ApiController
    {
        private CaraCrachaModelo db = new CaraCrachaModelo();

        // GET: api/PessoaFisicas
        public IQueryable<PessoaFisica> GetPessoaFisica()
        {
            return db.PessoaFisica;
        }

        // GET: api/PessoaFisicas/5
        [ResponseType(typeof(PessoaFisica))]
        public async Task<IHttpActionResult> GetPessoaFisica(string id)
        {
            PessoaFisica pessoaFisica = await db.PessoaFisica.FindAsync(id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }

            return Ok(pessoaFisica);
        }

        // PUT: api/PessoaFisicas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPessoaFisica(string id, PessoaFisica pessoaFisica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoaFisica.CPF)
            {
                return BadRequest();
            }

            db.Entry(pessoaFisica).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaFisicaExists(id))
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

        // POST: api/PessoaFisicas
        [ResponseType(typeof(PessoaFisica))]
        public async Task<IHttpActionResult> PostPessoaFisica(PessoaFisica pessoaFisica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PessoaFisica.Add(pessoaFisica);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PessoaFisicaExists(pessoaFisica.CPF))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pessoaFisica.CPF }, pessoaFisica);
        }

        // DELETE: api/PessoaFisicas/5
        [ResponseType(typeof(PessoaFisica))]
        public async Task<IHttpActionResult> DeletePessoaFisica(string id)
        {
            PessoaFisica pessoaFisica = await db.PessoaFisica.FindAsync(id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }

            db.PessoaFisica.Remove(pessoaFisica);
            await db.SaveChangesAsync();

            return Ok(pessoaFisica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaFisicaExists(string id)
        {
            return db.PessoaFisica.Count(e => e.CPF == id) > 0;
        }
    }
}