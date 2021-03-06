using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IList<InstructorModel>> ObtenerLista();

        Task<InstructorModel> ObtenerPorId(Guid id);

        Task<int> Nuevo(InstructorModel parametros);

        Task<int> Actualiza(InstructorModel parametros);

        Task<int> Elimina(Guid id);
    }
}
