namespace Gaming1.Application.Service.Resolvers
{
    public class HigherResolver
        : GameResolver
    {
        public override bool CanHandle(int secretNumber, int suggestedNumber)
        {
            return secretNumber > suggestedNumber;
        }

        public override string Handle(int secretNumber, int suggestedNumber)
        {
            return $"The secret number is higher than {suggestedNumber}.";
        }
    }
}