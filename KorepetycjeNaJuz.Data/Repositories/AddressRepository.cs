using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(KorepetycjeContext context)
            : base(context)
        {

        }
    }
}
