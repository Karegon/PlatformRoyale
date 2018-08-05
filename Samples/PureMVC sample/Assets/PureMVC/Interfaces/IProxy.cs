namespace PureMVC.Interfaces
{
    using System;

    public interface IProxy
    {
        void OnRegister();
        void OnRemove();

        object Data { get; set; }
        string ProxyName { get; }
    }
}

