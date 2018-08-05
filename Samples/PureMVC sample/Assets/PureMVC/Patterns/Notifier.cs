namespace PureMVC.Patterns
{
    using PureMVC.Interfaces;
    using System;

    /// <summary>
    /// </summary>
    public class Notifier : INotifier
    {
        private IFacade m_facade = PureMVC.Patterns.Facade.Instance;

        public void SendNotification(string notificationName)
        {
            this.m_facade.SendNotification(notificationName);
        }
        public void SendNotification(string notificationName, object body)
        {
            this.m_facade.SendNotification(notificationName, body);
        }
        public void SendNotification(string notificationName, object body, string type)
        {
            this.m_facade.SendNotification(notificationName, body, type);
        }

        protected IFacade Facade
        {
            get
            {
                return this.m_facade;
            }
        }
    }
}

