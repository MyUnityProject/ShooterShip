using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField]
    float bulletSpeed = 10.0f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        float directionRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        float bulletX = Mathf.Cos(directionRad);
        float bulletY = Mathf.Sin(directionRad);
        Vector2 direction = new Vector2(bulletX, bulletY);
        rb.AddForce(bulletSpeed*direction, ForceMode2D.Impulse);
        Destroy(gameObject, 2);
	}

    void OnBecameInvisible()
    {
        Vector2 bulletPosition = transform.position;
        if ((bulletPosition.y > ScreenUtils.ScreenTop) || (bulletPosition.y  < ScreenUtils.ScreenBottom))
            bulletPosition.y = -bulletPosition.y;
        if ((bulletPosition.x  > ScreenUtils.ScreenRight) || (bulletPosition.x  < ScreenUtils.ScreenLeft))
            bulletPosition.x = -bulletPosition.x;
        transform.position = bulletPosition;

    }
}
