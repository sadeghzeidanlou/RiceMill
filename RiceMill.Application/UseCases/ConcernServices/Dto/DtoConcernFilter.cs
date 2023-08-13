﻿using RiceMill.Application.Common.Models.ResultObject;

namespace RiceMill.Application.UseCases.ConcernServices.Dto
{
    public class DtoConcernFilter : PagingInfo
    {
        public Guid? Id { get; set; }

        public List<Guid> Ids { get; set; }

        public Guid? RiceMillId { get; set; }

        public string Title { get; set; }
    }
}