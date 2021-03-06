using Inclub.Domain.AggregatesModel.CompraAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inclub.Infrastructure.EntityConfigurations
{

    class CompraEntityTypeConfiguration :  EntityBaseTypeConfiguration<Compra>
	{
		public override void Configure(EntityTypeBuilder<Compra> CompraConfiguration)
		{
			CompraConfiguration.ToTable("compra", Schema.Dbo);
			CompraConfiguration.HasKey(x => x.Id);
			CompraConfiguration.Property(x => x.Id).HasColumnName("ID_COMPRA").ValueGeneratedOnAdd();
			CompraConfiguration.Ignore(b => b.DomainEvents);

			// Mapping
			CompraConfiguration.Property(c => c.IdUsuario).HasColumnName("ID_USUARIO").IsRequired();
			CompraConfiguration.Property(c => c.Subtotal).HasColumnName("SUBTOTAL").IsRequired();
			CompraConfiguration.Property(c => c.Total).HasColumnName("TOTAL").IsRequired();
			CompraConfiguration.Property(c => c.EsEliminado).HasColumnName("ES_ELIMINADO").IsRequired();

			// Auditoria
			base.Configure(CompraConfiguration);
		}
	}
}
