using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using PureMVC.Unity;
using UnityEngine;

namespace SampleGameNamespace
{
    /*
     * Медиатор сцены
     */ 
    public class MdMenuScene : Mediator
    {
        public new const string NAME = "MdMenuScene";

        // ссылка на прокси сцены
        private PrMenuScene _prScene;

        // ссылки на формы 
        private GameObject settings;
        private GameObject mainMenu;

        public MdMenuScene() : base(NAME, null)
        {
        }

        public override void OnRegister()
        {
            base.OnRegister();
            Debug.Log("OnRegister " + NAME);
            _prScene = Facade.RetrieveProxy(PrMenuScene.NAME) as PrMenuScene;


            Transform canvas = Tools.FindObjectByName(MyResources.DEF_CANVAS_NAME).transform;
            // меню настроек
            var settings = Tools.instantiateObject(MyResources.FROM_SETTINGS, canvas);
            settings.SetActive(false);
            Debug.Log(MyResources.FROM_SETTINGS + " created");

            mainMenu = Tools.FindObjectByName("MainMenu");

            Facade.RegisterMediator(new MdMenu(mainMenu));
            Facade.RegisterMediator(new MdSettings(settings));
            Facade.RegisterMediator(new MdSound());
        }
        

        public override void OnRemove()
        {
            base.OnRemove();
            Debug.Log("OnRemove " + NAME);
            Facade.RemoveMediator(MdMenu.NAME);
            Facade.RemoveMediator(MdSettings.NAME);
            Facade.RemoveMediator(MdSound.NAME);
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(MenuMessages.NOTE_SETTINGS_LEVEL);
            return notes;
        }
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case MenuMessages.NOTE_SETTINGS_LEVEL:
                    _prScene.difficulty = (Difficulty) note.Body;
                    Debug.Log("NOTE_SETTINGS_LEVEL " + _prScene.difficulty);
                    break;
            }
        }

    }

}
