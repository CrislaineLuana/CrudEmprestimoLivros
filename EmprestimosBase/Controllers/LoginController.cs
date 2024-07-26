using EmprestimosBase.Dto;
using EmprestimosBase.Services.LoginService;
using EmprestimosBase.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosBase.Controllers
{
    public class LoginController : Controller
    {

        private readonly ILoginInterface _loginInterface;
        private readonly ISessaoInterface _sessaoInterface;
        public LoginController(ILoginInterface loginInterface, ISessaoInterface sessaoInterface)
        {
            _loginInterface = loginInterface;
            _sessaoInterface = sessaoInterface;
        }

        public IActionResult Login()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }
        public IActionResult Logout()
        {
            _sessaoInterface.RemoveSessao();
            return RedirectToAction("Login");
        }


        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegisterDto usuarioRegisterDto)
        {
            if (ModelState.IsValid) {

                var usuario = await _loginInterface.RegistrarUsuario(usuarioRegisterDto);

                if (usuario.Status) {

                    TempData["MensagemSucesso"] = usuario.Mensagem;

                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioRegisterDto);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(usuarioRegisterDto);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (ModelState.IsValid) {

                var usuario = await _loginInterface.Login(usuarioLoginDto);

                if (usuario.Status) {

                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemErro"] = usuario.Mensagem;
                    return View(usuarioLoginDto);
                }



            }
            else
            {
                return View(usuarioLoginDto);
            }
        }
    }
}
