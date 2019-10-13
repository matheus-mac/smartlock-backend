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
using smartlock_backend.Util;

namespace smartlock_backend.Controllers
{
    public class InvasoesController : ApiController
    {
        private CaraCrachaModelo db = new CaraCrachaModelo();

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
            await db.SaveChangesAsync();

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

        [ActionName("RegistraInvasao")]
        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<HttpResponseMessage> RegistraInvasaoAsync(int numeroSerial)
        {
            Fechadura fechadura = db.Fechadura.Find(numeroSerial);
            DateTime horarioInvasao = DateTime.Now;

            db.Invasoes.Add(new Invasoes()
            {
                DataHora = horarioInvasao,
                VideoLink = "",
                NumeroSerial = numeroSerial,
            });

            await db.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK, db.Invasoes.Where(invasao=> invasao.NumeroSerial == numeroSerial).ToList().LastOrDefault().InvasoesId);
        }

        [ActionName("EditaLinkInvasao")]
        [HttpPost]
        [ResponseType(typeof(bool))]
        public async Task<HttpResponseMessage> EditaLinkInvasao(int invasaoId, string videoLink)
        {
            Invasoes invasao = db.Invasoes.Find(invasaoId);
            Fechadura fechadura = invasao.Fechadura;

            invasao.VideoLink = videoLink;

            await db.SaveChangesAsync();

            bool emailEnviado = EmailUtil.EnviarEmail(fechadura.Usuario.Email,
                                      Constantes.EmailRegistraInvasaoAssunto,
                                          String.Format(Constantes.EmailRegistraInvasaoTexto, fechadura.NumeroSerial,
                                          fechadura.IdentificadorFechadura, invasao.DataHora, videoLink));


            return Request.CreateResponse(HttpStatusCode.OK, emailEnviado);
        }
    }
}