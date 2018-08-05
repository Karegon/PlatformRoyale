using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.Audio;

namespace SampleGameNamespace
{
    /// <summary>
    /// Управляет звуком на сцене
    /// </summary>
    class MdSound : Mediator
    {
        public new const string NAME = "MdSound";
        protected PrMenuScene PrMenuScene;
        private AudioMixer audioMixer;

        // ствол текущего ружья

        public MdSound() : base(NAME)
        {
            Debug.Log("MdSound constructor");
        }

        public override void OnRegister()
        {
            base.OnRegister();
            PrMenuScene = Facade.RetrieveProxy(PrMenuScene.NAME) as PrMenuScene;
            audioMixer = Resources.Load<AudioMixer>("MyAudioMixer");
            Update();
        }

        private void Update()
        {
            if (audioMixer)
            {
                //audioMixer.SetFloat("Ambient", prScene1.audioSettings.ambientVolume);
                audioMixer.SetFloat("Master", PrMenuScene.audioSettings.masterVolume);
                audioMixer.SetFloat("Music", PrMenuScene.audioSettings.musicVolume);
                //audioMixer.SetFloat("Player", prScene1.audioSettings.playerVolume);
                var value = PrMenuScene.audioSettings.isUseMusic ? PrMenuScene.audioSettings.musicVolume : -100f;
                audioMixer.SetFloat("Music", value);
                value = PrMenuScene.audioSettings.isUseAudio ? PrMenuScene.audioSettings.ambientVolume : -100f;
                audioMixer.SetFloat("Ambient", value);
                value = PrMenuScene.audioSettings.isUseAudio ? PrMenuScene.audioSettings.playerVolume : -100f;
                audioMixer.SetFloat("Player", value);
            }
        }

        public override void OnRemove()
        {
            base.OnRemove();
            PrMenuScene = null;
        }


        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(MenuMessages.NOTE_SETTINGS_MUSIC);
            notes.Add(MenuMessages.NOTE_SETTINGS_AUDIO);
            notes.Add(MenuMessages.NOTE_SETTINGS_VOLUME);
            return notes;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case MenuMessages.NOTE_SETTINGS_MUSIC:
                    PrMenuScene.audioSettings.isUseMusic = (bool)note.Body;
                    Update();
                    break;
                case MenuMessages.NOTE_SETTINGS_AUDIO:
                    PrMenuScene.audioSettings.isUseAudio = (bool)note.Body;
                    Update();
                    break;
                case MenuMessages.NOTE_SETTINGS_VOLUME:
                    PrMenuScene.audioSettings.masterVolume = (float) note.Body;
                    Update();
                    break;

            }
        }

    }

}
