namespace PureMVC.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IMediator
    {
        void HandleNotification(INotification note);
        IList<string> ListNotificationInterests();
        void OnRegister();
        void OnRemove();
        string MediatorName { get; }
        object ViewComponent { get; set; }
    }
}

