using ContatoMVCcomADO.Models;
using ContatoMVCcomADO.Repositorio;
using System;
using System.Web.Mvc;

namespace ContatoMVCcomADO.Controllers
{
    public class ContatoController : Controller
    {
        // GET: Contato
        private ContatoRepositorio _repositorio;

        //Primeira Action a ser aberta ObterContato
        //Método get mostra pela URL.
        [HttpGet]
        public ActionResult ObterContato()
        {
            _repositorio = new ContatoRepositorio();
            ModelState.Clear();
            return View(_repositorio.ObterContato());
        }

        //Obs: Os métodos abaixo estão com  mesmo nome porém com assinaturas diferentes
        //para fazerem funções diferentes.
        //Incluindo os contatos.
        //Método get acionado pela URL que mostra os dados na tela através do return..
        [HttpGet]
        public ActionResult IncluirContato()
        {
            return View();
        }

        //Incluindo os contatos.
        //Método post acionado ao clicar no botão gravar os dados dos campos.
        [HttpPost]
        public ActionResult IncluirContato(Contato contatoObj)
        {
            //tratamento de erro
            try
            {
                //Se todos os dados foram devidamente informados será feita a inclusão.
                if (ModelState.IsValid)
                {
                    //Instanciando o Time.
                    _repositorio = new ContatoRepositorio();

                    if (_repositorio.AdicionarContato(contatoObj))
                    {
                        //ViewBag para mostrar a mensagem de cadastrado com sucesso.
                        ViewBag.Mensagem = "Contato cadastrado com sucesso";

                        //Método para Limpar os Campos.
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch (Exception)
            {
                //Retorna para a Pagina que mostra os contatos.
                return View("ObterContato");
            }
        }

        //Editando os contatos.
        //Método get acionado pela URL que mostra os dados na tela através do return..
        [HttpGet]
        //Passando por parâmetro o valor do registro.
        public ActionResult EditarContato(int id)
        {
            _repositorio = new ContatoRepositorio();

            //Return com método lambda para receber somente o time com o id.
            return View(_repositorio.ObterContato().Find(t => t.Id == id));
        }

        //Incluindo os contatos.
        //Método post acionado ao clicar no botão editar os dados dos campos.
        [HttpPost]
        //Método recebe os parâmetros do id e o objeto do Time.
        public ActionResult EditarContato(int id, Contato contatoObj)
        {
            //tratamento de erro
            try
            {
                //Criando o objeto repositorio
                _repositorio = new ContatoRepositorio();
                //Método atualizar rece um time.
                _repositorio.AtualizarContato(contatoObj);
                //Após atualizar retorna para a action ObterContato.
                return RedirectToAction("ObterContato");

            }
            catch (Exception)
            {
                return View("ObterContato");
            }
        }

        //Excluindo os contatos.
        //Método post acionado ao clicar no botão excluir os dados dos campos.
        public ActionResult ExcluirContato(int id)
        {
            try
            {
                _repositorio = new ContatoRepositorio();
                if (_repositorio.ExcluirTime(id))
                {
                    ViewBag.Mensagem = "Contato Excluido com sucesso";
                }

                return RedirectToAction("ObterContato");
            }
            catch (Exception)
            {
                return View("ObterContato");
            }
        }
    }
}