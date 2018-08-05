using System;
using SampleGameNamespace;
using PureMVC.Patterns;
using PureMVC.Unity;
using UnityEngine;


namespace SampleGameNamespace
{
    /// <summary>
    /// Медиатор с поведением надо весить на игровой объект
    /// при этом создавать его не надо, так как его создаст Unity. Достаточно зарегистрировать,
    /// предварительно получив ссылку на инстанцию:  UnityEngine.Object.FindObjectOfType<MdInput>();
    /// Удаляется такой медиатор из системы сообщенй стандартным способом: Facade.RemoveMediator(MdInput.NAME);
    /// </summary>
    public class MdPlayerInput : MediatorBehavior {

        public new const string NAME = "MdPlayerInput";

        private PrMenuScene m_prMenuScene;


        public MdPlayerInput() : base(NAME)
        {
            Debug.Log(NAME + " constructor");
        }

        public override void OnRegister()
        {
            base.OnRegister();
            m_prMenuScene = Facade.RetrieveProxy(PrMenuScene.NAME) as PrMenuScene;
            Debug.Log(NAME + " OnRegister");
        }

        public override void OnRemove()
        {
            base.OnRemove();
            m_prMenuScene = null;
            Debug.Log(NAME + " OnRemove");
        }


        // Update is called once per frame
        void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SendNotification(BzMessages.WEAPON_CHANGE, KeyCode.Alpha1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SendNotification(BzMessages.WEAPON_CHANGE, KeyCode.Alpha2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SendNotification(BzMessages.WEAPON_CHANGE, KeyCode.Alpha3);
            }

            if (Input.GetMouseButton(0))
            {
                SendNotification(BzMessages.WEAPON_FIRE);
            }

            /*
            if (_prScene1.curState == MyMessages.STATE_GAME)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    SendNotification(MyMessages.NOTE_FL_SWITCH);
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                {
                    var direction = Input.GetAxis("Mouse ScrollWheel") < 0 ? -1 : 1;
                    SendNotification(MyMessages.NOTE_WEAPON_SCROLL, direction);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    SendNotification(MyMessages.NOTE_WEAPON_SWITCH,  MyMessages.WEAPON_PISTOL);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    SendNotification(MyMessages.NOTE_WEAPON_SWITCH, MyMessages.WEAPON_BOMB);
                }
                if (Input.GetMouseButton(0))
                {
                    SendNotification(MyMessages.NOTE_WEAPON_FIRE);
                }
            }
            */
        }
    }
}
