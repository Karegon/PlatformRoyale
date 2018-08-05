using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace SampleGameNamespace
{
    public class FormMediator : Mediator
    {
        protected string triggerState;
        private string _escState;
        protected string escState {
            get { return calcEscState(); }
            set { _escState = value; }
        }

        protected virtual string calcEscState()
        {
            return _escState;
        }

        protected PrMenuScene _prScene;

        // ссылка на форму 
        public GameObject form
        {
            get { return m_viewComponent as GameObject; }
        }

        public FormMediator(string mediatorName, object viewComponent) : base(mediatorName, viewComponent)
        {
            _prScene = Facade.RetrieveProxy(PrMenuScene.NAME) as PrMenuScene;
        }

        /*
         * Указываем, какие нотификации хочет слушать этот медиатор
         */
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(MenuMessages.NOTE_STATE_SWITCH);
            notes.Add(MenuMessages.NOTE_KEY_PRESSED);
            return notes;
        }

        /*
         * Обрабатываем эти нотификации
         */
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case MenuMessages.NOTE_KEY_PRESSED:
                    if ((KeyCode)note.Body == KeyCode.Escape && form.activeInHierarchy)
                        SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, escState);
                    break;
                case MenuMessages.NOTE_STATE_SWITCH:
                    form.SetActive(triggerState == note.Type);
                    break;
            }
        }
    }
}
