using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WILF.BL.Paciente
{
    public class GestionHistorial
    {
        public void Save(BE.Historial historial)
        {
            new DA
                .Paciente
                .RepositoryHistoria()
                .Save(historial);
        }

        public BE.Historial GetHistorialId(Int32 idhistorial)
        {
            return new DA
                .Paciente
                .RepositoryHistoria()
                .GetHistorialId(idhistorial);
        }

        public List<BE.Historial> GetHistorialPaciente(Int32 idpaciente)
        {
            return new DA
                .Paciente
                .RepositoryHistoria()
                .GetHistorialPaciente(idpaciente);
        }
    }
}
