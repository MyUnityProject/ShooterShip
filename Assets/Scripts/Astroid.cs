using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour {

    [SerializeField]
    List<Sprite> sprites;

    [SerializeField]
    float Force = 10.0f;

    [SerializeField]
    float Degrees = 0f;

    int lives = 2;


    float collRadius;
    // Use this for initialization
    void Start ()
    {
        //Set Random Sprite
        Sprite currentSprite = sprites[Random.Range(0, 3)];
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = currentSprite;


        //Set Radius
        collRadius = GetComponent<CircleCollider2D>().radius;


    }

    void OnBecameInvisible()
    {
       
        //Wrap around code when the ship goes out of camera view
        Vector2 rockPosition = transform.position;
        if ((rockPosition.y - collRadius > ScreenUtils.ScreenTop) || (rockPosition.y + collRadius < ScreenUtils.ScreenBottom))
        {            
            rockPosition.y = -rockPosition.y;  
        }
        if ((rockPosition.x - collRadius > ScreenUtils.ScreenRight) || (rockPosition.x + collRadius < ScreenUtils.ScreenLeft))
        {
            rockPosition.x = -rockPosition.x;
        }
            transform.position = rockPosition;

    }

    public void Initialize(float initialDirection,int life)
    {
        //Shoot it in random direction
        float direction = Random.Range(-30, 30) + initialDirection;
        float angleRadian = direction * Mathf.Deg2Rad;
        
        float xDir = Mathf.Cos(angleRadian);
        float yDir = Mathf.Sin(angleRadian);
        Vector2 Rockdirection = new Vector2(xDir, yDir);
        //Add force to rock
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        print(Rockdirection);
        rb.AddForce(Force * Rockdirection, ForceMode2D.Impulse);
        lives = life;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Bullet")
        {
            GameObject subAsteroid = Instantiate(gameObject);
            subAsteroid.GetComponent<Astroid>().InitializeOnBulletHit(lives-1);

            subAsteroid = Instantiate(gameObject);
            subAsteroid.GetComponent<Astroid>().InitializeOnBulletHit(lives - 1);

            Destroy(col.gameObject);
            Destroy(gameObject);
            
        }
    }

    void InitializeOnBulletHit(int life)
    {
        lives = life;
        if (lives < 0)
            Destroy(gameObject);

        AudioManager.Play(AudioClipName.AsteroidHit);
        //Make asteroid smaller by half
        Vector3 scale = transform.localScale;
        scale = scale / 2;
        transform.localScale = scale;
        //Calculate random direction
        float direction = Random.Range(0, 330);
        Initialize(direction, lives);



    }
}
