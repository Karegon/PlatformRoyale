using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SampleGameNamespace
{
    /// <summary>
    /// Класс для хранения данных об оружии
    /// </summary>
    public class Weapon
    {
        // публичное название оружия
        public string caption;
        // сила выстрела
        public float force;
        // время перезарядки
        public float rechargeTime;
        // патрон для выстрела
        public Bullet bullet;
        // название го на сцене
        public string objName;
        // название ресурса со звукуом выстрела
        public string soundShootName;
        // Кнопка для включения оружия
        public KeyCode keyCode;
        // Количество патронов
        public int bulletQty;

        // кэш инстанции оружия
        public GameObject instance;

    }
}
