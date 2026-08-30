using UnityEngine;
using Game.UI;

namespace Game.Static
{
    public class Singleton : MonoBehaviour
    {
        public bool IsPause = false;
        [field: SerializeField] public UI_Dialogue Dialogue { get; private set; }
        public static Singleton Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }
    }
}