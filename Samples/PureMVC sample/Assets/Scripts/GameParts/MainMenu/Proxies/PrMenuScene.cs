using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;


namespace SampleGameNamespace
{

    public enum Difficulty
    {
        difEasy,
        difNormal,
        difHard
    }
    /// <summary>
    /// Прокси для сцены1
    /// </summary>
    public class PrMenuScene : Proxy
    {
        public new const string NAME = "PrMenuScene";

        // сложность игры
        public Difficulty difficulty;

        // предыдущее состояние машины
        public string prevState;
        // текущее состояние машины
        private string _curState;
        public string curState {
            get { return _curState; }
            set
            {
                prevState = _curState;
                _curState = value;
            }
        }

        public AudioSettings audioSettings = new AudioSettings();

        public PrMenuScene() : base (NAME, null)
        {
            Debug.Log(NAME + " started");
        }

        public void sceneStart()
        {
        }

        public void sceneUpdate()
        {
        }

    }
}
