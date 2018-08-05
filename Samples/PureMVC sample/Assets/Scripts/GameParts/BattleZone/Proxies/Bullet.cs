using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Описывает параметры пули
    /// </summary>
    public class Bullet
    {
        // время жизни пули
        public float livetime;
        // базовый урон
        public float baseDamage;
        // текущий урон
        public float currentDamage;
        // отскакивает ли от стен
        public bool isBounces;
        // название префаба-модели пули
        public string prefabName;
        // название префаба-модели взрыва
        public string explodeName;
        // радиус взрыва, в который попадают предметы 
        public float explodeRadius = 0f;
        // повреждения при взрыве 
        public float explodeDamage = 0f;
        // сила толчка при взрыве
        public float explodeForce = 0f;

        // кэш инстанции пули
        public GameObject instance;

    }
}
