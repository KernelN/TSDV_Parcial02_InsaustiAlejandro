using UnityEngine;

struct Chunk
{
    public Transform transform;
    public TerrainChunkData data;
}
public class TerrainChunkManager : MonoBehaviour
{
    #region Variables
    [SerializeField] int chunksToGenerate;
    [SerializeField] int typesOfChunk;
    [SerializeField] Chunk[] chunks = new Chunk[2];
    [SerializeField] GameObject chunkPrefab;
    #endregion

    #region Unity Events
    private void Start()
    {
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i] = GenerateChunk();
        }
    }
    #endregion

    #region Methods
    Chunk GenerateChunk()
    {
        //generate chunk Game Object
        GameObject go = Instantiate(chunkPrefab);
        go.GetComponent<TerrainChunkData>().chunkIndex = Random.Range(1, typesOfChunk + 1);

        //Get chunk data
        Chunk chunk = new Chunk();
        chunk.transform = go.transform;
        chunk.data = go.GetComponent<TerrainChunkData>();
        return chunk;
    }
    #endregion
}