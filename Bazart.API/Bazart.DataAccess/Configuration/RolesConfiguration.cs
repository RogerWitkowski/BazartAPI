using Bazart.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bazart.DataAccess.Configuration
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Roles.Admin,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    NormalizedName = Roles.Admin.ToUpper()
                },
                 new IdentityRole
                 {
                     Id = Guid.NewGuid().ToString(),
                     Name = Roles.Customer,
                     ConcurrencyStamp = Guid.NewGuid().ToString(),
                     NormalizedName = Roles.Customer.ToUpper()
                 },
                  new IdentityRole
                  {
                      Id = Guid.NewGuid().ToString(),
                      Name = Roles.Artist,
                      ConcurrencyStamp = Guid.NewGuid().ToString(),
                      NormalizedName = Roles.Artist.ToUpper()
                  }
                );
        }
    }
}