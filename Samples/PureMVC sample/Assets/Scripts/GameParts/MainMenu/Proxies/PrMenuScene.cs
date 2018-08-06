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

        public AudioSettings audioSettings = new AudioSettings();

        public PrMenuScene() : base (NAME, null)
        {
            Debug.Log(NAME + " started");
        }

    }
}
