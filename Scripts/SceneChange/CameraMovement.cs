using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxpos;
    public Vector2 minpos;

    void Start() {
        transform.position = new Vector3(target.position.x,target.position.x, transform.position.z);
    }

    void LateUpdate() {
        if(transform.position != target.position) {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, minpos.x, maxpos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minpos.y, maxpos.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
        
    }
}
