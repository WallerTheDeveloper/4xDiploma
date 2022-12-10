using UnityEngine;

namespace Core.Map
{
    public class Map : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowMap()
        {
            gameObject.SetActive(true);
        }

        public void HideMap()
        {
            gameObject.SetActive(false);
        }
    }
}
