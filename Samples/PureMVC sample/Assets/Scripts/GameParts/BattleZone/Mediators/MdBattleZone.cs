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
    public class MdBattleZone : SceneMediator
    {

        public new const string NAME = "MdBattleZone";

        // ссылка на прокси сцены
        private PrBattleZone _prBattleZone;

        // ссылки на формы 
        private GameObject hero;
        private GameObject hud;
        private GameObject pauseForm;
        private GameObject settingsForm;
    
        public MdBattleZone(Bootstrap view) : base(NAME, view)
        {

        }

        protected override void init()
        {
            Debug.Log("init objects");
            _prBattleZone = Facade.RetrieveProxy(PrBattleZone.NAME) as PrBattleZone;

            hero = Tools.FindObjectByName("Hero");
            pauseForm = Tools.FindObjectByName("PauseForm");
            //settingsForm = Tools.FindObjectByName("SettingsForm");

        }

        protected override void onStartScene()
        {
            base.onStartScene();

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
           // Facade.RemoveMediator(MdSettings.NAME);
            Facade.RemoveMediator(MdPause.NAME);
            Facade.RemoveMediator(MdSound.NAME);
            Facade.RemoveMediator(MdPlayerInput.NAME);
            Facade.RemoveMediator(MdHud.NAME);
            Facade.RemoveMediator(MdHero.NAME);
            Facade.RemoveMediator(MdWeapon.NAME);
        }

        protected override void onUpdateScene()
        {
            base.onUpdateScene();
            //_prGame.sceneUpdate();
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(BzMessages.STATE_CHANGE);
            notes.Add(BzMessages.STATE_WAS_CHANGED);
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

            }
        }
    }
}
