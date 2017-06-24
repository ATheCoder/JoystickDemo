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
    private float lookTo = -1;

	// Use this for initialization
	void Start () {
        rb2D = this.GetComponent<Rigidbody2D>();
        hpValue.text = health.ToString();
        transform.localScale = new Vector3(-1, 1, 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2D.AddForce(new Vector2(0, CrossPlatformInputManager.GetAxis("Vertical") * 4));
        rb2D.AddForce(new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * 4 , 0));
        //Rotation Scripts:
        if(CrossPlatformInputManager.GetAxis("Horizontal") != 0 || CrossPlatformInputManager.GetAxis("Vertical") != 0) {
            if(CrossPlatformInputManager.GetAxis("Horizontal") > 0) {
                lookTo = 1;
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(CrossPlatformInputManager.GetAxis("Horizontal") < 0) {
                lookTo = -1;
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else {
                lookTo = -1;
            }
            lookPos = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            Debug.Log(Mathf.Atan(lookPos.y / lookPos.x) * Mathf.Rad2Deg);
            Vector3 lik = new Vector3(-1, 1, 1);
            rb2D.MoveRotation(Mathf.Atan(lookPos.y / lookPos.x) * Mathf.Rad2Deg * (transform.localScale == lik ? -1 : 1));

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