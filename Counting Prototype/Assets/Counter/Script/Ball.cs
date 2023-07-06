using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private PlayerControl playerControl;

    private Rigidbody rbBall;
    // Start is called before the first frame update
    void Start()
    {
        rbBall = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rbBall.velocity.magnitude > 5f)
        {
            rbBall.velocity = rbBall.velocity.normalized * 5f;
        }
        if(transform.position.z < -1.5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.5f);
        }
        if(transform.position.z > 1.9)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 1.5f);
        }
    }
    IEnumerator DestroyAfterASecond()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(DestroyAfterASecond());
        }
        if(collision.gameObject.CompareTag("left box boundary"))
        {
            transform.position = playerControl.pos - new Vector3(0, 0, 1.6f);
        }
        if(collision.gameObject.CompareTag("right box boundary"))
        {
            transform.position = playerControl.pos + new Vector3(0, 0, 1.6f);
        }
    }
}
