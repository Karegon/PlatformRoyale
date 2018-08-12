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
            pauseForm = Tools.FindObjectByName("FrmPause" + "(Clone)");
            settingsForm = Tools.FindObjectByName("SettingsForm" + "(Clone)");

            MdPlayerInput mdPlayerInput = UnityEngine.Object.FindObjectOfType<MdPlayerInput>();
            MdHud mdHud = UnityEngine.Object.FindObjectOfType<MdHud>();
            MdHero mdHero = UnityEngine.Object.FindObjectOfType<MdHero>();
            MdWeapon mdWeapon = UnityEngine.Object.FindObjectOfType<MdWeapon>();

            Transform canvas = Tools.FindObjectByName(MyResources.DEF_CANVAS_NAME).transform;
            // меню паузы
            pauseForm = Tools.instantiateObject(MyResources.FROM_PAUSE, canvas);
            pauseForm.SetActive(false);
            Debug.Log(MyResources.FROM_PAUSE + " created");

            // меню настроек
            settingsForm = Tools.instantiateObject(MyResources.FROM_SETTINGS, canvas);
            settingsForm.SetActive(false);
            Debug.Log(MyResources.FROM_SETTINGS + " created");


            //_prGame.sceneStart();
            Facade.RegisterMediator(new MdPause(pauseForm));
            Facade.RegisterMediator(new MdSettings(settingsForm));
            Facade.RegisterMediator(new MdSound());
            Facade.RegisterMediator(mdPlayerInput);
            Facade.RegisterMediator(mdHud);
            Facade.RegisterMediator(mdHero);
            Facade.RegisterMediator(mdWeapon);
        }

        public override void OnRemove()
        {
            base.OnRemove();
            Debug.Log("OnRemove " + NAME);
            Facade.RemoveMediator(MdPause.NAME);
            Facade.RemoveMediator(MdSettings.NAME);
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
                    if ((ZoneState)(int)note.Body == ZoneState.zsEnd)
                    {
                        Debug.Log(note.Type);
                        SendNotification(BaseMessages.NOTE_SWITCH_SCENE, null, BaseMessages.SCENE_MAIN_MENU);
                    }
                    break;
                case BzMessages.AVATAR_DEAD:
                    SendNotification(BaseMessages.NOTE_SWITCH_SCENE, null, BaseMessages.SCENE_MAIN_MENU);
                    break;


            }
        }
    }
}
