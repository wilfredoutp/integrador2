using System;
using System.Collections.Generic;

namespace WILF.BL.Paciente
{
    public class GestionEspecie
    {
        public void Save(BE.Especie especie)
        {
            new DA
                .Paciente
                .RepositoryEspecie()
                .Save(especie);
        }

        public BE.Especie GetEspecie(Int32 idespecie)
        {
           return new DA
                .Paciente
                .RepositoryEspecie()
                .GetEspecie(idespecie);
        }

        public List<BE.Especie> GetEspecie()
        {
            return new DA
                .Paciente
                .RepositoryEspecie()
                .GetEspecie();
        }
    }
}
