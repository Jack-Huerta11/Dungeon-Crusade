using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaceController : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    void Start()
    {
        // Ensure that a NavMeshSurface component is assigned.
        if (navMeshSurface == null)
        {
            Debug.LogError("NavMeshSurface reference is not set. Please assign a NavMeshSurface component.");
            return;
        }

        // Build the NavMesh.
        BuildNavMesh();
    }

    void BuildNavMesh()
    {
        // Bake the NavMesh.
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh baked successfully.");
        }
        else
        {
            Debug.LogError("NavMeshSurface reference is not set. Cannot bake NavMesh.");
        }
    }
}
