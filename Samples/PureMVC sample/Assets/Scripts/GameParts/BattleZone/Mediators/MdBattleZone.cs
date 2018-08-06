using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Unity;
using UnityEngine;


namespace SampleGameNamespace
{
    /// <summary>
    /// Медиатор обслуживает зону сражения
    /// Должен создать:
    /// - медиатор игрока
    /// - медиатор интерфейса игры
    /// - медиаторы модальных форм
    /// - медиатор ввода 
    /// - медиаторы врагов
    /// - медиаторы предметов
    /// - медиатор уровня?
    /// </summary>
    public class MdBattleZone : Mediator
    {

        public new const string NAME = "MdBattleZone";

        // ссылка на прокси сцены
        private PrBattleZone _prBattleZone;

        // ссылки на формы 
        private GameObject hero;
        private GameObject hud;
        private GameObject pauseForm;
        private GameObject settingsForm;
    
        public MdBattleZone() : base(NAME)
        {

        }

        public override void OnRegister()
        {
            Debug.Log("OnRegister " + NAME);
            _prBattleZone = Facade.RetrieveProxy(PrBattleZone.NAME) as PrBattleZone;

            hero = Tools.FindObjectByName("Hero");
            pauseForm = Tools.FindObjectByName("PauseForm");
            //settingsForm = Tools.FindObjectByName("SettingsForm");

            MdPlayerInput mdPlayerInput = UnityEngine.Object.FindObjectOfType<MdPlayerInput>();
            MdHud mdHud = UnityEngine.Object.FindObjectOfType<MdHud>();
            MdHero mdHero = UnityEngine.Object.FindObjectOfType<MdHero>();
            MdWeapon mdWeapon = UnityEngine.Object.FindObjectOfType<MdWeapon>();

            //_prGame.sceneStart();
            // Facade.RegisterMediator(new MdHud(hud));
            Facade.RegisterMediator(new MdPause(pauseForm));
            //Facade.RegisterMediator(new MdSettings(settingsForm));
            Facade.RegisterMediator(new MdSound());
            Facade.RegisterMediator(mdPlayerInput);
            Facade.RegisterMediator(mdHud);
            Facade.RegisterMediator(mdHero);
            Facade.RegisterMediator(mdWeapon);

            // запускаем игру
            //SendNotification(BzMessages.BZ_CMD_INIT);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            Debug.Log("OnRemove " + NAME);
            // Facade.RemoveMediator(MdSettings.NAME);
            Facade.RemoveMediator(MdPause.NAME);
            Facade.RemoveMediator(MdSound.NAME);
            Facade.RemoveMediator(MdPlayerInput.NAME);
            Facade.RemoveMediator(MdHud.NAME);
            Facade.RemoveMediator(MdHero.NAME);
            Facade.RemoveMediator(MdWeapon.NAME);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(BzMessages.STATE_CHANGE);
            notes.Add(BzMessages.STATE_WAS_CHANGED);
            notes.Add(BzMessages.AVATAR_DEAD);
            return notes;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case BzMessages.STATE_CHANGE:
                    _prBattleZone.setStateByMessage(note.Type);
                    break;
                case BzMessages.STATE_WAS_CHANGED:
                    break;
                case BzMessages.AVATAR_DEAD:
                    SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, MenuMessages.STATE_MAIN_MENU);
                    break;


            }
        }
    }
}
