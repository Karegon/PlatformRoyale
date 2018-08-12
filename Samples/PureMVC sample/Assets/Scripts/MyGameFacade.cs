using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;


namespace SampleGameNamespace
{
    /// <summary>
    /// Конкретный фасад для этого приложения
    /// Здесь можно хранить всякие ссылки на нужные объекты, кешировать всякие рулезы и пр. 
    /// Фасад - типичный синглтон. Всё обо всех знает и может дать потрогать, если хорошо попросишь.
    /// </summary>
    public class MyGameFacade : Facade
    {
        public Bootstrap bootstrap;

        public MyGameFacade() : base()
        {
            m_instance = this;
            Debug.Log("Creating App Facade");
        }

        // переопределим проперть, чтобы возвращала инстанцию нужного типа
        public new static MyGameFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new MyGameFacade();
                        }
                    }
                }
                return m_instance as MyGameFacade;
            }
        }

        public static GameObject go()
        {
            return (Instance as MyGameFacade).bootstrap.gameObject;
        }

        /// <summary>
        /// Старт фрейморка 
        /// </summary>
        /// <param name="bootstrap">загрузчик, который останется на всех сценах (DontDestroyOnLoad) </param>
        public void startup(Bootstrap bootstrap)
        {
            Application.quitting += onQuitting;
            this.bootstrap = bootstrap;
            SendNotification(BaseMessages.CMD_GAME_STARTUP, bootstrap);
        }

        private void onQuitting()
        {
            SendNotification(BaseMessages.NOTE_APP_QUIT);
            SendNotification(BaseMessages.CMD_GAME_SHUTDOWN);
        }

        /// <summary>
        /// повторный запуск фреймворка при загрузке новой сцены
        /// </summary>
        public void resume()
        {
            //SendNotification(BaseMessages.NOTE_SCENE_PREPARE);
        }

        protected override void InitializeFacade()
        {
            base.InitializeFacade();
        }

        protected override void InitializeView()
        {
            base.InitializeView();
        }

        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(BaseMessages.CMD_GAME_STARTUP, typeof(CmdLoaderStartup));
            RegisterCommand(BaseMessages.CMD_GAME_SHUTDOWN, typeof(CmdLoaderShutdown));
            RegisterCommand(MenuMessages.CMD_MENU_STARTUP, typeof(CmdMenuStartup));
            RegisterCommand(MenuMessages.CMD_MENU_SHUTDOWN, typeof(CmdMenuShutdown));
            RegisterCommand(BzMessages.CMD_BATTLE_ZONE_STARTUP, typeof(CmdBattleZoneStartup));
            RegisterCommand(BzMessages.CMD_BATTLE_ZONE_SHUTDOWN, typeof(CmdBattleZoneShutdown));
        }

        protected override void InitializeModel()
        {
            base.InitializeModel();
        }




    }

}
