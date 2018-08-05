namespace PureMVC.Interfaces
{
    using System;

    public interface INotification
    {
        string ToString();

        object Body { get; set; }

        string Name { get; }

        string Type { get; set; }
    }
}

