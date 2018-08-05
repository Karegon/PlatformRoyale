using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using UnityEngine;

namespace PureMVC.Unity
{
    /*
     *
     *  Нотификатор, унаследованный от MonoBehaviour
     *  Копирует класс Notifier
     * 
     */

    public class NotifierBehavior : CustomBehavior, INotifier
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
