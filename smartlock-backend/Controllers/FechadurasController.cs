﻿using System;
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
using smartlock_backend.DTOS;
using smartlock_backend.Models;
using smartlock_backend.Util;

namespace smartlock_backend.Controllers
{
    public class FechadurasController : ApiController
    {
        private CaraCrachaModelo db = new CaraCrachaModelo();

        // GET: api/Fechaduras
        public IQueryable<Fechadura> GetFechadura()
        {
            return db.Fechadura.Include("Usuario");
            
        }

        // GET: api/Fechaduras/5
        [ResponseType(typeof(Fechadura))]
        public async Task<IHttpActionResult> GetFechadura(int id)
        {
            Fechadura fechadura = await db.Fechadura.FindAsync(id);
            if (fechadura == null)
            {
                return NotFound();
            }

            return Ok(fechadura);
        }

        // PUT: api/Fechaduras/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFechadura(int id, Fechadura fechadura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fechadura.NumeroSerial)
            {
                return BadRequest();
            }

            db.Entry(fechadura).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FechaduraExists(id))
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

        // POST: api/Fechaduras
        [ResponseType(typeof(Fechadura))]
        public async Task<IHttpActionResult> PostFechadura(Fechadura fechadura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fechadura.Add(fechadura);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FechaduraExists(fechadura.NumeroSerial))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = fechadura.NumeroSerial }, fechadura);
        }

        // DELETE: api/Fechaduras/5
        [ResponseType(typeof(Fechadura))]
        public async Task<IHttpActionResult> DeleteFechadura(int id)
        {
            Fechadura fechadura = await db.Fechadura.FindAsync(id);
            if (fechadura == null)
            {
                return NotFound();
            }

            db.Fechadura.Remove(fechadura);
            await db.SaveChangesAsync();

            return Ok(fechadura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FechaduraExists(int id)
        {
            return db.Fechadura.Count(e => e.NumeroSerial == id) > 0;
        }

        [ActionName("VerificaCadastroUsuario")]
        [HttpGet]
        [ResponseType(typeof(VerificaUsuarioVinculadoDTO))]
        public HttpResponseMessage VerificaCadastroUsuario(int numeroSerial, string rfidUUID)
        {
            Usuario usuario = db.Usuario.Where(user => user.RFIDCard == rfidUUID).FirstOrDefault();
            if (usuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Usuario não encontrado");
            }

            Fechadura fechadura = db.Fechadura.Find(numeroSerial);
            bool usuarioVinculado = usuario.Fechadura.Contains(fechadura);

            if (!usuarioVinculado)
            {
                EmailUtil.EnviarEmail(fechadura.Usuario.Email, 
                                         Constantes.EmailUsuarioNaoVinculadoAssunto,
                                          String.Format(Constantes.EmailUsuarioNaoVinculadoTexto, usuario.UsuarioId.ToString(), usuario.Nome,
                                                fechadura.NumeroSerial, fechadura.IdentificadorFechadura, DateTime.Now));
            }

            return Request.CreateResponse(HttpStatusCode.OK, usuario.toVerificarUsuarioVinculadoDTO(usuarioVinculado));
        }
    }
}