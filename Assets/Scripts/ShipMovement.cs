using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to move the ship in 2 ways :  
/// 1) Forward Thrust using the spacebar
/// 2) Motion left and right using the mouse
/// </summary>
public class ShipMovement : MonoBehaviour {

    [SerializeField]
    float shipForce = 10.0f;

    [SerializeField]
    float shipRotationSpeed = 10.0f;

    [SerializeField]
    GameObject bullet;

    Vector2 shipDirection;
    Rigidbody2D rb;
    float collRadius;
    bool isFiring = false;

    //Variables for timer
    float timer;
    bool isAlive = true;

    [SerializeField]
    Text text;
	// Use this for initialization
	void Start () {
        
        //This gets the X and Y coordinates of ship Direction based on the angle.
        float shipzRotDeg = transform.eulerAngles.z;
        float shipzRotRad = Mathf.Deg2Rad * shipzRotDeg;
        float shipX = Mathf.Cos(shipzRotRad);
        float shipY = Mathf.Sin(shipzRotRad);
        shipDirection = new Vector2(shipX, shipY);

        //Get the rigid body attached to the object and the radius of the rigid body.
        rb = GetComponent<Rigidbody2D>();
        collRadius = GetComponent<CircleCollider2D>().radius;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Rotate") != 0)
        {
            //Update ship direction based on left or right click
            Vector3 shipAngle = (transform.eulerAngles);
            shipAngle.z += Input.GetAxis("Rotate") * shipRotationSpeed * Time.deltaTime;
            transform.eulerAngles = shipAngle;
        }
        if (isFiring == false)
        {
            if (Input.GetAxis("Fire1") != 0)
            {
                GameObject currBull = Instantiate(bullet, transform.position, transform.rotation);
                AudioManager.Play(AudioClipName.PlayerShot);
                isFiring = true;
            }
        }

        if (Input.GetAxis("Fire1") == 0)
            isFiring = false;

        if (isAlive)
        {
            timer = timer + Time.deltaTime;
            text.text = "Time : " + ((int)timer).ToString();
        }
	}

    void FixedUpdate()
    {
        //On space bar, thrust in direction ship is facing
        if (Input.GetAxis("Thrust") > 0)
        {
            float shipzRotDeg = transform.eulerAngles.z;
            float shipzRotRad = Mathf.Deg2Rad * shipzRotDeg;
            float shipX = Mathf.Cos(shipzRotRad);
            float shipY = Mathf.Sin(shipzRotRad);
            shipDirection = new Vector2(shipX, shipY);
            rb.AddForce(shipForce * shipDirection, ForceMode2D.Force);
        }
    }

    void OnBecameInvisible()
    {
        //Wrap around code when the ship goes out of camera view
        Vector2 shipPosition = transform.position;
        if ((shipPosition.y - collRadius > ScreenUtils.ScreenTop) || (shipPosition.y + collRadius < ScreenUtils.ScreenBottom))
            shipPosition.y = -shipPosition.y;
        if ((shipPosition.x - collRadius > ScreenUtils.ScreenRight) || (shipPosition.x + collRadius < ScreenUtils.ScreenLeft))
            shipPosition.x = -shipPosition.x;
        transform.position = shipPosition;

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if(col.gameObject.tag == "Asteroid")
        {
            AudioManager.Play(AudioClipName.PlayerDeath);      
            Destroy(gameObject);
        }
    }

}
