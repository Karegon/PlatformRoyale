using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using SampleGameNamespace;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;

namespace SampleGameNamespace
{
    /*
     * Медиатор формы настроек
     */ 
    public class MdSettings : FormMediator
    {
        public new const string NAME = "MdSettings";

        private Button btnClose;
        private Slider sldVolume;
        private Toggle tglEasy;
        private Toggle tglNormal;
        private Toggle tglHard;
        private Toggle tglAudio;
        private Toggle tglMusic;

        public MdSettings(object viewComponent) : base(NAME, viewComponent)
        {
        }

        public override void OnRegister()
        {
            base.OnRegister();

            triggerState = MenuMessages.STATE_SETTINGS;
            escState = MenuMessages.STATE_MAIN_MENU;

            btnClose = Tools.FindObjectByName("btnClose").GetComponent<Button>();
            btnClose.onClick.AddListener(() =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, escState));

            sldVolume = Tools.FindObjectByName("sldVolume").GetComponent<Slider>();
            sldVolume.value = _prScene.audioSettings.masterVolume;
            sldVolume.onValueChanged.AddListener((float value) =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_SETTINGS_VOLUME, value));

            tglEasy = Tools.FindObjectByName("tglEasy").GetComponent<Toggle>();
            tglNormal = Tools.FindObjectByName("tglNormal").GetComponent<Toggle>();
            tglHard = Tools.FindObjectByName("tglHard").GetComponent<Toggle>();

            tglEasy.isOn = (_prScene.difficulty == Difficulty.difEasy);
            tglNormal.isOn = (_prScene.difficulty == Difficulty.difNormal);
            tglHard.isOn = (_prScene.difficulty == Difficulty.difHard);

            tglEasy.onValueChanged.AddListener((bool value) =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_SETTINGS_LEVEL, Difficulty.difEasy));
            tglNormal.onValueChanged.AddListener((bool value) =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_SETTINGS_LEVEL, Difficulty.difNormal));
            tglHard.onValueChanged.AddListener((bool value) =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_SETTINGS_LEVEL, body: Difficulty.difHard));

            tglAudio = Tools.FindObjectByName("tglAudio").GetComponent<Toggle>();
            tglAudio.isOn = _prScene.audioSettings.isUseAudio;
            tglAudio.onValueChanged.AddListener((bool value) =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_SETTINGS_AUDIO, value));

            tglMusic = Tools.FindObjectByName("tglMusic").GetComponent<Toggle>();
            tglMusic.isOn = _prScene.audioSettings.isUseMusic;
            tglMusic.onValueChanged.AddListener((bool value) =>
                MyGameFacade.Instance.SendNotification(MenuMessages.NOTE_SETTINGS_MUSIC, value));
        }

        public override void OnRemove()
        {
            base.OnRemove();
            btnClose.onClick.RemoveAllListeners();
            sldVolume.onValueChanged.RemoveAllListeners();
            tglEasy.onValueChanged.RemoveAllListeners();
            tglNormal.onValueChanged.RemoveAllListeners();
            tglHard.onValueChanged.RemoveAllListeners();
            tglAudio.onValueChanged.RemoveAllListeners();
            tglHard.onValueChanged.RemoveAllListeners();
        }

        protected override string calcEscState()
        {
            return _prScene.prevState;
        }



    }
}
