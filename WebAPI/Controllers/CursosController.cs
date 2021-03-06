using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dominio;
using Aplicacion.Cursos;
using Microsoft.AspNetCore.Authorization;
using System;

namespace WebAPI.Controllers
{
    // http://localhost:5000/api/Cursos

    
    [Route ("api/[controller]")]
    [ApiController]
    public class CursosController: MiControllerBase
    {
      

        [HttpGet]
        // [Authorize]

        public async Task<ActionResult<List<CursoDto>>> Get()
        {

            return await Mediator.Send(new Consulta.ListaCursos());
        }


        // http://localhost:5000/api/Cursos/id
        [HttpGet("{id}")]

        public async Task<ActionResult<CursoDto>> GetbyId(Guid id)
        {
            return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
        }


        //[HttpPost]

        //public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecutar data)
        //{
        //    return await Mediator.Send(data);
        //}

        [HttpPut("{id}")]

        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecutar data)
        {
            data.CursoId = id;

            return await Mediator.Send(data);
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {

            return await Mediator.Send(new Eliminar.Ejecutar { Id = id });
        }




    }    
        
    }
