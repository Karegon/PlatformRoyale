namespace PureMVC.Interfaces
{
    using System;

    public interface IModel
    {
        bool HasProxy(string proxyName);
        void RegisterProxy(IProxy proxy);
        IProxy RemoveProxy(string proxyName);
        IProxy RetrieveProxy(string proxyName);
    }
}

