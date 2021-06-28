using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Chunk Coordinates")]
public class TerrainChunkScriptableObject : ScriptableObject
{
    [SerializeField] Vector2 leftEnd;
    [SerializeField] Vector2 rightEnd;
}