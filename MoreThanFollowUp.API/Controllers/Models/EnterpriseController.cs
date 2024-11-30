using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Application.DTO.Enterprise;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models.Users;

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
        private readonly IApplicationUserRoleEnterpriseTenantRepository _enterpriseUserRepository;
        private readonly IApplicationUserRoleEnterpriseTenantRepository _userRoleEnterpriseTenantRepository;
        public EnterpriseController(IEnterpriseRepository enterpriseRepository, ITenantRepository tenantRepository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IApplicationUserRoleEnterpriseTenantRepository enterpriseUserRepository, IApplicationUserRoleEnterpriseTenantRepository userRoleEnterpriseTenantRepository)
        {
            _enterpriseRepository = enterpriseRepository;
            _tenantRepository = tenantRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _enterpriseUserRepository = enterpriseUserRepository;
            _userRoleEnterpriseTenantRepository = userRoleEnterpriseTenantRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<GETEnterpriseDTO>> PostEnterprise(POSTEntepriseDTO enterpriseDTO, string UserId)
        {
            try
            {
               
                if (enterpriseDTO is null) { return NotFound(); }

                var newEnterprise = new Enterprise
                {
                    CorporateReason = enterpriseDTO.CorporateReason,
                    CNPJ = enterpriseDTO.CNPJ,
                    Segment = enterpriseDTO.Segment,
                };
               var EnterpriseCreated =  await _enterpriseRepository.RegisterAsync(newEnterprise);

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
                    var relationshipObject = await _userRoleEnterpriseTenantRepository.RecoverBy(u => u.UserId == recoverUser!.Id);
                    var result = new ApplicationUserRoleEnterpriseTenant();
                if (recoverUser != null)
                    {
                        //var result = await _userManager.AddToRoleAsync(user, roleName);
                        var roleToUserObject = new ApplicationUserRoleEnterpriseTenant
                        {
                            UserId = recoverUser!.Id,
                            User = recoverUser,
                            RoleId = recoverRole!.Id,
                            Role = recoverRole,
                            EnterpriseId = recoverEnterprise!.EnterpriseId,
                            Enterprise = recoverEnterprise,
                            TenantId = relationshipObject!.TenantId,
                            Tenant = relationshipObject.Tenant,

                        };
                         result = await _userRoleEnterpriseTenantRepository.RegisterAsync(roleToUserObject);

                        //=========================================================================

                    }


                var getEnterpriseDTO = new GETEnterpriseDTO
                {
                    EnterpriseId = newEnterprise.EnterpriseId,
                    CNPJ = newEnterprise.CNPJ,
                    Segment = newEnterprise.Segment,
                    TenantId = result.Tenant!.TenantId,
                };


                return Ok(getEnterpriseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getEnterprisePerUser")]
        [OutputCache(Duration = 300)]
        public async Task<ActionResult<IEnumerable<GETEnterpriseDTO>>> GetEnterprisePerUser(string UserId)
        {
            try
            {
           
                var relationshipObjectList = _userRoleEnterpriseTenantRepository.SearchForAsync(p => p.User!.Id == UserId);

                if (relationshipObjectList.IsNullOrEmpty()) { return NotFound(); }
                var enteprisesDTO = new List<GETEnterpriseDTO>();

                foreach (var enterprise in relationshipObjectList)
                {
                    enteprisesDTO.Add(new GETEnterpriseDTO
                    {
                        EnterpriseId = enterprise.Enterprise!.EnterpriseId,
                        CorporateReason = enterprise.Enterprise.CorporateReason,
                        CNPJ = enterprise.Enterprise.CNPJ,
                        Segment = enterprise.Enterprise.Segment,
                        TenantId = enterprise.TenantId
                    });
                }
                return Ok(enteprisesDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getEnterprisePerId")]
        [OutputCache(Duration = 400)]
        public async Task<ActionResult<GETEnterpriseDTO>> GetEnterprise(Guid Enterpriseid)
        {
            try
            {

                var enterprise = await _enterpriseRepository.RecoverBy(p => p.EnterpriseId == Enterpriseid);
                var tenant = await _userRoleEnterpriseTenantRepository.RecoverBy(p => p.EnterpriseId == enterprise!.EnterpriseId);
                if (enterprise is null) { return NotFound(); }
                

                var enterpriseDTO = new GETEnterpriseDTO
                {
                    EnterpriseId = enterprise.EnterpriseId,
                    CorporateReason = enterprise.CorporateReason,
                    CNPJ = enterprise.CNPJ,
                    Segment = enterprise.Segment,
                    TenantId = tenant!.TenantId
                };

                return Ok(enterpriseDTO);
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
                var enterprise = await _enterpriseRepository.RecoverBy(t => t.EnterpriseId == enterpriseDTO.EnterpriseId);

                if (enterprise is not null)
                {
                    enterprise.CorporateReason = enterpriseDTO.CorporateReason ?? enterprise.CorporateReason;
                    enterprise.CNPJ = enterpriseDTO.CNPJ ?? enterprise.CNPJ;
                    enterprise.Segment = enterpriseDTO.Segment ?? enterprise.Segment;
                }
                else
                {
                    NotFound();
                }

                await _enterpriseRepository.UpdateAsync(enterprise!);

                return Ok();

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
