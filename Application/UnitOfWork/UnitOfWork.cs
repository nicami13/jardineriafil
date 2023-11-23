using System;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using persistense.Data;
using System;
using System.Threading.Tasks;


namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly JardineriaContext _context;

        private ICliente _clientes;
        private IDetallePedido _detallespedidos;
        private IEmpleado _empleados;
        private IGamaProducto _gamaproductos;
        private IOficina _oficinas;
        private IPago _pagos;
        private IPedido _pedidos;
        private IProducto _productos;

        private IJefe _jefes;
        public UnitOfWork(JardineriaContext context)
        {
            _context = context;
        }
        
        public IJefe Jefes
        {
            get
            {
                if(_jefes==null)
                {
                    _jefes = new JefeRepository(_context);
                }
                return _jefes;
            }
        }


        public ICliente Clientes
        {
            get
            {
                return _clientes ??= new ClienteRepository(_context);
            }
        }

        public IDetallePedido DetallesPedidos
        {
            get
            {
                return _detallespedidos ??= new DetallePedidoRepository(_context);
            }
        }

        public IEmpleado Empleados
        {
            get
            {
                return _empleados ??= new EmpleadoRepository(_context);
            }
        }

        public IGamaProducto GamaProductos
        {
            get
            {
                return _gamaproductos ??= new GamaProductoRepository(_context);
            }
        }

        public IOficina Oficinas
        {
            get
            {
                return _oficinas ??= new OficinaRepository(_context);
            }
        }

        public IPago Pagos
        {
            get
            {
                return _pagos ??= new PagoRepository(_context);
            }
        }

        public IPedido Pedidos
        {
            get
            {
                return _pedidos ??= new PedidoRepository(_context);
            }
        }

        public IDetallePedido DetallePedidos
        {
            get
            {
                return _detallespedidos ??= new DetallePedidoRepository(_context);
            }
        }

        public IProducto Productos
        {
            get
            {
                return _productos ??= new ProductoRepository(_context);
            }
        }


        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades.
                throw new ApplicationException("Error al guardar cambios en la base de datos.", ex);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
