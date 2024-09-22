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

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<POSTTenantDTO>> postTenant(POSTTenantDTO tenantDTO)
        {
            //if (tenantDTO is null) { return NotFound(); }

            //var tenantExist = _tenantRepository.RecuperarPorAsync(p => p.TenantName!.ToUpper().Equals(tenantDTO.TenantName!.ToUpper()));
            //int totalLicense = 50;
            //int totalUsed = 10;

            //if (!(tenantExist is null))
            //{
            //    var newTenant = new Tenant
            //    {
            //        TenantName = tenantDTO.TenantName,
            //        TenantCustomDomain = tenantDTO.TenantCustomDomain,
            //        TenantStatus = tenantDTO.TenantStatus,
            //        Responsible = tenantDTO.Responsible,
            //        Email = tenantDTO.Email,
            //        PhoneNumber = tenantDTO.PhoneNumber,
            //        CreatedAt = DateTime.Now,

            //    };
            //    await _tenantRepository.AdicionarAsync(newTenant);
            //    var tenant = await _tenantRepository.RecuperarPorAsync(p => p.TenantId == newTenant.TenantId);

            //    var subscription = new Subscription
            //    {

            //        TenantId = tenant!.TenantId,
            //        Tenant = tenant,
            //        Plan = "Enterprise",
            //        Status = "Ativo",
            //        TotalLicense = totalLicense,
            //        TotalUsed = totalUsed,
            //        TotalAvailable = totalLicense - totalUsed,
            //        StartDate = DateTime.Now,
            //        EndDate = DateTime.Now.AddDays(365)


            //    };

            //    await _subscriptionRepository.AdicionarAsync(subscription);
            //    var subscriptionExist = await _subscriptionRepository.RecuperarPorAsync(s => s.SubscriptionId == subscription.SubscriptionId);

            //    if (!(subscriptionExist is null))
            //    {
            //        if (subscription.Status! == "Ativo")
            //        {
            //            var invoice = new Invoice
            //            {
            //                TenantId = tenant.TenantId,
            //                Tenant = tenant,
            //                Amount = totalLicense * 500.50,
            //                Status = "Ativo",
            //                CreateAt = DateTime.Now,
            //                DueDate = DateTime.Now.AddDays(365)

            //            };

            //            await _invoiceRepository.AdicionarAsync(invoice);

            //        }
            //    }
            //    else
            //    {
            //        return NotFound("Subscription not exist!");
            //    }
            //}
            //else
            //{
            //    return BadRequest("Tenant exist!");
            //}



            return Ok(tenantDTO);
        }
    }
}
