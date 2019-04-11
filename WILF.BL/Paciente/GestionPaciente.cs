using System;
using System.Collections.Generic;

namespace WILF.BL.Paciente
{
    public class GestionPaciente
    {
        public void Save(BE.Paciente paciente)
        {
            new DA.Paciente.RepositoryPaciente().Save(paciente);
        }

        public BE.Paciente GetPaciente(Int32 idpaciente)
        {
            return new DA
                .Paciente
                .RepositoryPaciente()
                .GetPaciente(idpaciente);
        }

        public List<BE.Paciente> GetPacientexPersonaId(Int32 idpersona)
        {
            return new DA
                .Paciente
                .RepositoryPaciente()
                .GetPacientexPersonaId(idpersona);
        }
    }
}
