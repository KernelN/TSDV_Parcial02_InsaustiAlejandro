using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [SerializeField] float followRadius;
    [SerializeField] float zoomInRadius;
    [SerializeField] GameObject zoomedOutCamera;
    [SerializeField] GameObject zoomedInCamera;
    [SerializeField] Transform player;
    [SerializeField] GameplayManager gameManager;
    Transform currentCamera;
    bool zoomIsOn = false;
    LayerMask mapLayer;
    float maxXPos;
    #endregion

    #region Unity Events
    private void Start()
    {
        maxXPos = gameManager.mapLimit.x + 5 - zoomInRadius;
        mapLayer = LayerMask.GetMask("Map");
        currentCamera = zoomedOutCamera.transform;
        player.GetComponent<ShipController>().OnAltitudeChange += AdjustZoom;
    }
    private void Update()
    {
        FollowPlayer();
    }
    #endregion

    #region Methods
    void FollowPlayer()
    {
        if (Mathf.Abs(player.position.y - currentCamera.position.y) > followRadius)
        {
            currentCamera.position = new Vector3(currentCamera.position.x, GetNewYAxis(), currentCamera.position.z);
        }
        if (!zoomIsOn) { return; }
        if (Mathf.Abs(player.position.x - currentCamera.position.x) < followRadius) { return; }
        currentCamera.position = new Vector3(GetNewXAxis(), currentCamera.position.y, currentCamera.position.z);
    }
    float GetNewXAxis()
    {

        if (player.position.x > currentCamera.position.x + followRadius) //move to the right
        {
            //Prevent camera to getting too close to the edge
            if (maxXPos - player.position.x < zoomInRadius)
            {
                return maxXPos - zoomInRadius;
            }

            return player.position.x - followRadius;
        }
        else if (player.position.x < currentCamera.position.x - followRadius) //move to the left
        {
            //Prevent camera to getting too close to the edge
            if (-maxXPos - player.position.x < zoomInRadius)
            {
                return -maxXPos + zoomInRadius;
            }

            return player.position.x + followRadius;
        }

        return currentCamera.position.x;
    }
    float GetNewYAxis()
    {
        if (player.position.y > currentCamera.position.y + followRadius)
        {
            return player.position.y - followRadius;
        }
        else if (player.position.y < currentCamera.position.y - followRadius)
        {
            return player.position.y + followRadius;
        }

        return currentCamera.position.y;
    }
    void AdjustZoom(float playerDistanceToRock)
    {
        RaycastHit2D hit = Physics2D.CircleCast(player.localPosition, zoomInRadius, -player.up, 0, mapLayer);
        if (zoomIsOn && hit.collider == null)
        {
            zoomIsOn = false;
            zoomedInCamera.SetActive(false);
            zoomedOutCamera.SetActive(true);
            currentCamera = zoomedOutCamera.transform;
        }
        else if (!zoomIsOn && hit.collider != null)
        {
            zoomIsOn = true;
            zoomedInCamera.gameObject.SetActive(true);
            zoomedOutCamera.gameObject.SetActive(false);
            currentCamera = zoomedInCamera.transform;
        }
    }
    #endregion
}