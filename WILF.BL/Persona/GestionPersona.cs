using System;
using System.Collections.Generic;
using System.Transactions;

namespace WILF.BL.Persona
{
    public class GestionPersona
    {
        public bool Save(BE.Persona persona)
        {
            bool result = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    new DA.Persona.RepositoryPersona().Save(persona);
                    if (persona.IdPersona > 0)
                    {
                        BE.Usuario usuario = new BE.Usuario
                        {
                            IdPersona = persona.IdPersona,
                            User = persona.Dni,
                            Password = persona.Dni,
                            IdPerfil = 2
                        };
                        new Usuario.GestionUsuario().Save(usuario);
                    }
                    scope.Complete();
                    result = true;
                }
                //TODO: envio de correo electronico
                //if(!string.IsNullOrEmpty(persona.Email))
                //    Emails.Email.send(persona.Email, "Prueba", "No se que va a enviar");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public List<BE.Persona> GetCliente()
        {
            return new DA
                .Persona
                .RepositoryPersona()
                .GetCliente();
        }
        public BE.Persona GetCliente(Int32 idpersona)
        {
            return new DA
                .Persona
                .RepositoryPersona()
                .GetCliente(idpersona);
        }
        public List<BE.Persona> GetCliente(string dni, string nombre)
        {
            return new DA
                .Persona
                .RepositoryPersona()
                .GetCliente(dni, nombre);
        }
    }
}
