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
    public class MdPause : FormMediator
    {

        public new const string NAME = "MdPause";

        private Button btnResume;
        private Button btnSettings;
        private Button btnQuitGame2;

        public MdPause(object viewComponent) : base(NAME, viewComponent)
        {

        }

        public override void OnRegister()
        {
            base.OnRegister();
            /*
            triggerState = MyMessages.STATE_PAUSE;
            escState = MyMessages.STATE_GAME;

            btnResume = Tools.FindObjectByName("btnResume").GetComponent<Button>();
            btnResume.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MyMessages.NOTE_STATE_SWITCH, null, MyMessages.STATE_GAME));

            btnSettings = Tools.FindObjectByName("btnSettings2").GetComponent<Button>();
            btnSettings.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MyMessages.NOTE_STATE_SWITCH, null, MyMessages.STATE_SETTINGS));

            btnQuitGame2 = Tools.FindObjectByName("btnQuitGame2").GetComponent<Button>();
            btnQuitGame2.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MyMessages.NOTE_STATE_SWITCH, null, MyMessages.STATE_MAIN_MENU));
                */
        }

        public override void OnRemove()
        {
            base.OnRemove();
            btnResume.onClick.RemoveAllListeners();
            btnSettings.onClick.RemoveAllListeners();
            btnQuitGame2.onClick.RemoveAllListeners();
        }
       
    }
}

