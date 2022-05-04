using Inclub.Domain.AggregatesModel.CompraAggregate;
using Inclub.Domain.AggregatesModel.ProductoAggregate;
using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using Inclub.Domain.SeedWork;
using Inclub.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Inclub.Infrastructure
{
    public class InclubContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraDetalle> CompraDetalle { get; set; }

        private readonly IMediator _mediator;

        private InclubContext(DbContextOptions<InclubContext> options) : base(options) { }

        public InclubContext(DbContextOptions<InclubContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("DaxsportContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CompraEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CompraDetalleEntityTypeConfiguration());
        }


        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {

            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync();
            return true;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"La Transacción {transaction.TransactionId} no es la actual");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }


    public static class Schema
    {
        public static string Dbo = "dbo";
        public static string Sistema = "sistema";
        public static string Maestro = "maestro";
        public static string Transaccional = "transaccional";
    }
}

