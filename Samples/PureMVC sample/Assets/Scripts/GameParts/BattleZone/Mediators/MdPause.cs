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
            triggerState = BzMessages.STATE_PAUSE;
            escState = BzMessages.STATE_IN_PROGRESS;
            /*
            btnResume = Tools.FindObjectByName("btnResume").GetComponent<Button>();
            btnResume.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BzMessages.STATE_CHANGE, null, BzMessages.STATE_IN_PROGRESS));

            btnSettings = Tools.FindObjectByName("btnSettings2").GetComponent<Button>();
            btnSettings.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BzMessages.STATE_CHANGE, null, BzMessages.STATE_END));

            btnQuitGame2 = Tools.FindObjectByName("btnQuitGame2").GetComponent<Button>();
            btnQuitGame2.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(BzMessages.STATE_CHANGE, null, BzMessages.STATE_END));
            */
        }

        public override void OnRemove()
        {
            base.OnRemove();
            /*
            btnResume.onClick.RemoveAllListeners();
            btnSettings.onClick.RemoveAllListeners();
            btnQuitGame2.onClick.RemoveAllListeners();
            */
        }
       
    }
}

