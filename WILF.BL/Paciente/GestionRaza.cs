using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WILF.BL.Paciente
{
    public class GestionRaza
    {
        public void Save(BE.Raza raza)
        {
            new DA
                .Paciente
                .RepositoryRaza()
                .Save(raza);
        }

        public BE.Raza GetRaza(Int32 idraza)
        {
            return new DA
                .Paciente
                .RepositoryRaza()
                .GetRaza(idraza);
        }

        public List<BE.Raza> GetRaza()
        {
            return new DA
                .Paciente
                .RepositoryRaza()
                .GetRaza();
        }

        public List<BE.Raza> GetRazaEspecieId(Int32 idespecie)
        {
            return new DA
                .Paciente
                .RepositoryRaza()
                .GetRazaEspecieId(idespecie);
        }
    }
}
