using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace PureMVC.Unity
{
    /// <summary>
    /// Предок всех загрузчиков сцен.
    /// Реализует механизм прокидывания в MVC событий от движка Unity
    /// </summary>
    public abstract class CustomBootstrap : MonoBehaviour
    {
        /*
        public event UnityAction onStart;
        public event UnityAction onUpdate;
        public event UnityAction onFixedUpdate;
        public event UnityAction<Scene, Scene> onSceneChanged;
        */

        /// <summary>
        /// Проверяет, нет ли на сцене ещё одного загрузчика (который был добавлен ранее и приехал
        /// к нам с другой сцены), и если нет, то вызывает initNewBootstrap, который должен быть переопредлен в потомках
        /// </summary>
        protected virtual void Awake()
        {
            //SceneManager.activeSceneChanged += SceneChanged;
            Debug.Log("bootstrap awake " + getFullName());
            // загрузчик стратрует фасад и всю обвязку PMVC, только если он ещё не был инициализирован
            if (!Facade.hasInstance())
            {
                initNewBootstrap();
            }
            else
            {
                resumeBootstrap();
                // а если фасад уже есть, значит его инициализировал загрузчик с другой сцены.
                // В этом случае эту инстранцию объекта следует уничтожить от греха подальше
                //Object.Destroy(this.gameObject);
            }
        }

        /// <summary>                                                                                                   
        /// Должен быть переопределен в потомках как точка входа программы. 
        /// Используется для инициализации фасада
        /// </summary>
        protected virtual void resumeBootstrap()
        {
            Debug.Log("resumeBootstrap");
        }

        /// <summary>                                                                                                   
        /// Должен быть переопределен в потомках как точка входа программы. 
        /// Используется для инициализации фасада
        /// </summary>
        protected virtual void initNewBootstrap()
        {
            Debug.Log("initNewBootstrap");
            DontDestroyOnLoad(gameObject);
        }

        

        /*
        protected virtual void Start()
        {
            Debug.Log("bootstrap start " + getFullName());
            if (onStart != null) onStart();
        }

        protected virtual void SceneChanged(Scene current, Scene next)
        {
            if (onSceneChanged != null) onSceneChanged(current, next);
        }

        protected virtual void Update()
        {
            if (onUpdate != null) onUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (onFixedUpdate != null) onFixedUpdate();
        }
        */

        private string getFullName()
        {
            return gameObject.scene.name + ':' + gameObject.name;
        }

    }
}
