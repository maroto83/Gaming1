namespace Gaming1.Application.Service.Resolvers
{
    public class WinnerResolver
        : GameResolver
    {
        public override bool CanHandle(int secretNumber, int suggestedNumber)
        {
            return secretNumber == suggestedNumber;
        }

        public override string Handle(int secretNumber, int suggestedNumber)
        {
            return $"Yes! The secret number is {suggestedNumber}. You are the winner!";
        }
    }
}