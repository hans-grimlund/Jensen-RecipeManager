using RecipeManager.Interfaces;

namespace RecipeManager.Services
{
    public class Errorhandler : IErrorhandler
    {
        private readonly ILogger<Errorhandler> _logger;

        public Errorhandler(ILogger<Errorhandler> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("Something went wrong...");
            Console.WriteLine();
            _logger.LogError(ex.Message);    
            _logger.LogError(ex.StackTrace);    
        }
    }
}