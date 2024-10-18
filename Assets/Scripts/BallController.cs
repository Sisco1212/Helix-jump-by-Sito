using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool ignoreNextCollision;
    public Rigidbody rb;
    public float impulseForce = 5f;
    private Vector3 startPos;
    public int perfectPass;
    public bool isSuperSpeedActive;
    public int superSpeed = 10;
    
    void Awake()
    {
        startPos = transform.position;        
    }

    private void OnCollisionEnter(Collision collision) {
        if(ignoreNextCollision)
        return;

        if(isSuperSpeedActive) {
            if(!collision.transform.GetComponent<Goal>()) {
                Destroy(collision.transform.parent.gameObject, 0.3f);
                Debug.Log("Destroying platforms");
            }
        } else{

        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if(deathPart) {
            deathPart.HitDeathPart();
        }
        }
        
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
        ignoreNextCollision = true;
        Invoke("AllowCollision", .2f);

        perfectPass = 0;
        isSuperSpeedActive = false;

    }

    private void Update() {
        if(perfectPass >= 3 && !isSuperSpeedActive) {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
    }

    private void AllowCollision() {
        ignoreNextCollision = false;
    }

    public void ResetBall() {
        transform.position = startPos;
    }
 
 
}
