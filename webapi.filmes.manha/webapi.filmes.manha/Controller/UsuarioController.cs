using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webapi.filmes.manha.Domains;
using webapi.filmes.manha.Interfaces;
using webapi.filmes.manha.Repositories;

namespace webapi.filmes.manha.Controller
{
    //Define que a rota de uma requisição será no seguinte formato 
    //dominio/api/nomeController 
    //exemplo: http://localhost:5000/api/genero
    [Route("api/[controller]")]

    //Definir que é um controlador APi
    [ApiController]

    //Definir que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    //Métodos controlador que herda da controller base
    //Onde seá criado os Endopoins(Rotas)
    public class UsuarioController : ControllerBase
    {

        private IUsuarioRepository _usuarioRepository;

        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        
        }



        /// <summary>
        /// Endponit para Logar
        /// </summary>
        /// <param name="email"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(UsuarioDomains usuario)
        {
            try
            {
                UsuarioDomains usuarioBuscado = _usuarioRepository.Logar(usuario.Email, usuario.Senha);


                if (usuario == null)
                {
                    return NotFound("Email ou Senha inválido");
                }

                //Caso encontre o usuário, prossegue para a criaçõa do token

                //Primeiro - Definir as informações(Claims) que serçai forncidos no token(PAYLOAD)
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role,usuarioBuscado.Permissao),

                    //existe a possibilidade de criar uma claim personalizada
                    new Claim("Claim Personalizada", "Valor da Claim Personalizada")
                };

                //Segundo - Definir a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chaves-autenticacao-webapi-dev"));

                //Terceiro - Definir as credenciais do token (HEADER)
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //Quarto - Gerar Token
                var token = new JwtSecurityToken
                    (
                        //Emissor do Token, Quem está emitindo esse Token
                        issuer: "webapi.filmes.manha", //Aqui coloca o nome do Projeto

                        //Destinatário do token 
                        audience: "webapi.filmes.manha",

                        //dados definidos nas Claims(informações)
                        claims: claims,

                        //tempo de expirações do token
                        expires: DateTime.Now.AddMinutes(5),

                        //credenciais do token 
                        signingCredentials: creds
                    );

                //Quinta - retornar o token criado
                return Ok(new
                {
                
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch(Exception error )
            {
                return BadRequest(error.Message);
            }


        }
    }
}
