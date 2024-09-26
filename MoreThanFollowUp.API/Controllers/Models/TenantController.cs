using Microsoft.AspNetCore.Mvc;
using MoreThanFollowUp.Application.DTO.Tenant;
using MoreThanFollowUp.Domain.Models;
using MoreThanFollowUp.Infrastructure.Interfaces.Models;

namespace MoreThanFollowUp.API.Controllers.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        public TenantController(ITenantRepository tenantRepository, ISubscriptionRepository subscriptionRepository, IInvoiceRepository invoiceRepository)
        {
            _tenantRepository = tenantRepository;
            _subscriptionRepository = subscriptionRepository;
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<IEnumerable<GETTenantDTO>>> getTenant()
        {
            try
            {
                var tenantList = await _tenantRepository.ToListAsync();

                if (tenantList is null) { return NotFound(); }

                var tenantDTOList = new List<GETTenantDTO>();

                foreach (var tenant in tenantList)
                {
                    tenantDTOList.Add(new GETTenantDTO
                    {
                        TenantId = tenant.TenantId,
                        TenantName = tenant.TenantName,
                        TenantCustomDomain = tenant.TenantCustomDomain,
                        TenantStatus = tenant.TenantStatus,
                        Responsible = tenant.Responsible,
                        Email = tenant.Email,
                        PhoneNumber = tenant.PhoneNumber,
                    });
                }

                return (tenantDTOList);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<POSTTenantDTO>> PostTenant(POSTTenantDTO tenantDTO)
        {
            try
            {
                if (tenantDTO is null) { return NotFound(); }

                var newTenant = new Tenant
                {
                    TenantName = tenantDTO.TenantName,
                    TenantCustomDomain = tenantDTO.TenantCustomDomain,
                    TenantStatus = tenantDTO.TenantStatus,
                    Responsible = tenantDTO.Responsible,
                    Email = tenantDTO.Email,
                    PhoneNumber = tenantDTO.PhoneNumber,
                    CreatedAt = DateTime.Now,

                };
                await _tenantRepository.RegisterAsync(newTenant);
                return Ok(newTenant);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPatch]
        [Route("update")]
        public async Task<ActionResult> changeTenant(PATCHTenantDTO patchTenantDTO)
        {

            try
            {
                var tenantList = await _tenantRepository.RecoverBy(t => t.TenantId == patchTenantDTO.TenantId);

                if (tenantList is not null)
                {
                    tenantList.TenantName = patchTenantDTO.TenantName ?? tenantList.TenantName;
                    tenantList.TenantCustomDomain = patchTenantDTO.TenantCustomDomain ?? tenantList.TenantCustomDomain;
                    tenantList.TenantStatus = patchTenantDTO.TenantStatus ?? tenantList.TenantStatus;
                    tenantList.Responsible = patchTenantDTO.Responsible ?? tenantList.Responsible;
                    tenantList.Email = patchTenantDTO.Email ?? tenantList.Email;
                    tenantList.PhoneNumber = patchTenantDTO.PhoneNumber ?? tenantList.PhoneNumber;
                }
                else
                {
                    NotFound();
                }

                await _tenantRepository.UpdateAsync(tenantList!);

                return Ok(tenantList);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> Delete(int idTenant)
        {
            try
            {

                // Deleta o Projeto de Fato
                var tenant = await _tenantRepository.RecoverBy(p => p.TenantId == idTenant);

                if (tenant is null) { return NotFound(); }

                await _tenantRepository.DeleteAsync(tenant);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
