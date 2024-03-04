using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.EventEnvelopes.User
{
    public record UserCreated(
        Guid Id,
        string Username,
        string FirstName,
        string LastName,
        string Email,
        RecordStatusEnum Status
        )
    {
        public static UserCreated Created(Guid id,
        string username,
        string firstName,
        string lastName,
        string email,
        RecordStatusEnum status) => new(id, username, firstName, lastName,email, status);
    }
}
