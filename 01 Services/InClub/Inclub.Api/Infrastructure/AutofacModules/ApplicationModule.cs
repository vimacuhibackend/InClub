using Autofac;
using Inclub.Api.Application.Queries;
using Inclub.Domain.AggregatesModel.CompraAggregate;
using Inclub.Domain.AggregatesModel.ProductoAggregate;
using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using Inclub.Infrastructure;
using Inclub.Infrastructure.Repositories;

namespace Inclub.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;
        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Queries
            builder.Register(c => new UsuarioQueries(QueriesConnectionString))
                .As<IUsuarioQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new ProductoQueries(QueriesConnectionString))
                .As<IProductoQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new CompraQueries(QueriesConnectionString))
                .As<ICompraQueries>()
                .InstancePerLifetimeScope();

            builder.Register(c => new CompraDetalleQueries(QueriesConnectionString))
                .As<ICompraDetalleQueries>()
                .InstancePerLifetimeScope();
            #endregion

            #region Repository
            builder.RegisterType<UsuarioRepository>()
            .As<IUsuarioRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<ProductoRepository>()
            .As<IProductoRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<CompraRepository>()
            .As<ICompraRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<CompraDetalleRepository>()
            .As<ICompraDetalleRepository>()
            .InstancePerLifetimeScope();
            #endregion

            #region Unidad de Trabajo
            builder.RegisterType<UnitOfWork>().InstancePerLifetimeScope();
            #endregion
        }
    }
}

