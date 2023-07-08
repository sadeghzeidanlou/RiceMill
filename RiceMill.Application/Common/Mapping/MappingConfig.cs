﻿using Mapster;
using RiceMill.Application.UseCases.ConcernServices.Dto;
using RiceMill.Application.UseCases.RiceMillServices.Dto;
using RiceMill.Domain.Models;

namespace RiceMill.Application.Common.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Models.RiceMill, DtoRiceMill>()
                .MaxDepth(2)
                .TwoWays();

            config.NewConfig<Concern, DtoConcern>()
                .MaxDepth(2)
                .TwoWays();
        }
    }
}