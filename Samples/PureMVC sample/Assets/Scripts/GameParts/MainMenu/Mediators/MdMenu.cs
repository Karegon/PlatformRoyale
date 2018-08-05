using UnityEngine.UI;

namespace SampleGameNamespace
{
    /*
     *  Медиатор для работы с главным меню. Без промежуточных View работает напрямую с GO, 
     *  представляющим форму главного меню.
     */
    public class MdMenu : FormMediator
    {
        public new const string NAME = "MdMenu";

        private Button btnGame;
        private Button btnSettings;
        private Button btnQuit;

        public MdMenu(object viewComponent) : base(NAME, viewComponent)
        {

        }

        /*
         * При инициализации регистрируем события
         */ 
        public override void OnRegister()
        {
            base.OnRegister();
            triggerState = MenuMessages.STATE_MAIN_MENU;
            escState = MenuMessages.STATE_QUIT;

            btnGame = Tools.FindObjectByName("btnGame").GetComponent<Button>();
            btnGame.onClick.AddListener(() =>
              MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, MenuMessages.STATE_BATTLE_ZONE));

            btnSettings = Tools.FindObjectByName("btnSettings1").GetComponent<Button>();
            btnSettings.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, MenuMessages.STATE_SETTINGS));

            btnQuit = Tools.FindObjectByName("btnQuit").GetComponent<Button>();
            btnQuit.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, MenuMessages.STATE_QUIT));
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

    }
}
