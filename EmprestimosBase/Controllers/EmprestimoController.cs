using ClosedXML.Excel;
using EmprestimosBase.Data;
using EmprestimosBase.Models;
using EmprestimosBase.Services.EmprestimosService;
using EmprestimosBase.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmprestimosBase.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ISessaoInterface _sessaoInterface;
        private readonly IEmprestimosInterterface _emprestimosInterterface;

        public EmprestimoController(
                                    IEmprestimosInterterface emprestimosInterterface
                                    , ISessaoInterface sessaoInterface)
        {
            _sessaoInterface = sessaoInterface;
            _emprestimosInterterface = emprestimosInterterface;
        }

        public async Task<IActionResult> Index()
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if(usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimos = await _emprestimosInterterface.BuscarEmprestimos();
           
            return View(emprestimos.Dados);
        }

        public IActionResult Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        public async Task<IActionResult> Editar(int? id)
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimosInterterface.BuscarEmprestimosPorId(id);

            return View(emprestimo.Dados);
        }

        public async Task<IActionResult> Excluir(int? id)
        {

            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimosInterterface.BuscarEmprestimosPorId(id);
           
            return View(emprestimo.Dados);

        }

        [HttpGet]

        public async Task<IActionResult> Exportar()
        {
            var dados = await _emprestimosInterterface.BuscaDadosEmprestimoExcel();

            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados, "Dados Empréstimos");
                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimo.xls");

                }

            }
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {

                var emprestimoResult = await _emprestimosInterterface.CadastrarEmprestimo(emprestimo);

                if (emprestimoResult.Status)
                {
                    TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = emprestimoResult.Mensagem;
                    return View(emprestimo);
                }

                return RedirectToAction("Index");
            }

            return View();
            
        }

        [HttpPost] 
        public async Task<IActionResult> Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {

                var emprestimoResullt = await _emprestimosInterterface.EditarEmprestimo(emprestimo);

                if (emprestimoResullt.Status) {
                    TempData["MensagemSucesso"] = emprestimoResullt.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = emprestimoResullt.Mensagem;
                    return View(emprestimo);
                }
              
                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Ocorreu algum erro no momento da edição!";
            return View(emprestimo);
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(EmprestimosModel emprestimo)
        {
            if ( emprestimo == null)
            {
                TempData["MensagemErro"] = "Emprestimo não localizado";
                return View(emprestimo);
            }

            var emprestimoResult = await _emprestimosInterterface.RemoveEmprestimo(emprestimo);

            if (emprestimoResult.Status) {
                TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
            }
            else
            {
                TempData["MensagemErro"] = emprestimoResult.Mensagem;
                return View(emprestimo);
            }

            
            return RedirectToAction("Index");

        }



    }
}
