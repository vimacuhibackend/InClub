using Inclub.Domain.AggregatesModel;
using Inclub.Domain.AggregatesModel.ProductoAggregate;
using Inclub.Domain.Validations;
using Inclub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Inclub.Api.Application.Validations
{
	public class ProductoValidationContextGenerator
	{
		private IGenericRepository<Producto> _productoRepository;
		private UnitOfWork _unitOfWork;

		public ProductoValidationContextGenerator(UnitOfWork unitOfWork, IGenericRepository<Producto> productoRepository)
		{
			_productoRepository = productoRepository;
			_unitOfWork = unitOfWork;
		}

		public ProductoValidationContext GenerarModelo(Producto producto, OperacionProducto operacion)
		{
			var context = new ProductoValidationContext();
			context.operacion = operacion;
			switch (operacion)
			{
				case OperacionProducto.Registrar:
				break;
				case OperacionProducto.Modificar:
					context.productoEstadoActual = _productoRepository.Get(x => x.Id == producto.Id, asNoTracking: true).FirstOrDefault();
				break;
				case OperacionProducto.Eliminar:
					context.productoEstadoActual = _productoRepository.Get(x => x.Id == producto.Id, asNoTracking: true).FirstOrDefault();
				break;
			}
			return context;
		}
	}
}
