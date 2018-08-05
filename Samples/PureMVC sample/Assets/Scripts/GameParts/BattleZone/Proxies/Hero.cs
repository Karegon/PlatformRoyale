using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Направление аватара героя
    /// </summary>
    enum AvararDir
    {
        adLeft = 1,
        adRight
    }

    /// <summary>
    /// Состояние героя
    /// </summary>
    enum AvararState
    {
        asUnknown,
        asInit,
        asIdle,
        asDead,
        asDeleted,
        asWalk,
        asReborn,
        asFire,
        asHit
    }

    /// <summary>
    /// Герой игры
    /// </summary>
    class Hero
    {
        public const int MIN_HEALTH = 0;
        public const int MAX_HEALTH = 100;
        // ссылка на аватар героя
        public GameObject avatar;
        // маркер для проверки расположения земли
        private Transform groundCheck;
        // кэшируем коллайдер чекера
        private Collider2D groundCheckCollider;
        // кэшируем твердое тело аватара
        private Rigidbody2D body;


        // в каком слое должен быть аватар
        public string curLayer = "Player";
        // требуется ли подбросить аватар
        public bool needJump = false;
        // направление туловища аватара
        public AvararDir direction = AvararDir.adLeft;    
        // сила для движения по сторонам
        public float moveForce = 15f;
        // максимальная скорость героя
        public float maxSpeed = 5f;
        // сила прыжка
        public float jumpForce = 450f;
        // персонаж находится на земле
        public bool isGrounded = false;
        // значение здоровья
        public float health = 50f;

        public Vector2 curForce = Vector2.zero;
        public Vector2 curVelocity = Vector2.zero;

        public WeaponManager weaponManager = new WeaponManager();

        private readonly HashSet<AvararState> m_state = new HashSet<AvararState>();
        public HashSet<AvararState> state
        {
            get
            {
                return m_state;
            }
        }

        //private Animator anim;				  // Reference to the player's animator component.
        //public Rigidbody2D bullet;              // Prefab of the bullet
        //public Rigidbody2D bomb;                // Prefab of the bomb
        // скорость пули
        //public float bulletSpeed = 20f;

        public void init(GameObject avatar)
        {
            this.avatar = avatar;
            state.Add(AvararState.asInit);
            body = avatar.GetComponent<Rigidbody2D>();
            groundCheck = avatar.transform.Find("groundCheck");
            groundCheckCollider = groundCheck.GetComponent<Collider2D>();
            weaponManager.init();
            state.Remove(AvararState.asInit);
        }

        /// <summary>
        /// нанести повреждения. Обрати внимание, что демедж должен быть отрицательным. В противном
        /// случае герой увеличит здоровье.
        /// </summary>
        /// <param name="value"></param>
        public void doDamage(float value)
        {
            health += value;
            if (health > MAX_HEALTH) health = MAX_HEALTH;
            if (health < MIN_HEALTH) health = MIN_HEALTH;
            MyGameFacade.Instance.SendNotification(BzMessages.HEALTH_CHANGED, health);
            if (health < 0) MyGameFacade.Instance.SendNotification(BzMessages.AVATAR_DEAD, this);
        }

        /// <summary>
        /// Осуществляем управление аватаром
        /// Этот метод должен вызываться из Update
        /// </summary>
        public void calcaulate()
        {
            if (!avatar) return;
            weaponManager.update();

            // Получим битовую маску слоя Ground
            // и посмотрим, не пересекаемся ли мы специальным маркером groundCheck, входящим в состав героя,
            // с со слоем Ground (не стоим ли на земле)
            //int mask = 1 << LayerMask.NameToLayer("Ground");
            //isGrounded = Physics2D.Linecast(go.transform.position, groundCheck.position, mask);

            // советуют сделать по-другому.
            isGrounded = groundCheckCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
            //Debug.Log(isGrounded);

            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                needJump = true;
            }

            /*
            if (Input.GetButtonDown("Fire1"))
            {
                float angle;
                float speed;
                if (direction == HeroBodyDirection.hdRight)
                {
                    angle = 0f;
                    speed = bulletSpeed;
                }
                else
                {
                    angle = 180f;
                    speed = -bulletSpeed;
                }
                // ... instantiate the rocket facing right and set it's velocity to the right. 
                Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle))) as Rigidbody2D;
                bulletInstance.velocity = new Vector2(speed, 0);

            }

            if (Input.GetButtonDown("Fire2"))
            {
                Rigidbody2D bombInstance = Instantiate(bomb, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
            }
            */
        }

        /// <summary>
        /// Расчитываем поведение персонажа в физическом мире. 
        /// Этот метод должен вызываться из FixedUpdate
        /// </summary>
        public void calculatePhysics()
        {
            // берем данные по горизонтальной оси
            float h = Input.GetAxis("Horizontal");

            // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
            // ... add a force to the player.
            if (h != 0 && h * body.velocity.x < maxSpeed)
            {
                curForce = Vector2.right * h * moveForce;
                body.AddForce(curForce, ForceMode2D.Force);
                //Debug.Log(curForce);
                Facade.Instance.SendNotification(BzMessages.AVATAR_MOVE, curForce);
            }

            // If the player's horizontal velocity is greater than the maxSpeed...
            // ... set the player's velocity to the maxSpeed in the x axis.
            
            if (Mathf.Abs(body.velocity.x) > maxSpeed)
            {
                curVelocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
                body.velocity = curVelocity;
            }

            // если двигаемся вправо, а аватар смотрит влево или же если двигаемся влево, 
            // а аватар смотрит вправо, то нужно развернуть аватар
            if ((h > 0 && direction == AvararDir.adLeft) || 
                (h < 0 && direction == AvararDir.adRight))
                doFlip();

            // needJump устанавливается в Update
            if (needJump)
            {
                // добавим вертикальной силы для прыжка
                body.AddForce(new Vector2(0f, jumpForce));
                Facade.Instance.SendNotification(BzMessages.AVATAR_JUMP);
                needJump = false;
            }

            Facade.Instance.SendNotification(BzMessages.AVATAR_ANIM_CHANGE);
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            //anim.SetFloat("Speed", Mathf.Abs(h));
        }

        /// <summary>
        /// Разворачивает аватар игрока в противоположную сторону
        /// </summary>
        public void doFlip()
        {
            direction = direction != AvararDir.adLeft ? AvararDir.adLeft : AvararDir.adRight;
            MyGameFacade.Instance.SendNotification(BzMessages.AVATAR_FLIP);
        }

        public Vector2 getDirection()
        {
            return direction == AvararDir.adLeft ? Vector2.left : Vector2.right;
        }


    }
}
