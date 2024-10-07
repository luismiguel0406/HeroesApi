using HeroesApi.Interfaces;

namespace HeroesApi.Services
{
    public class ElementTeller : IElement
    {
        private readonly ILogger<ElementTeller> _logger;


        public ElementTeller(ILogger<ElementTeller> logger)
        {
            _logger = logger;
        }
        public void SayElement(string element)
        {
            _logger.LogInformation($"Your element is: {element}");
        }

        public string WriteElement(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return $"This is a message: {message}";
            }
            return string.Empty;
        }
    }
}
