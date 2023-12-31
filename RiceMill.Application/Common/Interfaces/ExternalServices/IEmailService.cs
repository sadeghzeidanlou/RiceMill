﻿using RiceMill.Application.Common.Models.ExternalDto;

namespace RiceMill.Application.Common.Interfaces.ExternalServices
{
    public interface IEmailService
    {
        Task Send(DtoEmailDetail emailDetail);
    }
}