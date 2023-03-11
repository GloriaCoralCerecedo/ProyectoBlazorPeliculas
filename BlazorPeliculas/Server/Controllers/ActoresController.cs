using AutoMapper;
using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.DTOs;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public ActoresController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get([FromQuery]PaginacionDTO paginacion)
        {
            //return await context.Actores.ToListAsync();

            var queryable = context.Actores.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnRespuesta(queryable, paginacion.CantidadRegistros);
            return await queryable.OrderBy(x => x.Nombre).Paginar(paginacion).ToListAsync();
        }

        [HttpGet("buscar/{textoBusqueda}")]
        public async Task<ActionResult<List<Actor>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                return new List<Actor>();
            }
            textoBusqueda = textoBusqueda.ToLower();
            return await context.Actores.Where(actor => actor.Nombre.ToLower().Contains(textoBusqueda)).Take(5).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> Get(int id)
        {
            var actor = await context.Actores.FirstOrDefaultAsync(actor => actor.Id == id);
            if (actor is null)
            {
                return NotFound();
            }
            return actor;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Actor actor)
        {
            if (actor.Foto2 is not null)
            {
                var fotoActor = Convert.ToBase64String(actor.Foto2);
                actor.Foto = fotoActor;
            }
            context.Add(actor);
            await context.SaveChangesAsync();
            return actor.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Actor actor)
        {
            context.Update(actor);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAfectadas = await context.Actores.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (filasAfectadas == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
