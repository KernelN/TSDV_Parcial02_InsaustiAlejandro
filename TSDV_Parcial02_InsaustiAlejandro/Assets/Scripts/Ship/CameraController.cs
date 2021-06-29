using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [SerializeField] float followRadius;
    [SerializeField] Transform player;
    #endregion

    #region Unity Events
    private void Update()
    {
        FollowPlayer();
    }
    #endregion

    #region Methods
    void FollowPlayer()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) > followRadius)
        {
            transform.position = new Vector3(GetNewXAxis(), transform.position.y, transform.position.z);
        }
        if (Mathf.Abs(player.position.y - transform.position.y) > followRadius)
        {
            transform.position = new Vector3(transform.position.x, GetNewYAxis(), transform.position.z);
        }
    }
    float GetNewXAxis()
    {
        if (player.position.x > transform.position.x + followRadius)
        {
            return player.position.x - followRadius;
        }
        else if (player.position.x < transform.position.x - followRadius)
        {
            return player.position.x + followRadius;
        }

        return transform.position.x;
    }
    float GetNewYAxis()
    {
        if (player.position.y > transform.position.y + followRadius)
        {
            return player.position.y - followRadius;
        }
        else if (player.position.y < transform.position.y - followRadius)
        {
            return player.position.y + followRadius;
        }

        return transform.position.y;
    }
    #endregion
}