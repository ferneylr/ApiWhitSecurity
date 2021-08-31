using System.Collections.Generic;
using MediatR;
using Dominio;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {
            private readonly CursosOnlineContext context;
            private readonly IMapper _mapper;

            public Manejador(CursosOnlineContext _context, IMapper mapper)
            {
                context = _context;
                _mapper = mapper;
            }

            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken CancellationToken)
            {
                var cursos = await context.Curso
                    .Include(x=> x.ComentarioLista)
                    .Include(x=> x.PrecioPromocion)
                    .Include(x => x.InstructoresLink)
                    .ThenInclude(x => x.Instructor).ToListAsync();

                var cursosDto = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);
                

                return cursosDto;


            }
        }
    }
}