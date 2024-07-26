using EmprestimosBase.Models;

namespace EmprestimosBase.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoveSessao();
    }
}
