using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Класс описывает доступное вооружение игрока 
    /// </summary>
    public class WeaponManager
    {
        protected SimpleTimer _timer = new SimpleTimer();
        protected bool _isFire = true;

        public List<Weapon> weapons = new List<Weapon>();

        private int _curWeaponIndex = -1;

        public Weapon curWeapon
        {
            get
            {
                Weapon res;
                if (_curWeaponIndex != -1)
                    res = weapons[_curWeaponIndex];
                else
                    res = null;

                return res;
            }
        }

        public void init()
        {
            Weapon wp;
            wp = new Weapon();
            wp.caption = "Gun1";
            wp.objName = "Weapon1";
            wp.force = 1800;
            wp.rechargeTime = 0.2f;
            wp.soundShootName = "";
            wp.keyCode = KeyCode.Alpha1;
            wp.bulletQty = 100;
            wp.bullet = new Bullet();
            wp.bullet.prefabName = "prefabs/components/bullets/bullet1";
            wp.bullet.livetime = 10f;
            wp.bullet.baseDamage = 10f;
            wp.bullet.currentDamage = 10f;
            wp.bullet.explodeDamage = 50f;
            wp.bullet.explodeForce = 3f;
            wp.bullet.explodeRadius = 10f;
            wp.bullet.isBounces = false;
            weapons.Add(wp);

            wp = new Weapon();
            wp.caption = "Gun2";
            wp.objName = "Weapon2";
            wp.force = 1800;
            wp.rechargeTime = 0.5f;
            wp.soundShootName = "";
            wp.keyCode = KeyCode.Alpha2;
            wp.bulletQty = 50;
            wp.bullet = new Bullet();
            wp.bullet.prefabName = "prefabs/components/bullets/bullet2";
            wp.bullet.livetime = 10f;
            wp.bullet.baseDamage = 10f;
            wp.bullet.currentDamage = 10f;
            wp.bullet.explodeDamage = 50f;
            wp.bullet.explodeForce = 3f;
            wp.bullet.explodeRadius = 10f;
            wp.bullet.isBounces = true;
            weapons.Add(wp);

            wp = new Weapon();
            wp.caption = "Gun3";
            wp.objName = "Weapon3";
            wp.force = 1800;
            wp.rechargeTime = 1f;
            wp.soundShootName = "";
            wp.keyCode = KeyCode.Alpha3;
            wp.bulletQty = 10;
            wp.bullet = new Bullet();
            wp.bullet.prefabName = "prefabs/components/bullets/bullet3";
            wp.bullet.livetime = 10f;
            wp.bullet.baseDamage = 10f;
            wp.bullet.currentDamage = 10f;
            wp.bullet.explodeDamage = 50f;
            wp.bullet.explodeForce = 3f;
            wp.bullet.explodeRadius = 10f;
            wp.bullet.isBounces = true;
            weapons.Add(wp);
        }

        public Weapon nextWeapon()
        {
            _curWeaponIndex++;
            if (_curWeaponIndex >= weapons.Count - 1) _curWeaponIndex = 0;
            return curWeapon;
        }

        public Weapon prevWeapon()
        {
            _curWeaponIndex--;
            if (_curWeaponIndex < 0) _curWeaponIndex = weapons.Count - 1;
            return curWeapon;
        }

        public Weapon setWeaponIndex(int value)
        {
            if (value >= 0 && value < weapons.Count) _curWeaponIndex = value;
            return curWeapon;
        }

        public Weapon setWeapon(Weapon aWeapon)
        {
            var index = weapons.IndexOf(aWeapon);
            setWeaponIndex(index);
            return curWeapon;
        }

        public void update()
        {
            _timer.Update();
            if (_timer.IsEvent())
            {
                _isFire = true;
            }
        }

        public void reload()
        {
            _isFire = false;
            _timer.Start(curWeapon.rechargeTime);
        }

        public void tryFire()
        {
            if (_isFire && curWeapon != null)
            {
                if (curWeapon.bulletQty > 0)
                {
                    curWeapon.bulletQty--;
                    MyGameFacade.Instance.SendNotification(BzMessages.WEAPON_CREATE_BULLET);
                }
                
            }
            else
            {
                MyGameFacade.Instance.SendNotification(BzMessages.WEAPON_MISFIRE);
            }
        }
    }
}
