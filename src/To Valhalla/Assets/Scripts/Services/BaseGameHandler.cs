using UnityEngine;

namespace Services
{
    public abstract class BaseGameHandler<T> : MonoBehaviour where T : BaseGameHandler<T>
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = FindObjectOfType<T>();
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
