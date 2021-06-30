using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    public float followRadius;
    [SerializeField] GameObject zoomedOutCamera;
    [SerializeField] GameObject zoomedInCamera;
    [SerializeField] Transform player;
    Transform currentCamera;
    bool zoomIsOn = false;
    LayerMask mapLayer;
    #endregion

    #region Unity Events
    private void Start()
    {
        mapLayer = LayerMask.GetMask("Map");
        currentCamera = zoomedOutCamera.transform;
        player.GetComponent<ShipController>().OnAltitudeChange += AdjustZoom;
    }
    private void Update()
    {
        FollowPlayer();
    }
    private void OnDisable()
    {
        player.GetComponent<ShipController>().OnAltitudeChange -= AdjustZoom;
    }
    #endregion

    #region Methods
    void FollowPlayer()
    {
        if (zoomIsOn && Mathf.Abs(player.position.x - currentCamera.position.x) > followRadius)
        {
            currentCamera.position = new Vector3(GetNewXAxis(), currentCamera.position.y, currentCamera.position.z);
        }
        if (Mathf.Abs(player.position.y - currentCamera.position.y) > followRadius)
        {
            currentCamera.position = new Vector3(currentCamera.position.x, GetNewYAxis(), currentCamera.position.z);
        }
    }
    float GetNewXAxis()
    {
        if (player.position.x > currentCamera.position.x + followRadius)
        {
            return player.position.x - followRadius;
        }
        else if (player.position.x < currentCamera.position.x - followRadius)
        {
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
        RaycastHit2D hit = Physics2D.CircleCast(player.localPosition, followRadius * 2, -player.up, 0, mapLayer);
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