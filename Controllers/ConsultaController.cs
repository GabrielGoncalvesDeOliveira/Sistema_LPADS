using SistemaGestaoCantinasIgrejas.Models;
using SistemaGestaoCantinasIgrejas.Models.Consulta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaGestaoCantinasIgrejas.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly Contexto contexto;

        public ConsultaController(Contexto context)
        {
            contexto = context;
        }


        public IActionResult Pesquisa()
        {
            return View();
        }

        //[HttpGet("/Consulta/ListarItens/{participanteId}")]
        public IActionResult Geral(string nome)
        {
            List<Participante> lista = new List<Participante>();
            if (nome != null)
            {
                lista = contexto.participante.OrderBy(o => o.nome)
                    .Where(c => c.nome.Contains(nome)).ToList();
            }
            else
            {
                lista = contexto.participante.OrderBy(o => o.nome).ToList();
            }

            return View(lista);
        }

        public IActionResult Participantes(string nome)
        {
            List<Participante> lista = new List<Participante>();
            if (nome != null)
            {
                lista = contexto.participante.OrderBy(o => o.nome)
                 .Where(c => c.nome.Contains(nome)).ToList();
            }
            else
            {
                lista = contexto.participante.OrderBy(o => o.nome).ToList();
            }

            return View(lista);
        }

        public IActionResult Agrupar()
        {

            IEnumerable<VendasGrp> lstGrpVendas = from item in contexto.venda
                                   .Include(v => v.participante).Include(v => v.produto)
                                   .ToList()
                                                    group item by new { item.participante.nome, item.produto.descricao }
                                   into grupo
                                                    orderby grupo.Key.nome, grupo.Key.descricao
                                                    select new VendasGrp
                                                    {
                                                        participante = grupo.Key.nome,
                                                        produto = grupo.Key.descricao,
                                                        valor = grupo.Sum(p => p.quantidade * p.valor)
                                                    };

            return View(lstGrpVendas);

        }

        public IActionResult AgruparByNome()
        {

            IEnumerable<VendasGrp> lstGrpVendas= from item in contexto.venda
                                   .Include(v => v.participante).Include(v => v.produto)
                                   .ToList()
                                                    group item by new { item.participante.nome }
                                   into grupo
                                                    orderby grupo.Key.nome
                                                    select new VendasGrp
                                                    {
                                                        participante = grupo.Key.nome,
                                                        valor = grupo.Sum(v => v.quantidade * v.valor)
                                                    };

            return View(lstGrpVendas);

        }

        public IActionResult AgruparByMes()
        {
            IEnumerable<VendasMes> lstGrpVendas = from item in contexto.venda
                                   .Include(v => v.participante).Include(v => v.produto)
                                   .ToList()
                                                  group item by new { item.participante.nome, item.data.Month }
                                   into grupo
                                                  orderby grupo.Key.nome
                                                  orderby grupo.Key.Month
                                                  select new VendasMes
                                                  {
                                                      nome = grupo.Key.nome,
                                                      mes = grupo.Key.Month,
                                                      valor = grupo.Sum(v => v.quantidade * v.valor)
                                                  };

            return View(lstGrpVendas);
        }

        public IActionResult PivotByMes()
        {
            IEnumerable<VendasMes> lstGrpVendas = from item in contexto.venda
                                   .Include(v => v.participante).Include(v => v.produto)
                                   .ToList()
                                                  group item by new { item.participante.nome, item.data.Month }
                                   into grupo
                                                  orderby grupo.Key.nome
                                                  orderby grupo.Key.Month
                                                  select new VendasMes
                                                  {
                                                      nome = grupo.Key.nome,
                                                      mes = grupo.Key.Month,
                                                      valor = grupo.Sum(v => v.quantidade * v.valor)
                                                  };

            return View(lstGrpVendas);
        }
    }
}
