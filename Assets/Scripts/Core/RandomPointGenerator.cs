using System.Collections;
using System.Collections.Generic;
using Core.Miscellaneous;
using UnityEngine;

namespace Core
{
    public class RandomPointGenerator : MonoBehaviour
    {
        public static Vector3 GenerateRandomPointAtMap()
        {
            return new Vector3(
                Random.Range(MapSettings.minPositionX, MapSettings.maxPositionX),
                0f, 
                Random.Range(MapSettings.minPositionZ, MapSettings.maxPositionZ)
            );
        }
    }
}