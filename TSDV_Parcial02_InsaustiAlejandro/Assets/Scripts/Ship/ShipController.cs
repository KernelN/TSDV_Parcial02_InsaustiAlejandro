using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
    #region Variables
    public UnityAction<float> OnFuelChange;
    public UnityAction<float> OnAltitudeChange;
    public UnityAction<float> OnVerticalSpeedChange;
    public UnityAction<float> OnHorizontalSpeedChange;
    public float maxFuel { get; private set; }
    public float fuel { get; private set; }
    public float rocketPower { get { return data.rocketPower; } }
    public float rotationSpeed { get { return data.rotationSpeed; } }
    public float gravity { get { return data.gravity; } }
    public float fuelUsePerSecond { get { return data.fuelUsePerSecond; } }
    [SerializeField] GameObject rocketPropulsionEffect;
    ShipData data;
    Rigidbody2D rb;
    LayerMask mapLayer;
    Vector2 rocketForce;
    Vector3 rotation;
    float altitude;
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
        rocketForce = new Vector2(0,0);
        rotation = new Vector3(0,0,0);
        rocketPropulsionEffect.SetActive(false);
    }
    private void Update()
    {
        UpdateGravity();
        CheckVSpeed();
        CheckHSpeed();
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