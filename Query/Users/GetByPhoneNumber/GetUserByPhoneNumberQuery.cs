using Query.Common.Domain;
using Query.Users.DTOs;

namespace Query.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IBaseQuery<UserDto>;
