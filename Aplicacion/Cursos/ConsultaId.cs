using MediatR;
using Dominio;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicacion.ManejadorError;
using System.Net;
using System;
using AutoMapper;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico: IRequest<CursoDto>{

            public Guid Id { get; set; }
        }

        

        public class Manejador: IRequestHandler<CursoUnico, CursoDto>
        {
            private readonly CursosOnlineContext context;
            private readonly IMapper _mapper;

              public Manejador(CursosOnlineContext _context, IMapper mapper)
              {
                  context= _context;
                _mapper = mapper;
              }

            public  async Task<CursoDto> Handle (CursoUnico request, CancellationToken CancellationToken)
            {
                var curso = await context.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructoresLink)
                    .ThenInclude(a => a.Instructor).FirstOrDefaultAsync(y=> y.CursoId== request.Id);

                if (curso == null)
                {
                    //  throw new Exception("No se puede eliminar curso");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontro el curso" });
                }

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);
                return cursoDto;
            }
        }
    }
}