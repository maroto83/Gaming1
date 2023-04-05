using System;

namespace Gaming1.Application.Service.Services
{
    public class SecretNumberGenerator
        : ISecretNumberGenerator
    {
        public int Create(int minimum = 1, int maximum = 100)
        {
            return new Random().Next(minimum, maximum);
        }
    }
}
