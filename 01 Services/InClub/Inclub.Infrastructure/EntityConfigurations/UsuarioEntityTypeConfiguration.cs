using Inclub.Domain.AggregatesModel.UsuarioAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inclub.Infrastructure.EntityConfigurations
{

    class UsuarioEntityTypeConfiguration :  EntityBaseTypeConfiguration<Usuario>
	{
		public override void Configure(EntityTypeBuilder<Usuario> UsuarioConfiguration)
		{
			UsuarioConfiguration.ToTable("usuario", Schema.Dbo);
			UsuarioConfiguration.HasKey(x => x.Id);
			UsuarioConfiguration.Property(x => x.Id).HasColumnName("ID_USUARIO").ValueGeneratedOnAdd();
			UsuarioConfiguration.Ignore(b => b.DomainEvents);

			// Mapping
			UsuarioConfiguration.Property(c => c.UidUsuario).HasColumnName("UID_USUARIO").IsRequired();
			UsuarioConfiguration.Property(c => c.PasswordHash).HasColumnName("PASSWORD_HASH").IsRequired();
			UsuarioConfiguration.Property(c => c.NombreUsuario).HasColumnName("NOMBRE_USUARIO").IsRequired(false);
			UsuarioConfiguration.Property(c => c.EsEliminado).HasColumnName("ES_ELIMINADO").IsRequired();

			// Auditoria
			base.Configure(UsuarioConfiguration);
		}
	}
}
