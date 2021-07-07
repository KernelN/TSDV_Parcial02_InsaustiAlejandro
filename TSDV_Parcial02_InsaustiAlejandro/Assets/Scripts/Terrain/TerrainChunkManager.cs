using UnityEngine;


public class TerrainChunkManager : MonoBehaviour
{
    [System.Serializable]
    struct Chunk
    {
        public Transform transform;
        public TerrainChunkData data;
    }
    
    [SerializeField] int differentTypesOfChunk;
    [SerializeField] Chunk[] chunks;
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunksEmpty;
    const int chunkLength = 40;

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
                    MoveChunkToTheLeft(i, i - 1);
                }
                else
                {
                    MoveChunkToTheRight(i, i - 1);
                }
            }
        }
    }
    #endregion

    #region Methods
    Chunk GenerateChunk()
    {
        //generate chunk Game Object
        GameObject go = Instantiate(chunkPrefab, chunksEmpty);
        TerrainChunkData chunkData = go.GetComponent<TerrainChunkData>();
        chunkData.chunkIndex = Random.Range(1, differentTypesOfChunk + 1);
        chunkData.GenerateChunkData();
        go.name = "Chunk Type " + chunkData.chunkIndex;

        //Get chunk data
        Chunk chunk = new Chunk();
        chunk.transform = go.transform;
        chunk.data = chunkData;
        return chunk;
    }
    void MoveChunkToTheLeft(int leftIndex, int rightIndex)
    {
        //get needed data
        Transform leftT = chunks[leftIndex].transform;
        Transform rightT = chunks[rightIndex].transform;
        float leftLinkY = chunks[leftIndex].data.SO.rightEndY * leftT.localScale.y;
        float rightLinkY = chunks[rightIndex].data.SO.leftEndY * rightT.localScale.y;

        //calculate positions
        float xPosition = rightT.localPosition.x - chunkLength;
        float yPosition = rightT.localPosition.y + (rightLinkY) - (leftLinkY );

        //set positions
        leftT.localPosition = new Vector3(xPosition, yPosition, 0);
        chunks[leftIndex].data.onLeftSide = true;
    }
    void MoveChunkToTheRight(int leftIndex, int rightIndex)
    {
        //get needed data
        Transform leftT = chunks[leftIndex].transform;
        Transform rightT = chunks[rightIndex].transform;
        float leftLinkY = chunks[leftIndex].data.SO.rightEndY * leftT.localScale.y;
        float rightLinkY = chunks[rightIndex].data.SO.leftEndY * rightT.localScale.y;

        //calculate positions
        float xPosition = leftT.localPosition.x + chunkLength;
        float yPosition = leftT.localPosition.y + (leftLinkY) - (rightLinkY);

        //set positions
        rightT.localPosition = new Vector3(xPosition, yPosition, 0);
        chunks[rightIndex].data.onLeftSide = false;
    }
   

    //WIP, DON'T USE---------------------
    void UpdatePlayerChunkPosition()
    {
        //bool chunkIsLinkedByTheRight = chunks[chunkUsedByplayer].data.onLeftSide;

        //if (chunkIsLinkedByTheRight && player.localPosition.x < 0)
        //{
        //    MoveChunkToTheRight(0, chunkUsedByplayer);
        //}
        //else if (!chunkIsLinkedByTheRight && player.localPosition.x > 0)
        //{
        //    MoveChunkToTheRight(chunkUsedByplayer, chunks.Length - 1);
        //}
    }
    void ChangePlayerParentChunk()
    {
        //if (player.localPosition.x > chunkLength / 2)
        //{
        //    //player.parent;
        //}
    }
    //-----------------------------------
    #endregion
}