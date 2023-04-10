using Application.Common.Interfaces;
using Application.Features.Addresses.Commands.Delete;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Commands.HardDelete
{
    public class AddressHardDeleteCommandHandler : IRequestHandler<AddressHardDeleteCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AddressHardDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<Response<Guid>> Handle(AddressHardDeleteCommand request, CancellationToken cancellationToken)
        {
            var address = await _applicationDbContext.Addresses
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (address == null)
            {
                throw new ArgumentException(nameof(request.Id));
            }

            if(address.IsDeleted == false)
            {
                return new Response<Guid>($"Mark the address named \"{address.Name}\" as deleted before deleting permanenrtly.", address.Id);
            }

            _applicationDbContext.Addresses.Remove(address);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The address named \"{address.Name}\" was successfully deleted.", address.Id);
        }
    }
}
