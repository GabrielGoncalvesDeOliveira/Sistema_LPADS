using SistemaGestaoCantinasIgrejas.Models;
using SistemaGestaoCantinasIgrejas.Models.Consulta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestaoCantinasIgrejas.Controllers.Extra;
using System.Data;

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

            var PivotTableVenda = lstGrpVendas.ToList().ToPivotTable(
                        pivo => pivo.mes,
                        pivo => pivo.nome,
                        pivo => pivo.Any() ? pivo.Sum(x => x.valor) : 0);

            List<PivotMes> lista = new List<PivotMes>();
            lista = (from DataRow coluna in PivotTableVenda.Rows
                     select new PivotMes()
                     {
                         nome = Convert.ToString(coluna[0]),
                         mes1 = Convert.ToSingle(coluna[1]),
                         mes2 = Convert.ToSingle(coluna[2]),
                         //mes3 = Convert.ToSingle(coluna[3]),
                         //mes4 = Convert.ToSingle(coluna[4]),
                         //mes5 = Convert.ToSingle(coluna[5]),
                         //mes6 = Convert.ToSingle(coluna[6]),
                     }
            ).ToList();

            return View(lista);
        }
    }
}
