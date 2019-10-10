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
    public class PessoaJuridicasController : ApiController
    {
        private CaraCrachaModelo db = new CaraCrachaModelo();

        // GET: api/PessoaJuridicas
        public IQueryable<PessoaJuridica> GetPessoaJuridica()
        {
            return db.PessoaJuridica;
        }

        // GET: api/PessoaJuridicas/5
        [ResponseType(typeof(PessoaJuridica))]
        public async Task<IHttpActionResult> GetPessoaJuridica(string id)
        {
            PessoaJuridica pessoaJuridica = await db.PessoaJuridica.FindAsync(id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            return Ok(pessoaJuridica);
        }

        // PUT: api/PessoaJuridicas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPessoaJuridica(string id, PessoaJuridica pessoaJuridica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pessoaJuridica.CNPJ)
            {
                return BadRequest();
            }

            db.Entry(pessoaJuridica).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaJuridicaExists(id))
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

        // POST: api/PessoaJuridicas
        [ResponseType(typeof(PessoaJuridica))]
        public async Task<IHttpActionResult> PostPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PessoaJuridica.Add(pessoaJuridica);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PessoaJuridicaExists(pessoaJuridica.CNPJ))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pessoaJuridica.CNPJ }, pessoaJuridica);
        }

        // DELETE: api/PessoaJuridicas/5
        [ResponseType(typeof(PessoaJuridica))]
        public async Task<IHttpActionResult> DeletePessoaJuridica(string id)
        {
            PessoaJuridica pessoaJuridica = await db.PessoaJuridica.FindAsync(id);
            if (pessoaJuridica == null)
            {
                return NotFound();
            }

            db.PessoaJuridica.Remove(pessoaJuridica);
            await db.SaveChangesAsync();

            return Ok(pessoaJuridica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PessoaJuridicaExists(string id)
        {
            return db.PessoaJuridica.Count(e => e.CNPJ == id) > 0;
        }
    }
}