using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KidProperties : MonoBehaviour {


    // Use this for initialization
    Vector2 y;
    Vector2 x;
    List<Vector2> force = new List<Vector2>();
    void Start () {
        Vector2 force1 = transform.right * 20;
        Vector2 force2 = -transform.right * 20;
        Vector2 force3 = transform.up * 20;
        force.Add(force1);
        force.Add(force2);
        force.Add(force3);
        this.GetComponent<Rigidbody2D>().drag = Random.Range(0, 5);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, Random.Range(-360, 360) * Time.deltaTime / 2);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Gorilla")
        {
            other.GetComponent<TouchToMove>().enabled = false;
            other.tag = "DeadGorilla";
            other.GetComponent<Rigidbody2D>().velocity = 1 * Vector2.up;
        }

        if (other.tag == "HumanGorilla")

        {
            
            this.GetComponent<Rigidbody2D>().AddForce(force[Random.Range(0,force.Count)]);
            this.GetComponent<Rigidbody2D>().drag = 0;

        }
    }
}
