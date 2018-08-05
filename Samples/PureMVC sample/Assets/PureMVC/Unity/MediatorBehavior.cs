using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;

namespace PureMVC.Unity
{
    /*
     * Калька с медиатора, но с поддержкой MonoBehavior
     * Используй этот класс, если нужно создать медиатор с поддержкой событий класса MonoBehavior
     * Пригодится, если нужно сделать View как объект сцены.
     */
    public class MediatorBehavior : NotifierBehavior, IMediator
    {
        protected string m_mediatorName;
        protected object m_viewComponent;
        public const string NAME = "MediatorBehavior";

        public MediatorBehavior() : this(NAME, null)
        {
        }

        public MediatorBehavior(string mediatorName) : this(mediatorName, null)
        {
        }

        public MediatorBehavior(string mediatorName, object viewComponent)
        {
            this.m_mediatorName = (mediatorName != null) ? mediatorName : NAME;
            this.m_viewComponent = viewComponent;
        }

        /// <summary>
        /// </summary>
        /// <param name="notification"></param>
        public virtual void HandleNotification(INotification notification)
        {
        }

        public virtual IList<string> ListNotificationInterests()
        {
            return new List<string>();
        }

        public virtual void OnRegister()
        {
        }

        public virtual void OnRemove()
        {
        }

        public virtual string MediatorName
        {
            get
            {
                return this.m_mediatorName;
            }
        }

        public object ViewComponent
        {
            get
            {
                return this.m_viewComponent;
            }
            set
            {
                this.m_viewComponent = value;
            }
        }
    }
}
