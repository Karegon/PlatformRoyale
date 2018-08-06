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

            mainMenu = Tools.FindObjectByName("MainMenu");
            settings = Tools.FindObjectByName("Settings");

            Facade.RegisterMediator(new MdMenu(mainMenu));
            Facade.RegisterMediator(new MdSettings(settings));
            Facade.RegisterMediator(new MdSound());

            // включаем главное меню
            // SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, MenuMessages.STATE_MAIN_MENU);
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
            notes.Add(MenuMessages.NOTE_STATE_SWITCH);
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
                case MenuMessages.NOTE_STATE_SWITCH:
                    Debug.Log("STATE_SWITCH " + note.Type);
                    switch (note.Type)
                    {
                        case MenuMessages.STATE_QUIT:
                            #if UNITY_EDITOR
                                // Application.Quit() не работает в редакторе 
                                UnityEditor.EditorApplication.isPlaying = false;
                            #else
                                Application.Quit();
                            #endif
                            break;
                    }
                    break;
            }
        }

    }

}
