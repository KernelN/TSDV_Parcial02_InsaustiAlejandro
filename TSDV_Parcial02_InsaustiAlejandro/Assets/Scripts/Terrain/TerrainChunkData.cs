using System.Collections.Generic;
using UnityEngine;

public class TerrainChunkData : MonoBehaviour
{
    #region Variables
    public bool onLeftSide;
    public TerrainChunkScriptableObject SO;
    [SerializeField] int chunkIndex;
    #endregion

    #region Unity Events
    private void Start()
    {
        SO = GetChunkSO();
        GetComponent<SpriteRenderer>().sprite = GetChunkSprite();
        UpdateColliderPhysicsShape();
    }
    #endregion

    #region Methods
    TerrainChunkScriptableObject GetChunkSO()
    {
        return (TerrainChunkScriptableObject)Resources.Load("ScriptableObjects/Terrain/Chunk" + chunkIndex);
    }
    Sprite GetChunkSprite()
    {
        return (Sprite)Resources.Load("Sprites/TerrainChunk" + chunkIndex);
    }
    void UpdateColliderPhysicsShape()
    {
        //Get components
        PolygonCollider2D polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        //reset number of points in path
        polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        //Generate new path (the path of points wich form the physics shape)
        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }
    }
    #endregion
}