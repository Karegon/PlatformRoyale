
using UnityEngine;

namespace SampleGameNamespace
{
    /*
     * Набор вспомогательных методов
     */ 
    class Tools
    {
        /// <summary>
        /// Вернет тру, если в игре установленапауза 
        /// </summary>
        /// <returns></returns>
        public static bool isPause()
        {
            return Time.timeScale == 0;
        }

        public static GameObject FindObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].name == name)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        public static GameObject FindObjectByTag(string tag)
        {

            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].CompareTag(tag))
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        public static GameObject FindObjectByLayer(int layer)
        {

            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].hideFlags == HideFlags.None)
                {
                    if (objs[i].gameObject.layer == layer)
                    {
                        return objs[i].gameObject;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Создает на сцене объект из префаба и вернет ссылку на него
        /// </summary>
        /// <param name="resourceName">имя ресурса (префаба). Префаб должен располагаться в папке \resources\.. </param>
        /// <returns>ссылка на инстанциированный объект</returns>
        public static GameObject instantiateObject(string resourceName, Transform parent = null)
        {
            var res = UnityEngine.Resources.Load(resourceName, typeof(GameObject)) as GameObject;
            res = Object.Instantiate(res);
            if (parent)
                res.transform.SetParent(parent, false);
            return res;
        }
    }


}
