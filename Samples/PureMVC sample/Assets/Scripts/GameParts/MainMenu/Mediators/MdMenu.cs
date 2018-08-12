using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGameNamespace
{
    /*
     *  Медиатор для работы с главным меню. Без промежуточных View работает напрямую с GO, 
     *  представляющим форму главного меню.
     */
    public class MdMenu : Mediator
    {
        public new const string NAME = "MdMenu";

        private Button btnGame;
        private Button btnSettings;
        private Button btnQuit;

        protected string triggerState;

        public MdMenu(object viewComponent) : base(NAME, viewComponent)
        {
            
        }

        // ссылка на форму 
        public GameObject form
        {
            get { return m_viewComponent as GameObject; }
        }

        /*
         * При инициализации регистрируем события
         */
        public override void OnRegister()
        {
            base.OnRegister();

            triggerState = MenuMessages.STATE_MENU;

            btnGame = Tools.FindObjectByName("btnGame").GetComponent<Button>();
            btnGame.onClick.AddListener(() =>
              MyGameFacade.Instance.SendNotification(BaseMessages.NOTE_SWITCH_SCENE, 1, BaseMessages.SCENE_BATTLE_ZONE));

            btnSettings = Tools.FindObjectByName("btnSettings1").GetComponent<Button>();
            btnSettings.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MenuMessages.STATE_CHANGE, null, MenuMessages.STATE_SETTINGS));

            btnQuit = Tools.FindObjectByName("btnQuit").GetComponent<Button>();
            btnQuit.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BaseMessages.NOTE_APP_QUIT_REQUEST));
        }

        /*
         *  Очищаем ресурсы при удалении
         */ 
        public override void OnRemove()
        {
            base.OnRemove();
            btnGame.onClick.RemoveAllListeners();
            btnSettings.onClick.RemoveAllListeners();
            btnQuit.onClick.RemoveAllListeners();
        }

        /*
        * Указываем, какие нотификации хочет слушать этот медиатор
        */
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(MenuMessages.STATE_CHANGE);
            return notes;
        }

        /*
         * Обрабатываем эти нотификации
         */
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case MenuMessages.STATE_CHANGE:
                    if (form != null)
                        form.SetActive(triggerState == note.Type);
                    break;
            }
        }

    }
}
