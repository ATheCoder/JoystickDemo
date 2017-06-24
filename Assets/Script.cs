using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Script : MonoBehaviour {
    Rigidbody2D rb2D;
    public Text hpValue;
    float health = 100f;

    public float DamageMultiplier = 4f;
    public float DamageTolerance = 4f;

    private Vector2 lookPos;

	// Use this for initialization
	void Start () {
        rb2D = this.GetComponent<Rigidbody2D>();
        hpValue.text = health.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2D.AddForce(new Vector2(0, CrossPlatformInputManager.GetAxis("Vertical") * 10));
        rb2D.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * 10 , 0));
        if(CrossPlatformInputManager.GetAxis("Horizontal") > 0) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(CrossPlatformInputManager.GetAxis("Horizontal") < 0) {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        //Rotation Scripts:
        if(CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0) {
            lookPos = new Vector2(transform.position.x + CrossPlatformInputManager.GetAxis("Horizontal"), transform.position.y + CrossPlatformInputManager.GetAxis("Vertical"));
            Vector2 Pos = new Vector2(transform.position.x, transform.position.y);
            gameObject.transform.right = Pos - lookPos;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(rb2D.velocity.magnitude * rb2D.mass);
        if(rb2D.velocity.magnitude * rb2D.mass > DamageTolerance) {
            health -= Mathf.Ceil(rb2D.velocity.magnitude * rb2D.mass * DamageMultiplier);
            hpValue.text = health.ToString();
        }
    }
    void Die() {
        Debug.Log("I'm Dead!");
    }
}
