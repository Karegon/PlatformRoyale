namespace PureMVC.Interfaces
{
    using System;

    public interface IController
    {
        void ExecuteCommand(INotification notification);
        bool HasCommand(string notificationName);
        void RegisterCommand(string notificationName, Type commandType);
        void RemoveCommand(string notificationName);
    }
}