﻿using Microsoft.AspNetCore.Mvc;
using RiceMill.Application.Common.Models.ResultObject;
using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using System.ComponentModel.DataAnnotations;

namespace RiceMill.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ConcernController : BaseController
    {
        private readonly IConcernCommands _concernCommands;

        private readonly IConcernQueries _concernQueries;

        public ConcernController(IConcernCommands concernCommands, IConcernQueries concernQueries)
        {
            _concernCommands = concernCommands;
            _concernQueries = concernQueries;
        }

        [HttpGet]
        public Result<PaginatedList<DtoConcern>> Get([FromQuery] DtoConcernFilter filter) => _concernQueries.GetAll(filter);

        [HttpPost]
        public Result<DtoConcern> Post([FromBody] DtoCreateConcern dtoCreateConcern) => _concernCommands.Create(dtoCreateConcern);

        [HttpPut]
        public Result<DtoConcern> Put([FromBody] DtoUpdateConcern dtoUpdateConcern) => _concernCommands.Update(dtoUpdateConcern);

        [HttpDelete("{id}")]
        public Result<bool> Delete(Guid id) => _concernCommands.Delete(id);
    }
}