using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avilena : Antonio
{

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    public override void CheckDistance() {
        anim.SetBool("WakeUp", true);
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius 
                && Vector3.Distance(target.position, transform.position) > attackRadius) {
            
            if((currentState == EnemyState.idle || currentState == EnemyState.walk) && currentState != EnemyState.stagger) {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed*Time.deltaTime);

                ChangeAnim(temp - transform.position);
                rigb.MovePosition(temp); 

                anim.SetBool("WakeUp", true);
            }
        }
        else if(Vector3.Distance(target.position, transform.position) > chaseRadius) {
            if(Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance) {
            Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed*Time.deltaTime);
            ChangeAnim(temp - transform.position);
            rigb.MovePosition(temp);   
            }  
            else {
                ChangeGoal();
            }
        }              
    }

    private void ChangeGoal() {
        Debug.Log("yo");
        if(currentPoint == path.Length - 1) {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }

}
