using Inclub.Domain.AggregatesModel.ProductoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inclub.Infrastructure.EntityConfigurations
{

    class ProductoEntityTypeConfiguration :  EntityBaseTypeConfiguration<Producto>
	{
		public override void Configure(EntityTypeBuilder<Producto> ProductoConfiguration)
		{
			ProductoConfiguration.ToTable("producto", Schema.Dbo);
			ProductoConfiguration.HasKey(x => x.Id);
			ProductoConfiguration.Property(x => x.Id).HasColumnName("ID_PRODUCTO").ValueGeneratedOnAdd();
			ProductoConfiguration.Ignore(b => b.DomainEvents);

			// Mapping
			ProductoConfiguration.Property(c => c.CodigoProducto).HasColumnName("CODIGO_PRODUCTO").IsRequired();
			ProductoConfiguration.Property(c => c.NombreProducto).HasColumnName("NOMBRE_PRODUCTO").IsRequired();
			ProductoConfiguration.Property(c => c.StockInicial).HasColumnName("STOCK_INICIAL").IsRequired();
			ProductoConfiguration.Property(c => c.StockActual).HasColumnName("STOCK_ACTUAL").IsRequired();
			ProductoConfiguration.Property(c => c.PrecioVenta).HasColumnName("PRECIO_VENTA").IsRequired();
			ProductoConfiguration.Property(c => c.EsEliminado).HasColumnName("ES_ELIMINADO").IsRequired();

			// Auditoria
			base.Configure(ProductoConfiguration);
		}
	}
}
