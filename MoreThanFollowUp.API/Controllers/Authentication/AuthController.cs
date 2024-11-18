
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.API.Interfaces;
using MoreThanFollowUp.Application.DTO.Enterprise;
using MoreThanFollowUp.Application.DTO.Login;
using MoreThanFollowUp.Application.DTO.Users;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoreThanFollowUp.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IUserApplicationRepository _userApplicationRepository;
        private readonly IApplicationUserRoleEnterpriseRepository _userRoleEnterpriseRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IEnterprise_UserRepository _enterpriseUserRepository;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, ILogger<AuthController> logger, IUserApplicationRepository userApplicationRepository, IApplicationUserRoleEnterpriseRepository userRoleEnterpriseRepository, IEnterpriseRepository enterpriseRepository, ITenantRepository tenantRepository, IEnterprise_UserRepository enterpriseUserRepository)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
            _userApplicationRepository = userApplicationRepository;
            _userRoleEnterpriseRepository = userRoleEnterpriseRepository;
            _enterpriseRepository = enterpriseRepository;
            _tenantRepository = tenantRepository;
            _enterpriseUserRepository = enterpriseUserRepository;
        }

        [HttpPost]
        [Route("CreateRole")]
        //[Authorize(Policy = "ADMIN")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            ApplicationRole role = new()
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(role);
                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Roles Added");
                    return StatusCode(StatusCodes.Status200OK, new ResponseDTO { Status = "Success", Message = $"Role {roleName} added successfully" });
                }
                else
                {
                    _logger.LogInformation(2, "Error");
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = $"Issue adding the new {roleName} role" });

                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = "Role already exist." });

        }

        [HttpPost]
        [Route("AddUserToRoleToEnterprise")]
        [Authorize(Policy = "ADMIN")]
        public async Task<IActionResult> AddUserToRoleToEnterprise(string email, string roleName, Guid enterpriseId)
        {

            var user = await _userManager.FindByEmailAsync(email);
            var role = await _roleManager.FindByNameAsync(roleName);
            var enterprise = await _enterpriseRepository.RecoverBy(p => p.EnterpriseId == enterpriseId);
            if (user != null)
            {
                //var result = await _userManager.AddToRoleAsync(user, roleName);


                var roleToUserObject = new ApplicationUserRoleEnterprise
                {
                    UserId = user.Id,
                    User = user,
                    RoleId = role!.Id,
                    Role = role,
                    EnterpriseId = enterprise!.EnterpriseId,
                    Enterprise = enterprise

                };
                var result = await _userRoleEnterpriseRepository.RegisterAsync(roleToUserObject);

                if (result is not null)

                {
                    _logger.LogInformation(1, $"User {user.Email} Added to the {roleName} role and {enterprise.CorporateReason}");
                    return StatusCode(StatusCodes.Status200OK,
                    new ResponseDTO { Status = "Success", Message = $"User {user.Email} to the {roleName} role and {enterprise.CorporateReason}" });
                }

                else
                {
                    _logger.LogInformation(1, $"Error: Unable to add user {user.Email} to the {roleName} role and {enterprise.CorporateReason}");
                    return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = $"Error: Unable to add user {user.Email} to the {roleName} role and {enterprise.CorporateReason}" });
                }
            }
            return BadRequest(new { error = "Unable to find user" });
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username!);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))

            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName!),
                        new Claim(ClaimTypes.Email, user.Email!),
                        new Claim("id", user.UserName!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateAccessToken(authClaims, _configuration);

                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                user.RefreshToken = refreshToken;


                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                //CRIA UM NOVO USUARIO CASO ELE NÃO EXISTA
                //============================================================================
                var userExists = await _userManager.FindByNameAsync(model.Username!);

                if (userExists != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new ResponseDTO { Status = "Error", Message = "User already exists!" });
                }

                ApplicationUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    Function = model.Function,
                    CompletedName = model.CompletedName,
                };
                var result = await _userManager.CreateAsync(user, model.Password!);
                var createdUser = await _userManager.FindByEmailAsync(user.Email!);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "User creation failed!" });
                }
                //============================================================================

                //CRIA UM NOVO TENANT
                //============================================================================
                var newTenant = new Tenant
                {
                    TenantName = model.EnterpriseName,
                    TenantCustomDomain = null,
                    TenantStatus = "Ativo",
                    Responsible = model.CompletedName,
                    Email = model.Email,
                    PhoneNumber = null,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };
                var tenantCreated = await _tenantRepository.RegisterAsync(newTenant);
                //============================================================================


                //CRIA UMA NOVA EMPRESA E ASSOCIA AO TENANT CRIADO ACIMA
                //============================================================================
                var newEnterprise = new Enterprise
                {
                    CorporateReason = model.EnterpriseName,
                    CNPJ = null,
                    Segment = null,
                    TenantId = tenantCreated.TenantId,
                    Tenant = tenantCreated
                };
                var enterpriseCreated = await _enterpriseRepository.RegisterAsync(newEnterprise);
                //============================================================================


                //CRIA O RELACIONAMENTO USUARIO E EMPRESA N:N
                //============================================================================
                var enterpriseUser = new Enterprise_User
                {
                    EnterpriseId = enterpriseCreated.EnterpriseId,
                    Enterprise = enterpriseCreated,
                    User = createdUser,
                };
                await _enterpriseUserRepository.RegisterAsync(enterpriseUser);
                //============================================================================

                //CRIA A ROLE ADMIN CASO ELA NÃO EXISTA
                //============================================================================
                var roleExist = await _roleManager.RoleExistsAsync("ADMIN");

                if (!roleExist)
                {
                    ApplicationRole role = new()
                    {
                        Name = "ADMIN",
                        NormalizedName = "ADMIN".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    };
                    await _roleManager.CreateAsync(role);
                }
                //============================================================================


                //ADICIONA A ROLE DO USUÁRIO NA CRIAÇÃO
                //============================================================================
                var recoverUser = await _userManager.FindByIdAsync(createdUser!.Id!);
                var recoverRole = await _roleManager.FindByNameAsync("ADMIN");
                var recoverEnterprise = await _enterpriseRepository.RecoverBy(p => p.EnterpriseId == enterpriseCreated.EnterpriseId);

                if (recoverUser != null)
                {
                    //var result = await _userManager.AddToRoleAsync(user, roleName);
                    var roleToUserObject = new ApplicationUserRoleEnterprise
                    {
                        UserId = recoverUser!.Id,
                        User = recoverUser,
                        RoleId = recoverRole!.Id,
                        Role = recoverRole,
                        EnterpriseId = recoverEnterprise!.EnterpriseId,
                        Enterprise = recoverEnterprise

                    };
                    await _userRoleEnterpriseRepository.RegisterAsync(roleToUserObject);

                    //=========================================================================

                }
                return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("refresh-token")]
        [Authorize(Policy = "ADMIN")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken ?? throw new ArgumentException(nameof(tokenModel));

            string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentException(nameof(tokenModel));

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken!, _configuration);

            if (principal == null)
            {
                return BadRequest("Invalid access token/refresh token");
            }
            string username = principal.Identity!.Name!;

            var user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token/refresh token");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Route("revoke/{username}")]
        [Authorize(Policy = "ADMIN")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpPost]
        [Route("RevokeRoleToUser")]
        [Authorize(Policy = "ADMIN")]
        public async Task<IActionResult> RevokeRole(string name, string role)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound($"User with nome '{name}' not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role);
            if (result.Succeeded)
            {
                return Ok($"Role '{role}' has been revoked from user '{user.UserName}'.");
            }
            else
            {
                return BadRequest("Failed to revoke role.");
            }
        }


        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return BadRequest("Invalid email.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, model.Token!);
            if (result.Succeeded)
            {
                return Ok("Email confirmed successfully.");
            }

            return BadRequest("Email confirmation failed.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return BadRequest("Invalid email.");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token!, model.Password!);
            if (result.Succeeded)
            {
                return Ok("Password has been reset.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUsers()
        {
            var users = await _userApplicationRepository.ToListAsync();

            if (users is null) { return NotFound(); }

            var listUsersDTO = new List<GetUsersDTO>();

            foreach (var user in users)
            {
                listUsersDTO.Add(new GetUsersDTO
                {
                    UserId = user.Id,
                    NameCompleted = user.CompletedName,
                    Function = user.Function,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                });

            }

            return Ok(listUsersDTO);
        }
        [HttpGet]
        [Route("getUserPerId")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetUserPerId(string UserId)
        {
            var user = await _userApplicationRepository.RecoverBy(u => u.Id == UserId);

            if (user is null)
            {
                return NotFound();
            }

            var userDTO = new GetUsersDTO
            {
                UserId = user.Id,
                NameCompleted = user.CompletedName,
                Function = user.Function,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return Ok(userDTO);
        }
        [HttpGet]
        [Route("getRolePerUserAndPerEnterprise")]
        public async Task<ActionResult<IEnumerable<GETRolePerUserPerEntepriseDTO>>> GetRolePerUserAndPerEnterprise(string UserId, Guid EnterpriseId)
        {

            var list =  _userRoleEnterpriseRepository.SearchForAsync(p => p.UserId == UserId).Where(p => p.EnterpriseId == EnterpriseId);

            if (list.IsNullOrEmpty())
            {
                return NotFound("User Not existe at the enterprise!");
            }
            var roleDTO = new List<GETRolePerUserPerEntepriseDTO>();

            foreach (var item in list)
            {
                roleDTO.Add(new GETRolePerUserPerEntepriseDTO
                {
                    Role_User_Enterprise = list!.Select(p => p.Role!.Name).ToList()!

                });
            }
            return Ok(roleDTO);
        }
    }
}



