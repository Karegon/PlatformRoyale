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
                Debug.Log("change scene to " + value);
                if (value != SceneManager.GetActiveScene().name)
                {
                    var data = findSceneData(_currentScene);
                    if (data != null) SendNotification(data.shutdownCommand); 
                    _currentScene = value;

                    //SceneManager.LoadScene(_currentScene, LoadSceneMode.Single);
                    bootstrap.StartCoroutine(loadScene());
                }
            }
        }

        public MdSceneController(object viewComponent) : base(NAME, viewComponent)
        {
            sceneList = new List<SceneInitData>
            {
                { new SceneInitData("Loader", MenuMessages.CMD_SHOW_DEFALUT_SCENE, MenuMessages.CMD_LOADER_SHUTDOWN) },
                { new SceneInitData("MainMenu", MenuMessages.CMD_MENU_STARTUP, MenuMessages.CMD_MENU_SHUTDOWN) },
                { new SceneInitData("Level1", BzMessages.CMD_BATTLE_ZONE_STARTUP, BzMessages.CMD_BATTLE_ZONE_SHUTDOWN) }
            };
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
            sceneLoadComplete();
        }

        private void sceneLoadComplete()
        {
            Debug.Log("Loading complete " + _currentScene);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_currentScene));
        }

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

        public override void OnRegister()
        {
            base.OnRegister();
            Debug.Log("OnRegister " + NAME);
            SceneManager.activeSceneChanged += SceneChanged;
            initRoute();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            Debug.Log("OnRemove " + NAME);
            SceneManager.activeSceneChanged -= SceneChanged;
        }

        protected virtual void SceneChanged(Scene current, Scene next)
        {
            Debug.Log("SceneChanged " + current.name + ">" + next.name);
            Debug.Log("Setting active scene " + SceneManager.GetActiveScene().name);
            var data = findSceneData(next.name);
            if (data != null) SendNotification(data.startupCommand);
        }

        /// <summary>
        /// Определят, на какой сцене мы находимся и вызывает соответствующую команду инициализации
        /// </summary>
        private void initRoute()
        {
            Debug.Log("initRoute");
            /*
           var sceneName = SceneManager.GetActiveScene().name;
            var data = findSceneData(sceneName);
            if (data != null) SendNotification(data.startupCommand);
            */
        }


        /*
         * Указываем, какие нотификации хочет слушать этот медиатор
         */
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(MenuMessages.NOTE_STATE_SWITCH);
            notes.Add(BaseMessages.NOTE_SCENE_PREPARE);
            return notes;
        }

        /*
         * Обрабатываем эти нотификации
         */
        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case MenuMessages.NOTE_STATE_SWITCH:
                    if (note.Type == MenuMessages.STATE_MAIN_MENU)
                    {
                        currentScene = "MainMenu";
                    }
                    else if (note.Type == MenuMessages.STATE_BATTLE_ZONE)
                    {
                        currentScene = "Level1";
                    }
                    break;
                case BaseMessages.NOTE_SCENE_PREPARE:
                    initRoute();
                    break;
            }
        }

    }
}