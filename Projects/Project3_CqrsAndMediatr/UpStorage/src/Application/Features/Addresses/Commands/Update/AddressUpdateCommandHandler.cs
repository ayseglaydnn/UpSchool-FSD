using Application.Common.Interfaces;
using Application.Features.Addresses.Commands.Add;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Commands.Update
{
    public class AddressUpdateCommandHandler : IRequestHandler<AddressUpdateCommand,Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            var address = await _applicationDbContext.Addresses
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (address == null)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            address.Name = request.Name;
            address.UserId = request.UserId;
            address.CountryId = request.CountryId;
            address.CityId = request.CityId;
            address.District = request.District;
            address.PostCode = request.PostCode;
            address.AddressLine1 = request.AddressLine1;
            address.AddressLine2 = request.AddressLine2;
            address.ModifiedOn = DateTimeOffset.Now;
            address.ModifiedByUserId = request.UserId;
            address.IsDeleted = false;
            
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The address with \"{address.Id}\" id was successfully updated.", address.Id);
        }

        
    }
}
