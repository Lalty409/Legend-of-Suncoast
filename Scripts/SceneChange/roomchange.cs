using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomchange : MonoBehaviour
{
    
    public Vector2 newMin;
    public Vector2 newMax;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            cam.minpos = newMin;
            cam.maxpos = newMax;
            other.transform.position += playerChange;
            
            if(needText) {
                StartCoroutine(placeNameCo());
            }
        }
    }


    private IEnumerator placeNameCo() { 
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(3F);
        text.SetActive(false);
    }

}
