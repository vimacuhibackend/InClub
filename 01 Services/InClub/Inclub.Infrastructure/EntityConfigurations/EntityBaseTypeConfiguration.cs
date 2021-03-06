using Inclub.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inclub.Infrastructure.EntityConfigurations
{
    public class EntityBaseTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Ignore(b => b.DomainEvents);
			builder.Ignore(x => x.Errors);
			builder.Property(x => x.GuidValue).HasColumnName("GUID").IsRequired();
			builder.Property(x => x.UsuarioCreacion).HasColumnName("USUARIO_CREACION").IsRequired();
			builder.Property(x => x.FechaCreacion).HasColumnName("FECHA_CREACION").IsRequired();
			builder.Property(x => x.IpCreacion).HasColumnName("IP_CREACION").IsRequired();
			builder.Property(x => x.UsuarioModificacion).HasColumnName("USUARIO_MODIFICACION").IsRequired(false);
			builder.Property(x => x.FechaModificacion).HasColumnName("FECHA_MODIFICACION").IsRequired(false);
			builder.Property(x => x.IpModificacion).HasColumnName("IP_MODIFICACION").IsRequired(false);
		}
	}
}
