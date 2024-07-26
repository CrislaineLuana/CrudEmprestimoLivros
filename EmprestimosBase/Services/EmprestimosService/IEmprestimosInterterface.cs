using EmprestimosBase.Models;
using System.Data;

namespace EmprestimosBase.Services.EmprestimosService
{
    public interface IEmprestimosInterterface
    {
        Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos();
        Task<ResponseModel<EmprestimosModel>> BuscarEmprestimosPorId(int? id);
        Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimo(EmprestimosModel emprestimosModel);
        Task<ResponseModel<EmprestimosModel>> EditarEmprestimo(EmprestimosModel emprestimosModel);
        Task<ResponseModel<EmprestimosModel>> RemoveEmprestimo(EmprestimosModel emprestimosModel);
       Task<DataTable> BuscaDadosEmprestimoExcel(); 
    }
}
