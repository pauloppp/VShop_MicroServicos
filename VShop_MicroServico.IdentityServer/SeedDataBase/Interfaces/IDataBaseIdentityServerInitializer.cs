namespace VShop_MicroServico.IdentityServer.SeedDataBase.Interfaces
{
    public interface IDataBaseIdentityServerInitializer
    {
        void InitializeSeedRoles();
        void InitializeSeedUsers();
    }
}
