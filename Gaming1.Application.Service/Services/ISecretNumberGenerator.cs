namespace Gaming1.Application.Service.Services
{
    public interface ISecretNumberGenerator
    {
        int Create(int minimum = 1, int maximum = 100);
    }
}