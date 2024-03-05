using Framework.Core.Event;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Projection
{
    //mendaftarkan services
    public static class EntityProjection
    {
        public static IServiceCollection Projection(this IServiceCollection services, Action<ProjectionBuilder> builder)
        {
            builder(new ProjectionBuilder(services));
            return services;
        }
    }

    public class ProjectionBuilder
    {
        public readonly IServiceCollection _services;
        public ProjectionBuilder(IServiceCollection services)
        {
            _services = services;
        }
        public ProjectionBuilder AddOn<TEvent>(Func<EventEnvelope<TEvent>, bool> onHandle) where TEvent : notnull
        {
            _services.AddTransient<IEventHandler<EventEnvelope<TEvent>>>(sp =>
            {
                return new AddProjection<TEvent>(onHandle);
            });
            return this;
        }
    }

    public class AddProjection<TEvent> : IEventHandler<EventEnvelope<TEvent>> where TEvent : notnull
    {
        private readonly Func<EventEnvelope<TEvent>, bool> _onCreate;
        public AddProjection(Func<EventEnvelope<TEvent>, bool> onCreate)
        {
            _onCreate = onCreate;
        }
        public async Task Handle(EventEnvelope<TEvent> eventEnvelop, CancellationToken cancellationToken)
        {
            var view = _onCreate(eventEnvelop);
        }
    }
}
