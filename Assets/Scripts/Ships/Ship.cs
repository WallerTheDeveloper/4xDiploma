using UnityEngine;
using Game.Attributes;
using Movement;

namespace Ships
{
    public class Ship: Mover, IFlyable
    {
        // private ShipData _data;
        // private Transform _transform;
        // private int _currentHealth;
        //
        // public Ship(ShipData data)
        // {
        //     _data = data;
        //     _currentHealth = data.HP;
        //     
        //     GameObject gameObject = GameObject.Instantiate(
        //         Resources.Load($"Prefabs/Ships/{_data.ShipPersonalCode}")
        //         ) as GameObject;
        //
        //     _transform = gameObject.transform;
        // }
        //
        // public void SetPosition(Vector3 position)
        // {
        //     _transform.position = position;
        // }
        //
        //
        // public Transform Transform => _transform;
        //
        // public int HP
        // {
        //     get => _currentHealth;
        //     set => _currentHealth = value;
        // }
        //
        // public int MaxHp => _data.HP;
        private ShipData _data;
        private Transform _transform;
        private int _currentHealth;

        public void Fly()
        {
            StartCoroutine(SmoothRotate());
            StartCoroutine(CalculateMovement());
        }
        
    }   
}
