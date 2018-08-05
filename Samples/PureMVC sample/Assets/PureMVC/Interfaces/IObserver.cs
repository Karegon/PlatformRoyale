namespace PureMVC.Interfaces
{
    using System;

    public interface IObserver
    {
        //NotifyContext
        bool CompareNotifyContext(object obj);
        void NotifyObserver(INotification notification);
        //Mediator Command
        object NotifyContext { set; }
        string NotifyMethod { set; }
    }
}

