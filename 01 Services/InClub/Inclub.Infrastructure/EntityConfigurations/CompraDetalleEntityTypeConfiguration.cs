using Inclub.Domain.AggregatesModel.CompraAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inclub.Infrastructure.EntityConfigurations
{

    class CompraDetalleEntityTypeConfiguration :  EntityBaseTypeConfiguration<CompraDetalle>
	{
		public override void Configure(EntityTypeBuilder<CompraDetalle> CompraDetalleConfiguration)
		{
			CompraDetalleConfiguration.ToTable("compra_detalle", Schema.Dbo);
			CompraDetalleConfiguration.HasKey(x => x.Id);
			CompraDetalleConfiguration.Property(x => x.Id).HasColumnName("ID_COMPRA_DETALLE").ValueGeneratedOnAdd();
			CompraDetalleConfiguration.Ignore(b => b.DomainEvents);

			// Mapping
			CompraDetalleConfiguration.Property(c => c.IdCompra).HasColumnName("ID_COMPRA").IsRequired();
			CompraDetalleConfiguration.Property(c => c.Cantidad).HasColumnName("CANTIDAD").IsRequired();
			CompraDetalleConfiguration.Property(c => c.IdProducto).HasColumnName("ID_PRODUCTO").IsRequired();
			CompraDetalleConfiguration.Property(c => c.PrecioUnitario).HasColumnName("PRECIO_UNITARIO").IsRequired();
			CompraDetalleConfiguration.Property(c => c.PrecioTotal).HasColumnName("PRECIO_TOTAL").IsRequired();
			CompraDetalleConfiguration.Property(c => c.EsEliminado).HasColumnName("ES_ELIMINADO").IsRequired();

			// Auditoria
			base.Configure(CompraDetalleConfiguration);
		}
	}
}
