using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Commands.Add
{
    public class AddressAddCommandHandler : IRequestHandler<AddressAddCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(AddressAddCommand request, CancellationToken cancellationToken)
        {
            if (!await _applicationDbContext.Countries.AnyAsync(x => x.Id == request.CountryId, cancellationToken))
            {
                throw new ArgumentException(nameof(request.CountryId));
            }

            if (!await _applicationDbContext.Cities.AnyAsync(x => x.Id == request.CityId, cancellationToken))
            {
                throw new ArgumentException(nameof(request.CityId));
            }

            if (await _applicationDbContext.Addresses.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
            {
                throw new ArgumentException(nameof(request.Name));
            }

            var address = new Address()
            {
                Name = request.Name,
                UserId = request.UserId,
                CountryId = request.CountryId,
                CityId = request.CityId,
                District = request.District,
                PostCode = request.PostCode,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                CreatedOn = DateTimeOffset.Now,
                CreatedByUserId = null,
                IsDeleted = false,
            };

            await _applicationDbContext.Addresses.AddAsync(address, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The new address named \"{address.Name}\" was successfully added.", address.Id);
        }
    }
}
