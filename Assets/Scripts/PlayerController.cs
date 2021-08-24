using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 0.01f;
    public GameObject Bullet;
    public Transform weaponPoint1;
    public Transform weaponPoint2;

    private Rigidbody2D playerRigidBody;
    private Coroutine firingRoutine = null;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
        HandlePlayerRotation();
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandlePlayerMovement()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed);
        }
    }

    private void HandlePlayerRotation()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firingRoutine = StartCoroutine(Fire());
        } else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(firingRoutine);
        }
    }

    IEnumerator Fire()
    {
        while(true)
        {
            Instantiate(Bullet, weaponPoint1.position, weaponPoint1.rotation);
            Instantiate(Bullet, weaponPoint2.position, weaponPoint2.rotation);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
