using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour {

	const float viewerMoveThresholdForChunkUpdate = 25f;
	const float sqrViewerMoveThresholdForChunkUpdate = viewerMoveThresholdForChunkUpdate * viewerMoveThresholdForChunkUpdate;


	public int colliderLODIndex;
	public LODInfo[] detailLevels;

	public MeshSettings meshSettings;
	public HeightMapSettings heightMapSettings;
	public TextureData textureSettings;

	 public Transform player;
	public Material mapMaterial;

	Vector2 playerPosition;
	Vector2 playerPositionOld;
	public GameObject[] objectsToSpawn;
    public int numberOfObjectsToSpawn = 10;
	float meshWorldSize;
	int chunksVisibleInViewDst;

	Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
	List<TerrainChunk> visibleTerrainChunks = new List<TerrainChunk>();

	void Start() {

		textureSettings.ApplyToMaterial (mapMaterial);
		textureSettings.UpdateMeshHeights (mapMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);

		float maxViewDst = detailLevels [detailLevels.Length - 1].visibleDstThreshold;
		meshWorldSize = meshSettings.meshWorldSize;
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / meshWorldSize);
		UpdateVisibleChunks ();
		 SpawnRandomObjects();
	}

	void Update() {
		playerPosition = new Vector2 (player.position.x, player.position.z);

		if (playerPosition != playerPositionOld) {
			foreach (TerrainChunk chunk in visibleTerrainChunks) {
				chunk.UpdateCollisionMesh ();
			}
		}

		if ((playerPositionOld - playerPosition).sqrMagnitude > sqrViewerMoveThresholdForChunkUpdate) {
			playerPositionOld = playerPosition;
			UpdateVisibleChunks ();
		}
	}
		
	void UpdateVisibleChunks() {
		HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2> ();
		for (int i = visibleTerrainChunks.Count-1; i >= 0; i--) {
			alreadyUpdatedChunkCoords.Add (visibleTerrainChunks [i].coord);
			visibleTerrainChunks [i].UpdateTerrainChunk ();
		}
			
		int currentChunkCoordX = Mathf.RoundToInt (playerPosition.x / meshWorldSize);
		int currentChunkCoordY = Mathf.RoundToInt (playerPosition.y / meshWorldSize);

		for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++) {
			for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
				if (!alreadyUpdatedChunkCoords.Contains (viewedChunkCoord)) {
					if (terrainChunkDictionary.ContainsKey (viewedChunkCoord)) {
						terrainChunkDictionary [viewedChunkCoord].UpdateTerrainChunk ();
					} else {
						TerrainChunk newChunk = new TerrainChunk (viewedChunkCoord,heightMapSettings,meshSettings, detailLevels, colliderLODIndex, transform, player, mapMaterial);
						terrainChunkDictionary.Add (viewedChunkCoord, newChunk);
						newChunk.onVisibilityChanged += OnTerrainChunkVisibilityChanged;
						newChunk.Load ();
					}
				}

			}
		}
	}

	void OnTerrainChunkVisibilityChanged(TerrainChunk chunk, bool isVisible) {
		if (isVisible) {
			visibleTerrainChunks.Add (chunk);
		} else {
			visibleTerrainChunks.Remove (chunk);
		}
	}


void SpawnRandomObjects() {
        for (int i = 0; i < numberOfObjectsToSpawn; i++) {
            Vector2 randomPoint = new Vector2(Random.Range(playerPosition.x - meshWorldSize, playerPosition.x + meshWorldSize),
                                               Random.Range(playerPosition.y - meshWorldSize, playerPosition.y + meshWorldSize));

            float terrainHeight = GetTerrainHeight(randomPoint);

            if (terrainHeight != 0f) {
                SpawnObjectOnTerrain(randomPoint, terrainHeight);
            }
        }
    }

    void SpawnObjectOnTerrain(Vector2 position, float terrainHeight) {
        GameObject randomObject = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Adjust the object's position to match the terrain height
        Vector3 spawnPosition = new Vector3(position.x, terrainHeight, position.y);

        // Instantiate the object at the adjusted position
        Instantiate(randomObject, spawnPosition, Quaternion.identity);
    }
	 float GetTerrainHeight(Vector2 position) {
        // Implement your terrain height calculation logic here
        // For simplicity, return 0 for now
        return 0f;
    }

}
[System.Serializable]
public struct LODInfo {
	[Range(0,MeshSettings.numSupportedLODs-1)]
	public int lod;
	public float visibleDstThreshold;


	public float sqrVisibleDstThreshold {
		get {
			return visibleDstThreshold * visibleDstThreshold;
		}
	}
}