﻿using BusinessLogic.Entities;
using Crosscutting.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Business.Context;
using Dtos.Features.InWhitelist;
using MediatR;

namespace BusinessLogic.Features.InWhitelist
{
    public class BsnInWhitelistQueryHandler : IRequestHandler<BsnInWhitelistQuery, ZoekResult>
    {
        readonly WhitelistContext _context;

        public BsnInWhitelistQueryHandler(WhitelistContext context)
        {
            Guard.IsNotNull(context, nameof(context));

            _context  = context;
        }

        public async Task<ZoekResult> Handle(BsnInWhitelistQuery request, CancellationToken cancellationToken)
        {
            Guard.IsNotNull(request, nameof(request));

            return new ZoekResult(
                await _context.FindAsync<Bsn>(
                    request.Bsnnummer) != null);
        }
    }
}
