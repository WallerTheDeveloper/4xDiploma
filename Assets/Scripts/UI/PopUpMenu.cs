using UnityEngine;

namespace UI
{
    public class PopUpMenu : MonoBehaviour
    {
        // private CinemachineVirtualCamera _camera;
        private void Start()
        {
            ActivatePopUpMenu(false);
        }
        // private void Update()
        // {
        //     transform.LookAt(-_camera.transform.position);
        // }
        public void ActivatePopUpMenu(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}