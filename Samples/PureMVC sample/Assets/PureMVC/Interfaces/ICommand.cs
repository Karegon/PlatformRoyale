namespace PureMVC.Interfaces
{
    using System;

    public interface ICommand
    {
        void Execute(INotification note);
    }
}

