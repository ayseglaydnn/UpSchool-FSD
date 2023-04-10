using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Queries.GetById
{
    public class AddressGetByIdQuery:IRequest<List<AddressGetByIdDto>>
    {
        public Guid Id { get; set; }
        public bool? IsDeleted { get; set; }

        public AddressGetByIdQuery(Guid id, bool? isDeleted)
        {
            Id = id;

            IsDeleted = isDeleted;
        }
    }
}
