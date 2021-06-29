using System.Collections.Generic;
using UnityEngine;

public class TerrainChunkData : MonoBehaviour
{
    #region Variables
    public int chunkIndex;
    public bool onLeftSide;
    public TerrainChunkScriptableObject SO { get; private set; }
    #endregion

    #region Unity Events
    #endregion

    #region Methods
    public void GenerateChunkData()
    {
        SO = GetChunkSO();
        GetComponent<SpriteRenderer>().sprite = GetChunkSprite();
        UpdateColliderPhysicsShape();
    }
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