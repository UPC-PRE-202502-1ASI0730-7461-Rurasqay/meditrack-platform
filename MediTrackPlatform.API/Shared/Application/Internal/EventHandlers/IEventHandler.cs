using Cortex.Mediator.Notifications;
using MediTrackPlatform.API.Shared.Domain.Model.Events;

namespace MediTrackPlatform.API.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{ }