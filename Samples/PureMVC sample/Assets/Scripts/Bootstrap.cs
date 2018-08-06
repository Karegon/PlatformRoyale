using System.Collections;
using System.Collections.Generic;
using SampleGameNamespace;
using PureMVC.Patterns;
using PureMVC.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace SampleGameNamespace
{
    /// <summary>
    /// Загрузчик фасада игры. Вешается на игровой объект сцены и запускает необходимые механизмы для инициализации PureMVC
    /// </summary>
    public class Bootstrap : CustomBootstrap
    {
        /// <summary>
        /// Инициализируем новый загрузчик PMVC
        /// </summary>
        protected override void initNewBootstrap()
        {
            base.initNewBootstrap();
            MyGameFacade facade = MyGameFacade.Instance;
            facade.startup(this);
        }

        /// <summary>
        /// Инициализируем новый загрузчик PMVC
        /// </summary>
        protected override void resumeBootstrap()
        {
            base.resumeBootstrap();
            MyGameFacade facade = MyGameFacade.Instance;
            facade.resume();
        }
    }
}