using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.BackgroundServices
{
    public class KafkaService : BackgroundService
    {
        private readonly ILogger<KafkaService> _logger;
        private readonly Func<CancellationToken, Task> _perform;
        public KafkaService(ILogger<KafkaService> logger, Func<CancellationToken, Task> perform)
        {
            _logger = logger;
            _perform = perform;
        }
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
            => Task.Run(async () =>
            {
                await Task.Yield();
                _logger.LogInformation("Kafka Service Stopped");

                await _perform(cancellationToken);//cancelation token itu biasanya 10 detik jika tidak ada respon
                _logger.LogInformation("Kafka Service Stopped");

            }, cancellationToken);
    }
}
