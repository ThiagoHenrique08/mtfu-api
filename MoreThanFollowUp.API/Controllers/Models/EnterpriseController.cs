using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Application.DTO.Enterprise;
using MoreThanFollowUp.Application.DTO.Tenant;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;
using MoreThanFollowUp.Infrastructure.Repository.Models;
using System.Linq.Expressions;

namespace MoreThanFollowUp.API.Controllers.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEnterprise_UserRepository _enterpriseUserRepository;
        private readonly IApplicationUserRoleEnterpriseRepository _userRoleEnterpriseRepository;
        public EnterpriseController(IEnterpriseRepository enterpriseRepository, ITenantRepository tenantRepository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IEnterprise_UserRepository enterpriseUserRepository, IApplicationUserRoleEnterpriseRepository userRoleEnterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
            _tenantRepository = tenantRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _enterpriseUserRepository = enterpriseUserRepository;
            _userRoleEnterpriseRepository = userRoleEnterpriseRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GETEnterpriseDTO>> PostEnterprise(POSTEntepriseDTO enterpriseDTO, string UserId)
        {
            try
            {
                var tenant = await _tenantRepository.RecoverBy(p=>p.TenantId == enterpriseDTO.TenantId);
                if (tenant is null) { return NotFound("Tenant is not exist"); }
                if (enterpriseDTO is null) { return NotFound(); }

                var newEnterprise = new Enterprise
                {
                    CorporateReason = enterpriseDTO.CorporateReason,
                    CNPJ = enterpriseDTO.CNPJ,
                    Segment = enterpriseDTO.Segment,
                    TenantId = tenant.TenantId,
                    Tenant = tenant,
                };
               var EnterpriseCreated =  await _enterpriseRepository.RegisterAsync(newEnterprise);



                //CRIA O RELACIONAMENTO USUARIO E EMPRESA N:N
                //============================================================================

                var User = await _userManager.FindByIdAsync(UserId);
                var enterpriseUser = new Enterprise_User
                {
                    EnterpriseId = EnterpriseCreated.EnterpriseId,
                    Enterprise = EnterpriseCreated,
                    User = User,
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

                    var recoverUser = await _userManager.FindByIdAsync(UserId);
                    var recoverRole = await _roleManager.FindByNameAsync("ADMIN");
                    var recoverEnterprise = await _enterpriseRepository.RecoverBy(p => p.EnterpriseId == EnterpriseCreated.EnterpriseId);

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



                var getEnterpriseDTO = new GETEnterpriseDTO
                {
                    EnterpriseId = newEnterprise.EnterpriseId,
                    CNPJ = newEnterprise.CNPJ,
                    Segment = newEnterprise.Segment,
                    TenantId = newEnterprise.TenantId,
                };


                return Ok(getEnterpriseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getEnterprise")]
        public async Task<ActionResult<IEnumerable<GETEnterpriseDTO>>> GetEnterprise(Guid TenantId)
        {
            try
            {
                var enteprises = _enterpriseRepository.SearchForAsync(p => p.TenantId == TenantId);

                if (enteprises.IsNullOrEmpty()) { return NotFound(); }
                var enteprisesDTO = new List<GETEnterpriseDTO>();

                foreach (var enterprise in enteprises)
                {
                    enteprisesDTO.Add(new GETEnterpriseDTO
                    {
                        EnterpriseId = enterprise.EnterpriseId,
                        CorporateReason = enterprise.CorporateReason,
                        CNPJ = enterprise.CNPJ,
                        Segment = enterprise.Segment,
                        TenantId = enterprise.TenantId,
                    });
                }
                return Ok(enteprisesDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPatch]
        [Route("update")]
        public async Task<ActionResult> UpdateEnterprise(PATCHEnterpriseDTO enterpriseDTO)
        {

            try
            {
                var enterpriseList = await _enterpriseRepository.RecoverBy(t => t.EnterpriseId == enterpriseDTO.EnterpriseId);

                if (enterpriseList is not null)
                {
                    enterpriseList.CorporateReason = enterpriseDTO.CorporateReason ?? enterpriseList.CorporateReason;
                    enterpriseList.CNPJ = enterpriseDTO.CNPJ ?? enterpriseList.CNPJ;
                    enterpriseList.CNPJ = enterpriseDTO.CNPJ ?? enterpriseList.CNPJ;
                }
                else
                {
                    NotFound();
                }

                await _enterpriseRepository.UpdateAsync(enterpriseList!);

                return Ok(enterpriseList
                       );

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete(Guid idEnterprise)
        {
            try
            {

                // Deleta o Projeto de Fato
                var enterprise = await _enterpriseRepository.RecoverBy(p => p.EnterpriseId == idEnterprise);

                if (enterprise is null) { return NotFound(); }

                await _enterpriseRepository.DeleteAsync(enterprise);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
