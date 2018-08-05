using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Специальная команда, которая вызывается для запуска дефолтной сцены 
    /// </summary>
    public class CmdShowDefaultScene : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Debug.Log("execute CmdShowDefaultScene");
            // вызываем стандартную 
            SendNotification(MenuMessages.NOTE_STATE_SWITCH, null, MenuMessages.STATE_MAIN_MENU);
        }
    }
}
