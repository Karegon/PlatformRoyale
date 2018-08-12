using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SampleGameNamespace
{

    public class SceneInitData
    {
        public string sceneName;
        public string startupCommand;
        public string shutdownCommand;

        public SceneInitData(string sceneName, string startupCommand, string shutdownCommand)
        {
            this.sceneName = sceneName;
            this.startupCommand = startupCommand;
            this.shutdownCommand = shutdownCommand;
        }
    }

    /// <summary>
    /// Обслуживает переключение игровых сцен
    /// </summary>
    public class MdSceneController : Mediator
    {

        public new const string NAME = "MdSceneController";

        // список доступных сцен в игре и комнад для их запуска
        public List<SceneInitData> sceneList;

        // ссылка на форму 
        public Bootstrap bootstrap
        {
            get { return m_viewComponent as Bootstrap; }
        }

        // текущая сцена
        private string _currentScene;
        public string currentScene
        {
            get
            {
                return _currentScene;
            }

            set
            {
                Debug.Log("try to change scene to " + value);
                Debug.Log("SceneManager.GetActiveScene().name=" + SceneManager.GetActiveScene().name);
                if (value != _currentScene)
                {
                    Debug.Log("change scene to " + value);
                    /*
                    var data = findSceneData(_currentScene);
                    if (data != null) SendNotification(data.shutdownCommand); 
                    */
                    _currentScene = value;

                    //SceneManager.LoadScene(_currentScene, LoadSceneMode.Single);
                    bootstrap.StartCoroutine(loadScene());
                }
            }
        }

        public MdSceneController(object viewComponent) : base(NAME, viewComponent)
        {
            // регистрация всех сцен игры в хандлере сцен
            sceneList = new List<SceneInitData>
            {
                { new SceneInitData("MainMenu", MenuMessages.CMD_MENU_STARTUP, MenuMessages.CMD_MENU_SHUTDOWN) },
                { new SceneInitData("Level1", BzMessages.CMD_BATTLE_ZONE_STARTUP, BzMessages.CMD_BATTLE_ZONE_SHUTDOWN) },
                { new SceneInitData("Level2", BzMessages.CMD_BATTLE_ZONE_STARTUP, BzMessages.CMD_BATTLE_ZONE_SHUTDOWN) }
            };
        }

        public override void OnRegister()
        {
            base.OnRegister();
            Debug.Log("OnRegister " + NAME);
            SceneManager.activeSceneChanged += onSceneChanged;
            SceneManager.sceneLoaded += onSceneLoaded;
            SceneManager.sceneUnloaded += onSceneUnloaded;
        }

        public override void OnRemove()
        {
            base.OnRemove();
            Debug.Log("OnRemove " + NAME);
            SceneManager.activeSceneChanged -= onSceneChanged;
            SceneManager.sceneLoaded -= onSceneLoaded;
            SceneManager.sceneUnloaded -= onSceneUnloaded;
        }


        IEnumerator loadScene()
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Single);
            async.allowSceneActivation = false;
            while (async.progress <= 0.89f)
            {
                //progressText.text = async.progress.ToString();
                yield return null;
            }
            async.allowSceneActivation = true;
            //sceneLoadComplete();
        }

        /*
        private void sceneLoadComplete()
        {
            Debug.Log("Loading complete " + _currentScene);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentScene));
        }
        */

        /// <summary>
        /// Вернет данные по сцене или null, если сцена не найдена
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        public SceneInitData findSceneData(string sceneName)
        {
            SceneInitData res = null;
            foreach (var data in sceneList)
            {
                if (data.sceneName == sceneName)
                {
                    res = data;
                    break;
                }
            }
            return res;
        }

        private void onSceneLoaded(Scene scence, LoadSceneMode mode)
        {
            Debug.Log("onSceneLoaded " + scence.name);
            //Debug.Log("currentScene: " + currentScene);
            // Проверка на активацию определенной сцены важна, так как загружаться может несколько сцен
            if (scence.name == _currentScene)
            {
                Debug.Log("Setting active scene " + SceneManager.GetActiveScene().name);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentScene));
                var data = findSceneData(_currentScene);
                if (data != null) SendNotification(data.startupCommand);
            }
        }

        private void onSceneUnloaded(Scene scence)
        {
            Debug.Log("onSceneUnloaded " + scence.name);
            // если какая-то из сцен выгружается, необходимо выполнить команду её отключения
            var data = findSceneData(scence.name);
            if (data != null) SendNotification(data.shutdownCommand);
        }

        protected virtual void onSceneChanged(Scene current, Scene next)
        {
            Debug.Log("SceneChanged " + current.name + ">" + next.name);
        }

        /// <summary>
        /// Определят, какая сцена должна отобразиться в начале игры.
        /// Если в Юнити открыта одна из сцен, то открывается именно она
        /// Если открыта сцена с лоадером, то приложение пойдет по стандартному сценарию, 
        /// последовально открывая сцены, как при загрузке пользователем
        /// </summary>
        private void initStartScene()
        {
            Debug.Log("initMainScene");
            string startScene = SceneManager.GetActiveScene().name == "Loader" ? "MainMenu" : SceneManager.GetActiveScene().name;
            currentScene = startScene;
        }

        /*
         * Указываем, какие нотификации хочет слушать этот медиатор
         */
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new List<string>
            {
                BaseMessages.NOTE_SWITCH_SCENE,
                BaseMessages.NOTE_SCENE_PREPARE,
                BaseMessages.NOTE_APP_QUIT_REQUEST
            };
            return notes;
        }

        /*
         * Обрабатываем эти нотификации
         */
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case MenuMessages.NOTE_SWITCH_SCENE:
                    Debug.Log("Scene switch request: " + note.Type);

                    if (note.Type == BaseMessages.SCENE_UNKNOWN)
                    {
                        initStartScene();
                    }
                    else if (note.Type == MenuMessages.SCENE_MAIN_MENU)
                    {
                        currentScene = "MainMenu";
                    }
                    else if (note.Type == MenuMessages.SCENE_BATTLE_ZONE)
                    {
                        var level = (int) note.Body;
                        currentScene = "Level" + level;
                    }
                    break;
                case BaseMessages.NOTE_SCENE_PREPARE:
                    initStartScene();
                    break;
                case BaseMessages.NOTE_APP_QUIT_REQUEST:
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                    break;
            }
        }

    }
}