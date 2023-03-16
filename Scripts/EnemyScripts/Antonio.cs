using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antonio : Enemy
{   
    public Rigidbody2D rigb;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;


    void Start() {
        currentState = EnemyState.idle;
        rigb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        target = GameObject.FindWithTag("Player").transform;

        anim.SetBool("WakeUp", true);
    }


    void FixedUpdate() {
        CheckDistance();
    }


    public virtual void CheckDistance() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius 
                && Vector3.Distance(target.position, transform.position) > attackRadius) {
            
            if((currentState == EnemyState.idle || currentState == EnemyState.walk) 
                    && currentState != EnemyState.stagger) {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);

                ChangeAnim(temp - transform.position);
                rigb.MovePosition(temp); 

                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
       }
       else if(Vector3.Distance(target.position, transform.position) > chaseRadius)
        anim.SetBool("WakeUp", false);
    }

    //moves enemy
    private void SetAnimFloat(Vector2 setVector) {
        anim.SetFloat("MoveX", setVector.x);
        anim.SetFloat("MoveY", setVector.y);
    }

    public void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("MoveX", direction.x);
        anim.SetFloat("MoveY", direction.y);
    }
        

    private void ChangeState(EnemyState newState) {
        if(currentState != newState)
        currentState = newState;
    }

}
