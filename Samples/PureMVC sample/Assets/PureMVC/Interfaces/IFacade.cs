namespace PureMVC.Interfaces
{
    using System;

    public interface IFacade : INotifier
    {
        bool HasCommand(string notificationName);
        bool HasMediator(string mediatorName);
        bool HasProxy(string proxyName);
        void RegisterCommand(string notificationName, Type commandType);
        void RegisterMediator(IMediator mediator);
        void RegisterProxy(IProxy proxy);
        void RemoveCommand(string notificationName);
        IMediator RemoveMediator(string mediatorName);
        IProxy RemoveProxy(string proxyName);
        IMediator RetrieveMediator(string mediatorName);
        IProxy RetrieveProxy(string proxyName);
        void NotifyObservers(INotification note);
    }
}

