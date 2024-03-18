using FluentValidation;
using FluentValidation.Results;
using Framework.Validation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Domain.Dtos;
using User.Domain.Services;

namespace User_GraphQL.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class AuthQuery
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private IValidator<LoginInputDto> _validator;
        public AuthQuery(IValidator<LoginInputDto> validator, IConfiguration configuration, IUserService service)
        {
            _configuration = configuration;
            _userService = service;
            _validator = validator;
        }
        public string Hello()
        {
            return "Hello GraphQL";
        }



        public async Task<LoginDto> LoginAsync(LoginInputDto dtoInput)
        {
            ValidationResult resultVal = await _validator.ValidateAsync(dtoInput);
            if (!resultVal.IsValid)
            {
                throw new GraphQLException(ValidationError.Create(resultVal));
            }
            else
            {
                LoginDto result = await _userService.Login(dtoInput);
                if (result.Id != new Guid())
                {
                    var claims = new List<Claim>()
                    {
                    new Claim(ClaimTypes.Name, result.UserName),
                    new Claim("FullName", result.FullName),
                    new Claim("Id", result.Id.ToString())
                };
                    foreach (var item in result.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.ToLower()));
                    }

                    var token = GetToken(claims);
                    result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    result.Expiration = token.ValidTo;
                    return result;
                }
                else
                {
                    throw new GraphQLException("Credential not Match");
                }
            }

            return new LoginDto();
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? "None"));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:ExpireDays"])),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
