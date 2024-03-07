using Framework.Core.Enums;
using Framework.Core.Event;
using Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Projection
{
    public record UserCreated(
        Guid Id,
        string FirstName,
        string LastName,
        string Email
        );
    public class UserProjection
    {
        public static bool Handle(EventEnvelope<UserCreated> eventEnvelope)
        {
            var (id, firstName, lastName, email) = eventEnvelope.Data;
            using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
            {
                UserEntity entity = new UserEntity()
                {
                    Id = (Guid)id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
    }
}
