using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerInteractable.Constructions
{
    public class EngineeringShip : BasicShip
    {
        [SerializeField] private StarBase _starBaseObject;

        public void BuildStarBaseInRadius(Transform starTransform)
        {
            Vector3 starPosition = starTransform.position;
            float starSpawnRadius = 5f;
            
            float randomPointInRadiusX = (Random.insideUnitCircle.normalized * starSpawnRadius).x;
            float randomPointInRadiusZ = (Random.insideUnitCircle.normalized * starSpawnRadius).y;

            Vector3 randomPositionInRadius = new Vector3(starPosition.x + randomPointInRadiusX, 0,
                starPosition.z + randomPointInRadiusZ);
            
            Instantiate(_starBaseObject, randomPositionInRadius, Quaternion.identity);
            print("Star base built from engi!");
        }
    }
}