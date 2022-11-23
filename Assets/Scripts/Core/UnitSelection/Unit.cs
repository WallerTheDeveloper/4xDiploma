using UnityEngine;

namespace Core.UnitSelection
{
    public class Unit : MonoBehaviour
    {
        private void Start()
        {
            UnitSelections.Instance.unitsList.Add(this.gameObject);
        }

        private void OnDestroy()
        {
            UnitSelections.Instance.unitsList.Remove(this.gameObject);
        }
    }
}