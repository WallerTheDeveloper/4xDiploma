using UnityEngine;

namespace UI.MenuUI
{
    public abstract class MenuView : MonoBehaviour
    {
        public abstract void Init();

        public void ShowView() => gameObject.SetActive(true);

        public void HideView() => gameObject.SetActive(false);

    }
}
