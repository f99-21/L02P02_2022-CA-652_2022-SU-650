using L02P02_2022_CA_652_2022_SU_650.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2022_CA_652_2022_SU_650.Controllers
{
    public class clientesController : Controller
    {
        private readonly libreriaDbContext _context;

        public clientesController(libreriaDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("agregarCliente");
        }

        public IActionResult agregarCliente() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult agregarCliente(string nombre, string apellido, string email, string direccion) 
        {
            var nuevoCliente = new clientes
            {
                nombre = nombre,
                apellido = apellido,
                email = email,
                direccion = direccion,
                created_at = DateTime.Now
            };

            _context.clientes.Add(nuevoCliente);
            _context.SaveChanges();

            HttpContext.Session.SetString("ClienteNombre", nuevoCliente.nombre);
            HttpContext.Session.SetString("ClienteApellido", nuevoCliente.apellido);
            HttpContext.Session.SetString("ClienteEmail", nuevoCliente.email);
            HttpContext.Session.SetString("ClienteDireccion", nuevoCliente.direccion);

            var pedido = new pedido_encabezado
            {
                id_cliente = nuevoCliente.id,
                cantidad_libros = 0,
                total = 0
            };

            _context.pedido_encabezado.Add(pedido);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("PedidoId", pedido.id);

            //Cambiar a controlador de pedidos
            return RedirectToAction("seleccionarLibro", "pedido_encabezado");
        }
    }
}
