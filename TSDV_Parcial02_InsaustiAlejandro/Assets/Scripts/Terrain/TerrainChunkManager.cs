using UnityEngine;


public class TerrainChunkManager : MonoBehaviour
{
    [System.Serializable]
    struct Chunk
    {
        public Transform transform;
        public TerrainChunkData data;
    }

    #region Variables
    [SerializeField] int differentTypesOfChunk;
    [SerializeField] Chunk[] chunks;
    [SerializeField] GameObject chunkPrefab;
    const int chunkLength = 40;
    #endregion

    #region Unity Events
    private void Start()
    {
        for (int i = 0; i < chunks.Length; i++)
        {
            //generate chunk
            chunks[i] = GenerateChunk();

            //accomadate one chunk to the left, and one to the right
            if (i > 0)
            {
                if (i % 2 == 0)
                {
                    AccomodateChunkToTheLeft(i, i - 1);
                }
                else
                {
                    AccomodateChunkToTheRight(i, i - 1);
                }
            }
        }

    }
    #endregion

    #region Methods
    Chunk GenerateChunk()
    {
        //generate chunk Game Object
        GameObject go = Instantiate(chunkPrefab);
        TerrainChunkData chunkData = go.GetComponent<TerrainChunkData>();
        chunkData.chunkIndex = Random.Range(1, differentTypesOfChunk + 1);
        chunkData.GenerateChunkData();

        //Get chunk data
        Chunk chunk = new Chunk();
        chunk.transform = go.transform;
        chunk.data = chunkData;
        return chunk;
    }
    void AccomodateChunkToTheLeft(int leftIndex, int rightIndex)
    {
        //get needed data
        Transform leftTransform = chunks[leftIndex].transform;
        Transform rightTransform = chunks[rightIndex].transform;
        float leftLinkY = chunks[leftIndex].data.SO.rightEndY;
        float rightLinkY = chunks[rightIndex].data.SO.leftEndY;

        //calculate positions
        float xPosition = rightTransform.localPosition.x - chunkLength;
        float yPosition =
            rightTransform.localPosition.y +
            (rightLinkY * rightTransform.localScale.y) -
            (leftLinkY * leftTransform.localScale.y);

        //set positions
        leftTransform.localPosition = new Vector3(xPosition, yPosition, 0);
    }
    void AccomodateChunkToTheRight(int leftIndex, int rightIndex)
    {
        //get needed data
        Transform leftTransform = chunks[leftIndex].transform;
        Transform rightTransform = chunks[rightIndex].transform;
        float leftLinkY = chunks[leftIndex].data.SO.rightEndY;
        float rightLinkY = chunks[rightIndex].data.SO.leftEndY;

        //calculate positions
        float xPosition = leftTransform.localPosition.x + chunkLength;
        float yPosition =
            leftTransform.localPosition.y +
            (leftLinkY * leftTransform.localScale.y) -
            (rightLinkY * rightTransform.localScale.y);

        //set positions
        rightTransform.localPosition = new Vector3(xPosition, yPosition, 0);
    }
    #endregion
}