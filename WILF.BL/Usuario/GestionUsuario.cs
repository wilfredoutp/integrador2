namespace WILF.BL.Usuario
{
    public class GestionUsuario
    {
        public void Save(BE.Usuario usuario)
        {
            new DA
                .Usuario
                .RepositoryUsuario()
                .Save(usuario);
        }
        public bool Valid(string user, string password)
        {
            return new DA
                .Usuario
                .RepositoryUsuario()
                .Valid(user, password);
        }
        public BE.Usuario GetUser(string user)
        {
            return new DA
                .Usuario
                .RepositoryUsuario()
                .GetUser(user);
        }
    }
}
