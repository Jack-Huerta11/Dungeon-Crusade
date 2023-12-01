using UnityEngine;

public class ConvertToSkinnedMesh : MonoBehaviour
{
    public Transform playerHips; // Reference to the Hips bone of the player
     [ContextMenu("Convert to skinned mesh")]
 public void Convert()
    {
        // Get the MeshFilter and MeshRenderer from the current GameObject
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Check if both components exist
        if (meshFilter != null && meshRenderer != null)
        {
            // Create a new GameObject with a SkinnedMeshRenderer
            GameObject skinnedMeshObject = new GameObject("SkinnedMeshObject");
            SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshObject.AddComponent<SkinnedMeshRenderer>();

            // Copy the mesh from the MeshFilter
            skinnedMeshRenderer.sharedMesh = meshFilter.sharedMesh;

            // Copy the materials from the MeshRenderer
            Material[] materials = meshRenderer.sharedMaterials;
            skinnedMeshRenderer.materials = materials;

            // Set the root bone to the Hips bone of the player
            skinnedMeshRenderer.rootBone = playerHips;

            // Set the bones to the same bones as the player (you may need to adjust this based on your character setup)
            Transform[] playerBones = playerHips.GetComponentsInChildren<Transform>();
            skinnedMeshRenderer.bones = playerBones;

            // Set default weight for all bones to 1
            float[] boneWeights = new float[playerBones.Length];
            for (int i = 0; i < boneWeights.Length; i++)
            {
                boneWeights[i] = 1f;
            }
            skinnedMeshRenderer.sharedMesh.boneWeights = CreateBoneWeights(boneWeights);

            // Optional: Destroy the original MeshFilter and MeshRenderer components
            Destroy(meshFilter);
            Destroy(meshRenderer);
        }
        else
        {
            Debug.LogError("MeshFilter or MeshRenderer not found on the GameObject.");
        }
    }

    private BoneWeight[] CreateBoneWeights(float[] weights)
    {
        BoneWeight[] boneWeights = new BoneWeight[weights.Length];
        for (int i = 0; i < boneWeights.Length; i++)
        {
            boneWeights[i].weight0 = weights[i];
        }
        return boneWeights;
    }
}
