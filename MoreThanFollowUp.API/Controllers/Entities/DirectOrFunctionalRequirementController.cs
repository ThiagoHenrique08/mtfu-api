using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.IdentityModel.Tokens;
using MoreThanFollowUp.Application.DTO.Project.DirectOrFunctionalRequirement;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Infrastructure.Interfaces.Entities.Projects;

namespace MoreThanFollowUp.API.Controllers.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectOrFunctionalRequirementController : ControllerBase
    {

        private readonly IDirectOrFunctionalRequirementRepository _directOrFunctionalRequirementRepository;
        private readonly IRequirementAnalysisRepository _requirementAnalysisRepository;

        public DirectOrFunctionalRequirementController(IDirectOrFunctionalRequirementRepository directOrFunctionalRequirementRepository, IRequirementAnalysisRepository requirementAnalysisRepository)
        {
            _directOrFunctionalRequirementRepository = directOrFunctionalRequirementRepository;
            _requirementAnalysisRepository = requirementAnalysisRepository;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<IEnumerable<GETDirectOrFunctionalDTO>>> CreateDirectOrFunctionalRequirement(POSTDirectOrFunctionlDTO requirement)
        {
            try
            {
                var requirementAnalysisExist = await _requirementAnalysisRepository.RecoverBy(p => p.RequirementAnalysisId == requirement.RequirementAnalysisId);

                if (requirementAnalysisExist is not null)
                {
                    var newDirectOrFunctional = new DirectOrFunctionalRequirement
                    {
                        FunctionOrAction = requirement.FunctionOrAction,
                        SystemBehavior = requirement.SystemBehavior,
                        RequirementAnalysisId = requirementAnalysisExist.RequirementAnalysisId,
                        RequirementAnalysis = requirementAnalysisExist,
                    };
                  await  _directOrFunctionalRequirementRepository.RegisterAsync(newDirectOrFunctional);

                    var requirementCreated = await _directOrFunctionalRequirementRepository.RecoverBy(p => p.DirectOrFunctionalRequirementId == newDirectOrFunctional.DirectOrFunctionalRequirementId);

                    if(requirementCreated is null)
                    {
                        return NotFound();
                    }

                    var newDirectOrFunctionalRequirementDTO = new GETDirectOrFunctionalDTO
                    {
                        DirectOrFunctionalRequirementId = requirementCreated.DirectOrFunctionalRequirementId,
                        FunctionOrAction = requirementCreated.FunctionOrAction,
                        SystemBehavior = requirementCreated.SystemBehavior,

                    };
                    return Ok(newDirectOrFunctionalRequirementDTO);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("list")]
        [OutputCache(Duration = 400)]
        public async Task<ActionResult<IEnumerable<GETDirectOrFunctionalDTO>>> GetDirectOrFunctionalRequirement(Guid RequirementAnalysisID)
        {
            try
            {
                var directOrFunctionalList =  _directOrFunctionalRequirementRepository.SearchForAsync(p=>p.RequirementAnalysisId == RequirementAnalysisID);
                var newListDirectOrFunctionalListDTO = new List<GETDirectOrFunctionalDTO>();

                if (!directOrFunctionalList.IsNullOrEmpty())
                {
                    foreach (var requirement in directOrFunctionalList)
                    {

                        newListDirectOrFunctionalListDTO.Add(new GETDirectOrFunctionalDTO
                        {
                            DirectOrFunctionalRequirementId = requirement.DirectOrFunctionalRequirementId,
                            FunctionOrAction = requirement.FunctionOrAction,
                            SystemBehavior = requirement.SystemBehavior,

                        });
                    }
                    return Ok(newListDirectOrFunctionalListDTO);
                }
                else
                {
                    return NotFound();
                }


            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);  
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteDirectOrFunctionalRequirement(Guid id)
        {
            try
            {
                var requiremnt = await _directOrFunctionalRequirementRepository.RecoverBy(p => p.DirectOrFunctionalRequirementId == id);

                if(requiremnt is not null)
                {
                    await _directOrFunctionalRequirementRepository.DeleteAsync(requiremnt);
                    return Ok(requiremnt);
                }
                else
                {
                    return NotFound("Id not exist");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
    }
}

