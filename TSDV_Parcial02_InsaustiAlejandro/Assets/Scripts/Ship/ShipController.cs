using UnityEngine;
using UnityEngine.Events;

public class ShipController : MonoBehaviour
{
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
    [SerializeField] GameplayManager gameManager;
    [SerializeField] float minAltitudeForCollisionDetect;
    [SerializeField] float collisionDetectionRadius;
    ShipData data;
    Rigidbody2D rb;
    Collider2D shipCollider;
    LayerMask mapLayer;
    Vector2 rocketForce;
    Vector3 rotation;
    float vSpeed;
    float hSpeed;
    float altitude;
    bool gameOver;

    #region Unity Methods
    private void Start()
    {
        shipCollider = GetComponent<Collider2D>();
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
        if (gameOver) { return; }

        UpdateGravity();
        KeepShipOnMap();

        CheckVSpeed();
        CheckHSpeed();
        CheckAltitude();
        if (!shipCollider.enabled && altitude < transform.localScale.y * minAltitudeForCollisionDetect)
        {
            shipCollider.enabled = true;
        }
        else if (shipCollider.enabled && altitude > transform.localScale.y * minAltitudeForCollisionDetect)
        {
            shipCollider.enabled = false;
        }
    }
    private void FixedUpdate()
    {
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //player dies if touches land at high speed or at the wrong angle
        if (Mathf.Abs(vSpeed) + Mathf.Abs(hSpeed) > rocketPower * 0.75f || (transform.eulerAngles.z > 5 && transform.eulerAngles.z < 355))
        {
            OnPlayerDeath?.Invoke();
            return;
        }

        //if player is still and touching land
        Vector2 leftPos = new Vector2(transform.localPosition.x - transform.localScale.x / 3, transform.localPosition.y); ;
        Vector2 rightPos = new Vector2(transform.localPosition.x + transform.localScale.x / 3, transform.localPosition.y); ;
        float rayLength = transform.localScale.y * 2;
        RaycastHit2D hitLeft = Physics2D.Raycast(leftPos, Vector2.down, rayLength, mapLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(rightPos, Vector2.down, rayLength, mapLayer);
        //if both feet are on ground, land succesfully
        if (hitLeft.collider && hitRight.collider)
        {
            OnPlayerLand?.Invoke();
        }
        else
        {
            OnPlayerDeath?.Invoke();
        }
        gameOver = true;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, -Vector3.up, 100, mapLayer);
        if (hit.collider == null) { return; }
        if (altitude == hit.distance) { return; }

        altitude = hit.distance;
        OnAltitudeChange?.Invoke(altitude);
    }
    void KeepShipOnMap()
    {
        Vector2 mapLimit = gameManager.mapLimit;
        if (transform.position.y > mapLimit.y)
        {
            transform.Translate(Vector3.down * (transform.position.y - (mapLimit.y + 1)));
        }

        if (transform.position.x > mapLimit.x)
        {
            transform.Translate(Vector3.left * (transform.position.x - (mapLimit.x + 1)));
        }
        else if (transform.position.x < -mapLimit.x)
        {
            transform.Translate(Vector3.right * (-(mapLimit.x + 1) - transform.position.x));
        }
    }
    #endregion
}