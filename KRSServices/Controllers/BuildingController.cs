using System.Text.Json.Serialization;
using AutoMapper;
using BL.DTOs.BuildingDTO;
using BL.Helpers;
using BL.Shared;
using DL.Entites;
using KRSServices.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KRSServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : BaseApiController
    {
        private readonly IUniteOfWork _UOW;
        private readonly IMapper _mapper;


        public BuildingController(IUniteOfWork uOW, IMapper mapper)
        {
            _UOW = uOW;
            _mapper = mapper;

        }


        [HttpGet]
        public IActionResult GetBuildings([FromQuery] BuildingPrameters param)
        {

            var Building = _UOW.buildingRepository.GetAllBuildings(param);

            //var metaData = new
            //{
            //    Building.TotalCount,
            //    Building.PageSize,
            //    Building.CurrentPage,
            //    Building.TotalPages,
            //    Building.HasNext,
            //    Building.HasPrevious,
            //};
            //Response.Headers.Add("Pagination", JsonConvert.SerializeObject(metaData));
            return Ok(Building);
        }


        [HttpGet("{id}")]
        public IActionResult GetBuilding(int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse(400));

            var building = _UOW.buildingRepository.GetById(id);
            if (building is null) return NotFound(new ApiResponse(404));
            var mapBuilding = _mapper.Map<building, CreatBuildingDto>(building);
            return Ok(mapBuilding);

        }

        [HttpPost]
        public IActionResult AddBuilding(CreatBuildingDto build)
        {
            if (build == null) return BadRequest(new { Message = "Object is null" });
            if (!ModelState.IsValid) return BadRequest(new { Message = "Invalid Model Object" });
            var mapbuild = _mapper.Map<CreatBuildingDto, building>(build);
            mapbuild.CreatedOn = DateTime.UtcNow;
            _UOW.buildingRepository.Add(mapbuild);
            _UOW.buildingRepository.savechanges();
            return Ok(build);
        }

        [HttpPut("{id}")]
        public IActionResult Edite(int id , [FromBody]CreatBuildingDto build)
        {
            if (build == null) return BadRequest(new { Message = "Object is null" });
            if (!ModelState.IsValid) return BadRequest(new { Message = "Invalid Model Object" });
            var entityBuilding = _UOW.buildingRepository.GetById(id);
            if (entityBuilding == null) return NotFound(new ApiResponse(404));
            build.CreatedOn = entityBuilding.CreatedOn;
            build.UpdatedOn= DateTime.UtcNow;
            _mapper.Map( build , entityBuilding);
            _UOW.buildingRepository.Update(entityBuilding);
            _UOW.buildingRepository.savechanges();
            return Ok(build);

        }
        [HttpDelete("{id}")]
        public  IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse(400));
            var result = _UOW.buildingRepository.GetById(id);
            if (result == null) return NotFound(new ApiResponse(404));
            result.IsDeleted = true;
            result.UpdatedOn = DateTime.UtcNow;
            _UOW.buildingRepository.Update(result);
            _UOW.buildingRepository.savechanges();
            return Ok();

        }
    }
}
