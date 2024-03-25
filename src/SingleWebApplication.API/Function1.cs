using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SingleWebApplication.API
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function(nameof(Function1))]
        public void Run()
        {
            _logger.LogInformation(nameof(Function1));
        }
    }
}
