using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MovimentControl.Api.Repository;
using MovimentControl.Api.Models;
using MovimentControl.Api.Validations;

namespace MovimentControl.Api.Controllers
{
    [ApiController]
    [Route("api/movimentcontrol")]

    public class ClientController : ControllerBase
    {
        public readonly IClientRepository repository;
        public readonly ClientValidator validator;


        public ClientController(IClientRepository _repo, ClientValidator validations)
        {
            repository = _repo;
            validator = validations;
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
                var model = repository.GetAll();
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
            var client = repository.GetById(id);
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

            repository.Add(model);

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

            var client = repository.GetById(id);
            if (client == null) return NotFound();

            ValidationResult validationResult = validator.Validate(model);
            if (!validationResult.IsValid) return BadRequest();

            repository.Update(model);
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
            var model = repository.GetById(id);
            if (model == null) return NotFound();

            repository.Delete(model);
            return Ok("Cliente excluído com sucesso");



        }
    }

}