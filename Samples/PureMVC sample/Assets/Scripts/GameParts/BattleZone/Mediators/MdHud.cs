using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using UnityEngine;
using PureMVC.Unity;
using UnityEngine.UI;

namespace SampleGameNamespace
{
    /// <summary>
    /// Медиатор обслуживает наэкранный индикатор (HUD)
    /// </summary>
    public class MdHud : MediatorBehavior
    {

        public new const string NAME = "MdHud";

        private const string REPLACE_TAG = "%VAL%";
        private const string AMMO_TEXT = "AMMO:" + REPLACE_TAG;
        private const string HEALTH_TEXT = "HEALTH:" + REPLACE_TAG;

        private PrBattleZone _prBattleZone;
        private Text healthText;
        private Text ammoText;

        public MdHud() : base(NAME)
        {
            Debug.Log(NAME + " constructor");
        }

        public override void OnRegister()
        {
            base.OnRegister();
            _prBattleZone = Facade.RetrieveProxy(PrBattleZone.NAME) as PrBattleZone;
            /*
            Transform tr = this.transform.Find("HealthText");
            if (tr)
                healthText = tr.GetComponent<Text>();
            tr = this.transform.Find("AmmoText");
            if (tr)
                ammoText = tr.GetComponent<Text>();
            */
            GameObject go = Tools.FindObjectByName("HealthText");
            if (go)
                healthText = go.GetComponent<Text>();
            go = Tools.FindObjectByName("AmmoText");
            if (go)
                ammoText = go.GetComponent<Text>();


            Debug.Log(NAME + " OnRegister");
        }

        public override void OnRemove()
        {
            base.OnRemove();
            _prBattleZone = null;
            Debug.Log(NAME + " OnRemove");
        }

        private void updateAmmoIndicator()
        {
            if (ammoText) ammoText.text = AMMO_TEXT.Replace(REPLACE_TAG,
                _prBattleZone.hero.weaponManager.curWeapon.bulletQty.ToString());

        }

        private void updateHealthIndicator()
        {
            if (healthText) healthText.text = HEALTH_TEXT.Replace(REPLACE_TAG,
                Mathf.Round(_prBattleZone.hero.health).ToString()); ;
        }


        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(BzMessages.WEAPON_CREATE_BULLET);
            notes.Add(BzMessages.HEALTH_CHANGED);
            notes.Add(BzMessages.WEAPON_WAS_CHANGED);
            return notes;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case BzMessages.WEAPON_WAS_CHANGED:
                    updateAmmoIndicator();
                    break;
                case BzMessages.WEAPON_CREATE_BULLET:
                    updateAmmoIndicator();
                    break;
                case BzMessages.HEALTH_CHANGED:
                    updateHealthIndicator();
                    break;
            }
        }
    }
}
