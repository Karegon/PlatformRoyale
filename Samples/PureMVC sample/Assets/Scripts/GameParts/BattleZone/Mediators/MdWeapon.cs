using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Unity;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Управляем оружием персонажа. Кладется на контейнер с оружием
    /// </summary>
    class MdWeapon : MediatorBehavior
    {

        public new const string NAME = "MdWeapon";

        private PrBattleZone _prBattleZone;
        //private GameObject bulletPrefab;
        private WeaponManager manager;

        public MdWeapon() : base(NAME)
        {
        } 

        public override void OnRegister()
        {
            base.OnRegister();
            _prBattleZone = Facade.RetrieveProxy(PrBattleZone.NAME) as PrBattleZone;
            if (_prBattleZone != null)
            {
                manager = _prBattleZone.hero.weaponManager;
            }
            Debug.Log(NAME + " OnRegister");
        }

        public override void OnRemove()
        {
            base.OnRemove();
            _prBattleZone = null;
            Debug.Log(NAME + " OnRemove");
        }

        /// <summary>
        /// Включает нужное оружие
        /// </summary>
        private void changeWeapon(KeyCode code)
        {
            Weapon curWeapon = null;

            // установим нужное оружие
            foreach (var weapon in manager.weapons)
            {
                if (weapon.keyCode == code)
                    curWeapon = manager.setWeapon(weapon);
            }

            // сменим внеший вид у оружия
            if (curWeapon != null)
            {
                foreach (Transform child in transform)
                {
                    if (child.name == curWeapon.objName)
                    {
                        child.gameObject.SetActive(true);
                        curWeapon.instance = child.gameObject;
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                if (curWeapon.bullet.instance == null)
                    curWeapon.bullet.instance = Resources.Load(curWeapon.bullet.prefabName) as GameObject;

                if (!curWeapon.bullet.instance) throw new Exception("Prefab is not found " + curWeapon.bullet.prefabName);
                manager.reload();
                SendNotification(BzMessages.WEAPON_WAS_CHANGED);

            }
        }

        private void createBullet()
        {
            Weapon curWeapon = manager.curWeapon; 

            if (curWeapon != null && curWeapon.bullet.instance != null)
            {
                GameObject someBullet =
                    UnityEngine.Object.Instantiate(curWeapon.bullet.instance, curWeapon.instance.transform.position,
                        curWeapon.instance.transform.localRotation);

                BulletBehavior bb = someBullet.GetComponent<BulletBehavior>();
                if (bb)
                {
                    bb.bullet = curWeapon.bullet;
                    Rigidbody2D rb = someBullet.GetComponent<Rigidbody2D>();
                    if (rb) rb.AddForce(_prBattleZone.hero.getDirection() * curWeapon.force);
                    _prBattleZone.hero.weaponManager.reload();
                    //audio.PlayOneShot(shootClip);
                }
            }
        
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(BzMessages.WEAPON_CHANGE);
            notes.Add(BzMessages.WEAPON_FIRE);
            notes.Add(BzMessages.WEAPON_MISFIRE);
            notes.Add(BzMessages.WEAPON_CREATE_BULLET);

            return notes;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case BzMessages.WEAPON_CHANGE:
                    Debug.Log("WEAPON_CHANGE");
                    changeWeapon((KeyCode) (int) note.Body);

                    break;

                case BzMessages.WEAPON_FIRE:
                    Debug.Log("WEAPON_FIRE");
                    _prBattleZone.hero.weaponManager.tryFire();
                    break;

                case BzMessages.WEAPON_CREATE_BULLET:
                    Debug.Log("WEAPON_CREATE_BULLET");
                    createBullet();
                    break;
                case BzMessages.WEAPON_MISFIRE:
                    // произошла осечка (оружие перезаряжается)
                    Debug.Log("WEAPON_MISFIRE");
                    break;


            }
        }

    }
}
