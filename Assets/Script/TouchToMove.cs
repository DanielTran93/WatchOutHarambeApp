using UnityEngine;
using System.Collections;

public class TouchToMove : MonoBehaviour
{

    Vector2 myposition;
    public GameObject Gorilla;
    Animator animator;
    Vector2 previousPosition;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            
            transform.position = Vector2.MoveTowards(transform.position, myposition, Time.deltaTime * 18.0f);
            if (Input.touchCount > 0)
            {
            Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x, touch.position.y));
                myposition = touchPosition;

            if (myposition.y > previousPosition.y)
            {
                animator.SetInteger("State", 2);
            }
            else
                animator.SetInteger("State", 1);
            //Vector2 dir = touchPosition - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        animator.SetInteger("State", 0);

    }

        

}
