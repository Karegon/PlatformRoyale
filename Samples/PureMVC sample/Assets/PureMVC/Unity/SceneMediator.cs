using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PureMVC.Unity
{
    /// <summary>
    /// Заготовка медиатора для обслуживания целых сцен
    /// Для создания медиаторов для части сцен используй обычный Mediator
    /// </summary>
    public abstract class SceneMediator : Mediator
    {
        public SceneMediator(string mediatorName, CustomBootstrap viewComponent) : base (mediatorName, viewComponent)
        {
            Debug.Log(mediatorName + " started");
            viewComponent.onStart += onStartScene;
            viewComponent.onUpdate += onUpdateScene;
            viewComponent.onSceneChanged += onSceneChanged;
        }



        /// <summary>
        /// Вызывается, когда сцена инициализируется
        /// </summary>
        protected virtual void onStartScene()
        {
            init();
        }

        protected virtual void init()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Вызывается, когда сцена перерисовывается
        /// </summary>
        protected virtual void onUpdateScene()
        {
        }

        /// <summary>
        /// Вызывается, когда сцена инициализируется
        /// </summary>
        protected virtual void onSceneChanged(Scene current, Scene next)
        {
            Debug.Log("change scene from " + current.name + " to " + next.name);
        }

    }
}
