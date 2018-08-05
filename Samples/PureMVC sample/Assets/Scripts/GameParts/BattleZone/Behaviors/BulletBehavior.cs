using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Unity;
using UnityEngine;

namespace SampleGameNamespace
{
    enum BulletState
    {
        bsUnknown = 0,
        bsCreate,
        bsFire,
        bsFly,
        bsHit,
        bsDestroy,
        bsCleanup
    }

    /// <summary>
    /// Визуализирует полет пули
    /// </summary>
    class BulletBehavior : CustomBehavior
    {

        private PrBattleZone _prBattleZone;

        public Bullet bullet;
        protected ParticleSystem ps;
        protected GameObject explode;
        protected GameObject explodeTemplate;
        protected AudioSource audio;
        protected AudioClip shotSound;
        protected AudioClip flySound;
        protected AudioClip explodeSound;

        protected BulletState m_state;
        public BulletState state
        {
            get { return m_state; }
            set { setState(value); }
        }

        private void setState(BulletState aState)
        {
            if (m_state != aState)
            {
                m_state = aState;
                Debug.Log("new state:" + m_state);
                switch (m_state)
                {
                    case BulletState.bsCreate:
                        doCreate();
                        break;
                    case BulletState.bsFire:
                        doFire();
                        break;
                    case BulletState.bsFly:
                        doFly();
                        break;
                    case BulletState.bsHit:
                        doHit();
                        break;
                    case BulletState.bsDestroy:
                        doDestroy();
                        break;
                    case BulletState.bsCleanup:
                        doCleanup();
                        break;
                    default: throw new Exception("Invalid bullet state " + m_state);
                }
            }
        }

        private void LateUpdate()
        {
            if (state == BulletState.bsHit)
            {

                if (ps && !ps.IsAlive())
                //if (ps.IsAlive() && ps.isPlaying && ps.time != 0 )
                {
                    state = BulletState.bsDestroy;
                }
            }
        }

        protected void Start()
        {
            state = BulletState.bsCreate;
        }

        protected void doCreate()
        {
            StartCoroutine(AutoDestroy(bullet.livetime));
            bullet.currentDamage = bullet.baseDamage;
            audio = GetComponent<AudioSource>();
            state = BulletState.bsFire;
        }

        private IEnumerator AutoDestroy(float bulletLivetime)
        {
            yield return new WaitForSeconds(bulletLivetime);
            state = BulletState.bsHit;
        }

        protected void doFire()
        {
            if (shotSound) audio.PlayOneShot(shotSound);
            //Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            //if (rb) rb.AddForce(_barrel.forward * prScene1.curWeapon.force);
            state = BulletState.bsFly;
        }

        protected void doFly()
        {
            if (flySound)
            {
                audio.clip = flySound;
                audio.Play();
            }
        }

        protected void doHit()
        {
            if (bullet.explodeName != null)
            {
                explodeTemplate = Resources.Load(bullet.explodeName) as GameObject;
                explode = Instantiate(explodeTemplate, transform.position, transform.localRotation);
                ps = explode.GetComponent<ParticleSystem>();
                if (ps) ps.Play();
                IsVisible = false;
                Light l = gameObject.GetComponent<Light>();
                if (l) l.intensity = 0;

                explodeSound = (AudioClip)Resources.Load("sound/explode");
                audio.PlayOneShot(explodeSound);

                Debug.Log("boom!");
                if (bullet.explodeRadius != 0)
                {
                    Collider[] colliders = Physics.OverlapSphere(this.transform.position, bullet.explodeRadius);
                    foreach (Collider hit in colliders)
                    {
                        Vector3 direction = hit.transform.position - transform.position;
                        if (hit.attachedRigidbody != null)
                        {
                            Debug.Log(hit.attachedRigidbody.name);
                            hit.attachedRigidbody.AddForce(direction.normalized * bullet.explodeForce);
                        }
                        //(actor as IDestructable).updateHealth(new CollisionInfo(DEF_DMG_VALUE, Vector3.zero));
                    }
                }

            }
            else
            {
                state = BulletState.bsDestroy;
            }
        }

        protected void doDestroy()
        {
            if (ps) ps.Stop();
            state = BulletState.bsCleanup;
        }

        protected void doCleanup()
        {
            Debug.Log("cleanup");
            Destroy(explode);
            Destroy(gameObject);
            explodeTemplate = null;
            bullet = null;
            ps = null;
        }

        /// <summary>
        /// Проверка на стокновение с препятствием.
        /// Какие слои участвуют в столкновении можно настроить в редакторе Edit > Project Settings> Physics
        /// Пули у нас должны быть в слое bullet, который взаимодействует с Ground, Enemy и Pickup
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (state == BulletState.bsFly)
            {
                Debug.Log("check hit");
                //SetDamage(collision.gameObject.GetComponent<IDestructable>());
                if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (!bullet.isBounces) state = BulletState.bsHit;
                }
                else
                {
                    state = BulletState.bsHit;
                }
            }
        }

        /*
        private void SetDamage(IDestructable obj)
        {
            if (obj != null)
            {
                obj.updateHealth(new CollisionInfo(bullet.currentDamage, transform.forward));
            }
        }
        */

    }
}
