using UnityEngine;

public class ShipController : MonoBehaviour
{
    #region Variables
    [SerializeField] float rocketPower;
    [SerializeField] float rotationSpeed;
    [SerializeField] float gravity;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject rocketPropulsionEffect;
    Vector2 rocketForce;
    Vector3 rotation;
    #endregion

    #region Unity Methods
    private void Start()
    {
        rocketForce = new Vector2(0,0);
        rotation = new Vector3(0,0,0);
        rocketPropulsionEffect.SetActive(false);
    }
    private void Update()
    {
        UpdateGravity();
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
    #endregion
}