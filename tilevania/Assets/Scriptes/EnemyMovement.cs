using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 1f;
    Rigidbody2D MyRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingRight())
        {
            MyRigidbody.velocity = new Vector2(MovementSpeed, 0);
    
        }else
        {
            MyRigidbody.velocity = new Vector2(-MovementSpeed, 0);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(MyRigidbody.velocity.x)), 1f);
    }
    bool IsFacingRight()
    {
        return transform.lossyScale.x > 0;
    }
}
