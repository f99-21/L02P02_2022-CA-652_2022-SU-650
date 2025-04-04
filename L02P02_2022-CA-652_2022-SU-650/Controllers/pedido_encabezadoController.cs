using L02P02_2022_CA_652_2022_SU_650.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2022_CA_652_2022_SU_650.Controllers
{
    public class pedido_encabezadoController : Controller
    {
        private readonly libreriaDbContext _context;

        public pedido_encabezadoController(libreriaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult seleccionarLibro() 
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (pedidoId != null) 
            {
                var librosSeleccionados = (from pd in _context.pedido_detalle
                                           join l in _context.libros on pd.id_libro equals l.id
                                           where pd.id_pedido == pedidoId
                                           select new
                                           {
                                               Libro = l.nombre,
                                               Precio = l.precio
                                           }).ToList();

                ViewData["librosSeleccionados"] = librosSeleccionados;
            }
            
            var listaLibros = (from l in _context.libros
                               join a in _context.autores on l.id_autor equals a.id
                               select new 
                               {
                                   id_libro = l.id,
                                   Libro = l.nombre,
                                   Autor = a.autor, 
                                   Precio = l.precio
                               }).ToList();

            ViewData["listaLibros"] = listaLibros;

            return View();
        }

        [HttpPost]
        public IActionResult agregarLibro(int id_libro) 
        {
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (pedidoId == null) 
            {
                return RedirectToAction("agregarCliente", "clientes");
            }

            var pedido_detalle = new pedido_detalle
            {
                id_pedido = pedidoId.Value,
                id_libro = id_libro,
                created_at = DateTime.Now,
            };

            _context.pedido_detalle.Add(pedido_detalle);
            _context.SaveChanges();

            return RedirectToAction("seleccionarLibro");
        }

        public IActionResult terminarVenta() 
        {
            ViewData["ClienteNombre"] = HttpContext.Session.GetString("ClienteNombre");
            ViewData["ClienteApellido"] = HttpContext.Session.GetString("ClienteApellido");
            ViewData["ClienteEmail"] = HttpContext.Session.GetString("ClienteEmail");
            ViewData["ClienteDireccion"] = HttpContext.Session.GetString("ClienteDireccion");

            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (pedidoId != null)
            {
                var librosSeleccionados = (from pd in _context.pedido_detalle
                                           join l in _context.libros on pd.id_libro equals l.id
                                           where pd.id_pedido == pedidoId
                                           select new
                                           {
                                               Libro = l.nombre,
                                               Precio = l.precio
                                           }).ToList();

                var total = librosSeleccionados.Sum(libro => libro.Precio);


                
                ViewData["librosSeleccionados"] = librosSeleccionados;
                ViewData["total"] = total;

            }

            return View();
        }

    }
}
