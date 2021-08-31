using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecutar : IRequest
        {
            public Guid Id { get; set; }
           
        }

        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly CursosOnlineContext context;
            public Manejador(CursosOnlineContext _context)
            {
                context = _context;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var instructorDB = context.CursoInstructor.Where(z => z.CursoId == request.Id).ToList();

                foreach (var instructor in instructorDB)
                {
                    context.CursoInstructor.Remove(instructor);
                }

                var comentarioDB = context.Comentario.Where(x => x.CursoId == request.Id);

                foreach (var cmt in comentarioDB)
                {
                    context.Comentario.Remove(cmt);
                }

                var precioDB = context.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if (precioDB!= null)
                {
                    context.Precio.Remove(precioDB);
                }

                var curso = await context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    //  throw new Exception("No se puede eliminar curso");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { curso = "No se encontro el curso" });
                }

                context.Remove(curso);
                var resultado = await context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }
                
                throw new Exception("No se pudieron guardar los cambios");
            }
        }
    }
}
