using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Unity;
using UnityEngine;

namespace SampleGameNamespace
{
    class MdHero : MediatorBehavior
    {
        Rigidbody2D body;

        public new const string NAME = "MdHero";

        private PrBattleZone _prBattleZone;
        private Hero hero;


        public MdHero() : base(NAME)
        {
            Debug.Log(NAME + " constructor");
        }

        public override void OnRegister()
        {
            base.OnRegister();
            _prBattleZone = Facade.RetrieveProxy(PrBattleZone.NAME) as PrBattleZone;
            hero = _prBattleZone.hero;
            hero.init(this.gameObject);
            body = GetComponent<Rigidbody2D>();
            Debug.Log(NAME + " OnRegister");
        }

        public override void OnRemove()
        {
            base.OnRemove();
            _prBattleZone = null;
            Debug.Log(NAME + " OnRemove");
        }

        private void FixedUpdate()
        {
           if (hero != null) hero.calculatePhysics();
        }

        private void Update()
        {
            if (hero != null) hero.calcaulate();
        }

        void flip()
        {
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                hero.doDamage(-10);
                var bounceDirection = hero.getDirection();
                bounceDirection.x *= -1 * 500f;
                bounceDirection.y = 250f;
                body.AddForce(bounceDirection, ForceMode2D.Force );
                Debug.Log(bounceDirection);
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Pickup"))
            {
                GameObject.Destroy(collision.gameObject);
                hero.doDamage(50);
                Debug.Log("Pickup health");
            }
        }





        public override IList<string> ListNotificationInterests()
        {
            IList<string> notes = new System.Collections.Generic.List<string>();
            notes.Add(BzMessages.AVATAR_FLIP);
            notes.Add(BzMessages.AVATAR_JUMP);
            notes.Add(BzMessages.AVATAR_MOVE);
            notes.Add(BzMessages.AVATAR_ANIM_CHANGE);
            notes.Add(BzMessages.AVATAR_DEAD);
            return notes;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case BzMessages.AVATAR_ANIM_CHANGE:
                    break;
                case BzMessages.AVATAR_FLIP:
                    flip();
                    break;
                case BzMessages.AVATAR_JUMP:
                    // Включим необходимую анимацию
                    //anim.SetTrigger("jump");
                    // проиграем звук прыжка
                    //AudioSource.PlayClipAtPoint(clipIndex, transform.position);
                    Debug.Log("jump");
                    break;
                case BzMessages.AVATAR_MOVE:
                    // Включим необходимую анимацию
                    //anim.SetTrigger("move");
                    //Debug.Log("move");
                    break;
                case BzMessages.AVATAR_DEAD:
                    Debug.Log("dead");
                    break;
            }
        }


    }

}
