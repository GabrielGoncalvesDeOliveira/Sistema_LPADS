using Microsoft.AspNetCore.Mvc;
using SistemaGestaoCantinasIgrejas.Models;

namespace SistemaGestaoCantinasIgrejas.Controllers
{
    public class QueryController : Controller
    {
        private readonly Contexto contexto;

        public QueryController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult Participante(string filtro)
        {
            List<Participante> lista = new List<Participante>();

            if (filtro == null)
            {
                lista = contexto.participante
                         .OrderBy(o => o.nome)
                         .ToList();

            }
            else
            {
                // lista = contexto.clientes.Where(c => c.nome == nome)
                //lista = contexto.clientes.Where(c => c.cidade == cidade)
                lista = contexto.participante.Where(c => c.nome.Contains(filtro))
                        .OrderBy(o => o.nome)
                        .ToList();
            }

            return View(lista);

        }

        public IActionResult Pesquisa()
        {
            return View();
        }
    }
}