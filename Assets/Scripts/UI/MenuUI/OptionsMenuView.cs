using UnityEngine;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class OptionsMenuView : MenuView
    {
        [SerializeField] private Slider _brightness;
        [SerializeField] private Button _backButton;

        public override void Init()
        {
            _brightness.onValueChanged.AddListener(delegate { ChangeBrightness();});
            _backButton.onClick.AddListener(() => MenuViewManager.MenuViewManagerInstance.ShowPrevious());
        }

        private void ChangeBrightness()
        {
            Screen.brightness = _brightness.value;
            print(Screen.brightness + " and " + _brightness.value);
        }
    }
}
