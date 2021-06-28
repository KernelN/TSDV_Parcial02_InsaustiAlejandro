using UnityEngine;

[CreateAssetMenu(fileName = "Terrain Chunk Coordinates")]
public class TerrainChunkScriptableObject : ScriptableObject
{
    [SerializeField] float leftEndY;
    [SerializeField] float rightEndY;
}