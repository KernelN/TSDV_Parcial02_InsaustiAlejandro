using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    #region Variables
    public UnityAction<float> OnAltitudeChange;
    [SerializeField] float shipTooCloseToRockDistance;
    [SerializeField] float rocketPower;
    [SerializeField] float rotationSpeed;
    [SerializeField] float gravity;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject rocketPropulsionEffect;
    LayerMask mapLayer;
    Vector2 rocketForce;
    Vector3 rotation;
    float altitude;
    #endregion

    #region Unity Methods
    private void Start()
    {
        mapLayer = LayerMask.GetMask("Map");
        rocketForce = new Vector2(0,0);
        rotation = new Vector3(0,0,0);
        rocketPropulsionEffect.SetActive(false);
    }
    private void Update()
    {
        UpdateGravity();
        CheckAltitude();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            AddRocketForce();   
        }
        else
        {
            SetRocketPropulsion(false);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            Rotate();
        }
    }
    #endregion

    #region Methods
    void UpdateGravity()
    {
        rb.gravityScale = gravity;
    }
    void SetRocketPropulsion(bool turnOn)
    {
        rocketPropulsionEffect.SetActive(turnOn);
    }
    void AddRocketForce()
    {
        rocketForce.y = rocketPower;
        rb.AddRelativeForce(rocketForce);
        SetRocketPropulsion(true);
    }
    void Rotate()
    {
        //positive rotation uses right arrow and rotates to the left
        //so rotation needs to be inverted to turn in the desired direction
        rotation.z = -Input.GetAxis("Horizontal");
        transform.Rotate(rotation);
    }
    void CheckAltitude()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, -transform.up, 100, mapLayer);
        if (hit.collider == null) { return; }
        if (altitude == hit.distance) { return; }
        
        altitude = hit.distance;
        OnAltitudeChange?.Invoke(altitude);
    }
    #endregion
}