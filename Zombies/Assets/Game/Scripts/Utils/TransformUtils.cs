using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Utils
{
    public static class TransformUtils
    {
        public static void DetatchFromParent(this Transform transform, bool worldPositionStays = true)
        {
            var parent = transform.parent;
            if (parent != null)
            {
                transform.SetParent(null, worldPositionStays);

                if (!parent.gameObject.activeInHierarchy)
                {
                    SceneManager.MoveGameObjectToScene(transform.gameObject, parent.gameObject.scene);
                }
            }
        }
    }
}
