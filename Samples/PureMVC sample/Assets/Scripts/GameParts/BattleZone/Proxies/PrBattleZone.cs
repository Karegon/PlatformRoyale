using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using UnityEngine;

namespace SampleGameNamespace
{

    public enum ZoneState
    {
        zsUnknown = 0,
        zsInit,
        zsInProgress,
        zsPause,
        zsEnd
    }

    /// <summary>
    /// Управляет моделью игровой зоны
    /// </summary>
    class PrBattleZone : Proxy
    {
        public new const string NAME = "PrBattleZone";

        // состояние игровой зоны (см. BzMessages.BATTLE_ZONE_STATE)
        private ZoneState _state = ZoneState.zsUnknown;
        public ZoneState state {
            get { return _state; }
            set { if (_state != value) changeState(value); }
        }

        public Hero hero;

        private void changeState(ZoneState value)
        {
            _state = value;
            SendNotification(BzMessages.STATE_WAS_CHANGED, _state);
        }

        public PrBattleZone() : base (NAME, null)
        {
            Debug.Log(NAME + " started");
            hero = new Hero();
        }

        public ZoneState messageToState(string note)
        {
            ZoneState res;
            switch (note)
            {
                // идет инициализация
                case BzMessages.STATE_INIT:
                    res = ZoneState.zsInit;
                    break;
                // идет игра
                case BzMessages.STATE_IN_PROGRESS:
                    res = ZoneState.zsInProgress;
                    break;
                // игра остановлена
                case BzMessages.STATE_PAUSE:
                    res = ZoneState.zsPause;
                    break;
                // игра окончена
                case BzMessages.STATE_END:
                    res = ZoneState.zsEnd;
                    break;
                default:
                    res = ZoneState.zsUnknown;
                    break;
            }
            return res;
        }

        public void setStateByMessage(string message)
        {
            state = messageToState(message);
        }
    }
}
