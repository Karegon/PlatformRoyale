namespace PureMVC.Interfaces
{
    using System;

    public interface IView
    {
        bool HasMediator(string mediatorName);
        void RegisterMediator(IMediator mediator);
        IMediator RemoveMediator(string mediatorName);
        IMediator RetrieveMediator(string mediatorName);
        void NotifyObservers(INotification note);
        void RegisterObserver(string notificationName, IObserver observer);
        void RemoveObserver(string notificationName, object notifyContext);
    }
}