using ClosedXML.Excel;
using EmprestimosBase.Data;
using EmprestimosBase.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmprestimosBase.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _db;

        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel>  emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Editar(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if(emprestimo == null)
            {
                return NotFound();
            }


            return View(emprestimo);
        }

        public IActionResult Excluir(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            
            return View(emprestimo);

        }

<<<<<<< Updated upstream
        [HttpGet]
=======
 
>>>>>>> Stashed changes
        public IActionResult Exportar()
        {
            var dados = GetDados();

<<<<<<< Updated upstream
            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados, "Dados Empréstimos");
                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimo.xls");
=======
            using (XLWorkbook workBook = new XLWorkbook()) 
            {
                workBook.AddWorksheet(dados,"Dados Empréstimo");

                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet","Emprestimo.xls");
>>>>>>> Stashed changes
                }

            }
        }

        private DataTable GetDados()
        {
<<<<<<< Updated upstream
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados emprestimos";
=======

            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados empréstimos";
>>>>>>> Stashed changes

            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
<<<<<<< Updated upstream
            dataTable.Columns.Add("Data última atualização", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if (dados.Count > 0)
=======
            dataTable.Columns.Add("Data empréstimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0)
>>>>>>> Stashed changes
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataEmprestimo);
                });
            }

            return dataTable;
<<<<<<< Updated upstream

        }

=======
        }


>>>>>>> Stashed changes

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                emprestimo.DataEmprestimo = DateTime.Now;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
                _db.Emprestimos.Add(emprestimo);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                return RedirectToAction("Index");
            }

            return View();
            
        }

        [HttpPost] 
        public IActionResult Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                var emprestimoDB = _db.Emprestimos.Find(emprestimo.Id);

<<<<<<< Updated upstream
                emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;
                emprestimoDB.Recebedor = emprestimo.Recebedor;
                emprestimoDB.Fornecedor = emprestimo.Fornecedor;
=======
                emprestimoDB.Fornecedor = emprestimo.Fornecedor;
                emprestimoDB.Recebedor = emprestimo.Recebedor;
                emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;
>>>>>>> Stashed changes

                _db.Emprestimos.Update(emprestimoDB);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição realizada com sucesso!";

                return RedirectToAction("Index");
            }

            TempData["MensagemErro"] = "Ocorreu algum erro no momento da edição!";
            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimo)
        {
            if ( emprestimo == null)
            {
                return NotFound();
            }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();
            TempData["MensagemSucesso"] = "Remoção realizada com sucesso!";
            return RedirectToAction("Index");

        }



    }
}
