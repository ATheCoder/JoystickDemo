using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Script : MonoBehaviour {
    Rigidbody2D rb2D;
    float health = 100f;
	// Use this for initialization
	void Start () {
        rb2D = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2D.AddForce(new Vector2(0, CrossPlatformInputManager.GetAxis("Accelator") * 20));
        rb2D.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")));
        if(CrossPlatformInputManager.GetAxis("Horizontal") > 0) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "Solid") {
            health -= 5;
        }
        if(health < 1) {
            Die();
        }
    }
    void Die() {
        Debug.Log("I'm Dead!");
    }
}
