using Saas.Application.Interfaces;

namespace Saas.Infrastructure.Data;

public class Test
{
    public Test()
    {
        IUserRepository userRepository = new UserRepository();
    }
}