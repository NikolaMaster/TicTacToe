using Ninject.Modules;
using TicTacToe.Dal.Interfaces;
using TicTacToe.Dal.Repositories;

namespace TicTacToe.Bll.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModule(string connection)
        {
            _connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}