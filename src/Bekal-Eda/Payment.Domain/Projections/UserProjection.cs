using Framework.Core.Enums;
using Framework.Core.Event;
using Payment.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Projections
{
    public record UserCreated(
        Guid Id,
        string UserName,
        string FirstName,
        string LastName,
        string Email,
        RecordStatusEnum Status
    );

    public class UserProjection
    {
        public static bool Handle(EventEnvelope<UserCreated> eventEnvelope)
        {
            var (id, userName, firstName, lastName, email, status) = eventEnvelope.Data;
            using (var context = new PaymentDbContext(PaymentDbContext.OnConfigure()))
            {
                UserEntity entity = new UserEntity()
                {
                    Id = (Guid)id,
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };

                context.Users.Add(entity);
                context.SaveChanges();
            }

            return true; //new AttributeCreated(id, type, unit, status, modified);
        }
    }
}
