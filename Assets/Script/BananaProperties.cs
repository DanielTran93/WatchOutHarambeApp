using UnityEngine;
using System.Collections;

public class BananaProperties : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Random.Range(0, 360) * Time.deltaTime / 2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gorilla")
        {
            other.tag = "HumanGorilla";
            Destroy(this.gameObject);
        }


        //if (other.tag == "Human")

        //{

        //    this.GetComponent<Rigidbody2D>().AddForce(force[Random.Range(0, force.Count)]);
        //    this.GetComponent<Rigidbody2D>().drag = 0;

        //}
    }
}
