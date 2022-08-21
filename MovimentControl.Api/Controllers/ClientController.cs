using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MovimentControl.Api.Repository;
using MovimentControl.Api.Models;
using MovimentControl.Api.Validations;
using MovimentControl.Api.Entities;
using MovimentControl.Api.Services;
using Microsoft.AspNetCore.Authorization;
using SendGrid.Helpers.Mail;
using MovimentControl.Api.Persistence.Repository;
using SendGrid;
using MovimentControl.Api.Interfaces;

namespace MovimentControl.Api.Controllers
{

    [Route("api/movimentcontrol")]
    [ApiController]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly ClientValidator validator;
        private readonly IUserService userService;

        private IConfiguration configuration;
        public ClientController(ClientValidator validator, IConfiguration configuration, IUserService userService, IClientService clientService)
        {

            this.validator = validator;
            this.configuration = configuration;
            this.userService = userService;
            this.clientService = clientService;
        }
        /// <summary>
        /// Realiza o login do usuário no sistema
        /// </summary>
        /// <param name="username">Usuário do sistema</param>
        /// <param name="password">Senha do usuário</param>
        /// <returns>Retorna o token JWT que será usado na navegação entre as páginas e o id que será usado na autenticação de dois fatores</returns>
        /// <response code="200"> Login do usuário do sistema foi bem concluido</response>

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(string username, string password)
        {


            var tokenString = TokenService.GenerateToken(username, configuration);

            var model = userService.GetUser(username, password);
            var token2Fa = RandomTokenService.GenerateToken2Fa();
            model.Token = token2Fa;
            userService.Update(model);
            userService.SendEmail(token2Fa, model);
            return Ok(new { token = tokenString, id = model.Id });
        }
        /// <summary>
        /// Realiza a autenticação de dois fatores do usuário no sistema
        /// </summary>
        /// <param name="user">Informações do usuário</param>
        /// <param name="id">Id do usuário</param>
        /// <returns>Retorna o token que é usado na autenticação de dois fatores</returns>
        /// <response code="200">A autenticação do usuário foi bem sucedida</response>
        /// <response code="401">O usuário passado é invalido</response>
        /// <response code="403">O token passado é invalido</response>
        [HttpPost("login2fa/{id}")]
        public IActionResult Login2Fa(User user,int id)
        {
            var model = userService.GetById(id);
            if (RandomTokenService.VerifyDate(model, user.LoggedTime.ToLocalTime()))
            {
                var token = user.Token;
                if (RandomTokenService.VerifyToken(token, model))
                {
                    return Ok(new { token = token });
                }
                return Forbid();
            }
            return Unauthorized();
        }


        /// <summary>
        /// Retorna todos os clientes cadastrados no sistema
        /// </summary>
        /// <returns>Todos os objetos criado pelo usuário</returns>
        /// <response code="200">Retorno das informações foi bem-sucedida</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var model = clientService.GetAll();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return NotFound($"Erro {ex.Message}");
            }


        }
        /// <summary>
        /// Retorna o cliente de acordo com o Id que foi passado na url
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns>Retorna o objeto</returns>
        /// <response code="200">Cliente retornado com sucesso </response>
        /// <response code="404">Cliente não encontrado </response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var client = clientService.GetById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        /// <summary>
        /// Cadastro de um cliente.
        /// </summary>
        /// <remarks>
        /// {
        ///     "fullName": "Gustavo Silveira",
        ///     "city": "Cajamar",
        ///     "cep": "07776435",
        ///     "district": "Jordanesia",
        ///     "state": "SP",
        ///     "address": "Avenida Deovair Cruz de Oliveira",
        ///     "cpf": "12481886089",
        ///     "rg": "4203171193",
        ///     "gender": 1,
        ///     "phone": "71923139139",
        ///     "career": "Segurança",
        ///     "email": "gift4444@outlook.com",
        ///     "service": 1,
        ///     "details": "Dores nas Costas e nas Pernas,Mal postura",
        ///     "healthI": 4,
        ///     "mCivil": 2
        /// }
        /// </remarks>
        /// <param name="model">Dados do cliente</param>
        /// <returns>Retorna os dados do cliente recém-criado</returns>
        /// <response code="201">Objeto criado com sucesso</response>
        /// <response code="400">Objeto inválido</response>
        [HttpPost]
        public IActionResult Post(ClientInputModel model)
        {
            ValidationResult validationResult = validator.Validate(model);
            if (!validationResult.IsValid) return BadRequest();

            clientService.Add(model);

            return CreatedAtAction("GetById",
            new { id = model.Id },
                model);
        }

        /// <summary>
        /// Modificação de informações de um cliente
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <param name="model">Dados do cliente</param>
        /// <returns>Retorna os dados do cliente modificado</returns>
        /// <response code="204">Cliente modificado com sucesso</response>
        /// <response code="400">Cliente inválido</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, ClientInputModel model)
        {

            var client = clientService.GetById(id);
            if (client == null) return NotFound();

            ValidationResult validationResult = validator.Validate(model);
            if (!validationResult.IsValid) return BadRequest();

            clientService.Update(model);
            return NoContent();


        }
        /// <summary>
        /// Exclusão de um cliente
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns>Cliente excluído com sucesso</returns>
        /// <response code="200">Cliente excluído com sucesso</response>
        /// <response code="404">Cliente não encontrado</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = clientService.GetById(id);
            if (model == null) return NotFound();

            clientService.Delete(model);
            return Ok("Cliente excluído com sucesso");



        }
    }

}