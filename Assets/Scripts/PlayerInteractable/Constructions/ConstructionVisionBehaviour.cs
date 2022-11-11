using Core;
using UnityEngine;

public class ConstructionVisionBehaviour : MonoBehaviour
{
    [SerializeField] private float _visionRadius = 10f;

    private Collider[] _planetsInsideSphere;
    private void FixedUpdate()
    {
        if (Physics.CheckSphere(transform.position, _visionRadius))
        {
            _planetsInsideSphere = Physics.OverlapSphere(transform.position, _visionRadius);
            EnableSpaceObjectsMeshRenderer(_planetsInsideSphere);
        }
    }

    private void EnableSpaceObjectsMeshRenderer(Collider[] planetCollider)
    {
        foreach (var collider in planetCollider)
        {
            var childMesh = collider.gameObject.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;
            if (childMesh != null)
            {
                childMesh.enabled = true;
                collider.gameObject.tag = Globals.activeObjectTag;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
    }
}
