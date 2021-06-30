using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    #region Variables
    public UnityAction<float> OnFuelChange;
    public UnityAction<float> OnAltitudeChange;
    public UnityAction<float> OnVerticalSpeedChange;
    public UnityAction<float> OnHorizontalSpeedChange;
    public UnityAction OnPlayerLand;
    public UnityAction OnPlayerDeath;
    public float maxFuel { get; private set; }
    public float fuel { get; private set; }
    public float rocketPower { get { return data.rocketPower; } }
    public float rotationSpeed { get { return data.rotationSpeed; } }
    public float gravity { get { return data.gravity; } }
    public float fuelUsePerSecond { get { return data.fuelUsePerSecond; } }
    [SerializeField] GameObject rocketPropulsionEffect;
    [SerializeField] float minAltitudeForCollisionDetect;
    [SerializeField] float collisionDetectionRadius;
    ShipData data;
    Rigidbody2D rb;
    LayerMask mapLayer;
    Vector2 rocketForce;
    Vector3 rotation;
    float vSpeed;
    float hSpeed;
    #endregion

    #region Unity Methods
    private void Start()
    {
        data = GetComponent<ShipData>();
        maxFuel = data.maxFuel;
        fuel = maxFuel;
        rb = GetComponent<Rigidbody2D>();
        mapLayer = LayerMask.GetMask("Map");
        rocketForce = new Vector2(0, 0);
        rotation = new Vector3(0, 0, 0);
        rocketPropulsionEffect.SetActive(false);
    }
    private void Update()
    {
        UpdateGravity();
        CheckVSpeed();
        CheckHSpeed();
        CheckAltitude();
        if (altitude < transform.localScale.y * minAltitudeForCollisionDetect)
        {
            CheckCollision();
        }

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
        if (rb.gravityScale == gravity) { return; }
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

        fuel -= fuelUsePerSecond * Time.deltaTime;
        OnFuelChange.Invoke(fuel);
    }
    void Rotate()
    {
        //positive rotation uses right arrow and rotates to the left
        //so rotation needs to be inverted to turn in the desired direction
        rotation.z = -Input.GetAxis("Horizontal");
        transform.Rotate(rotation);
    }

    void CheckVSpeed()
    {
        if (vSpeed != rb.velocity.y)
        {
            vSpeed = rb.velocity.y;
            OnVerticalSpeedChange.Invoke(vSpeed);
        }
    }

    void CheckHSpeed()
    {
        if (hSpeed != rb.velocity.x)
        {
            hSpeed = rb.velocity.x;
            OnHorizontalSpeedChange.Invoke(hSpeed);
        }
    }

    float altitude;
    void CheckAltitude()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, -Vector3.up, 100, mapLayer);
        if (hit.collider == null) { return; }
        if (altitude == hit.distance) { return; }

        altitude = hit.distance;
        OnAltitudeChange?.Invoke(altitude);
    }
    void CheckCollision()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.localPosition, collisionDetectionRadius, -Vector3.up, 0, mapLayer);
        if (hit.collider == null) { return; }

        //player dies if touches land at high speed or at the wrong angle
        if (Mathf.Abs(vSpeed) + Mathf.Abs(hSpeed) > rocketPower * 0.75f || (transform.eulerAngles.z > 5 && transform.eulerAngles.z < 355))
        {
            OnPlayerDeath?.Invoke();
        }
        else //if player is still and touching land, land
        {
            OnPlayerLand?.Invoke();
        }
    }
    #endregion
}