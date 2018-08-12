using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGameNamespace
{
    /*
     * Медиатор формы паузы
     */ 
    public class MdPause : Mediator
    {

        public new const string NAME = "MdPause";

        private Button btnResume;
        private Button btnSettings;
        private Button btnQuitGame2;

        protected string triggerState;

        public MdPause(object viewComponent) : base(NAME, viewComponent)
        {

        }


        // ссылка на форму 
        public GameObject form
        {
            get { return m_viewComponent as GameObject; }
        }

        public override void OnRegister()
        {
            base.OnRegister();
            triggerState = BzMessages.STATE_PAUSE;
            btnResume = Tools.FindObjectByName("btnResume").GetComponent<Button>();
            btnResume.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BzMessages.STATE_CHANGE, null, BzMessages.STATE_IN_PROGRESS));

            btnSettings = Tools.FindObjectByName("btnSettings2").GetComponent<Button>();
            btnSettings.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BzMessages.STATE_CHANGE, null, BzMessages.STATE_SETTINGS));

            btnQuitGame2 = Tools.FindObjectByName("btnQuitGame2").GetComponent<Button>();
            btnQuitGame2.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BzMessages.STATE_CHANGE, null, BzMessages.STATE_END));
        }

        public override void OnRemove()
        {
            base.OnRemove();
            btnResume.onClick.RemoveAllListeners();
            btnSettings.onClick.RemoveAllListeners();
            btnQuitGame2.onClick.RemoveAllListeners();
        }

        /*
       * Указываем, какие нотификации хочет слушать этот медиатор
       */
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(BzMessages.STATE_CHANGE);
            notes.Add(BzMessages.KEY_PRESSED);
            return notes;
        }

        /*
         * Обрабатываем эти нотификации
         */
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case BzMessages.STATE_CHANGE:
                    if (form != null)
                        form.SetActive(triggerState == note.Type);
                    break;
                case BzMessages.KEY_PRESSED:
                    if (form != null)
                    {
                        if ((KeyCode) (int) note.Body == KeyCode.Escape) togglePause();
                    }
                    else
                        Debug.Log("form is null");
                    break;
                   
            }
        }

        private void togglePause()
        {
            string state = form.activeInHierarchy ? BzMessages.STATE_IN_PROGRESS : BzMessages.STATE_PAUSE;
            SendNotification(BzMessages.STATE_CHANGE, null, state);
        }
    }
}

