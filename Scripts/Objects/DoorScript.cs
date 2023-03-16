using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorScript : Interactable
{   

    public bool unlocked = false;
    public Inventory playerInventory;
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;


    private void Update()
    {
        if(playerInRange) {
            context.Raise();
            if(Input.GetKeyDown(KeyCode.E)) {
                //Does the player have a key?
                if(playerInventory.numberOfKeys >= 1) {
                    playerInventory.numberOfKeys--;
                    unlocked = true;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        playerInRange = true;
        if(unlocked == true) {
            if(other.CompareTag("Player") && !other.isTrigger) {
                playerStorage.initialValue = playerPosition;
                StartCoroutine(FadeCo());
            }
        }
    }

   //panel fade in scene
    private void Awake() {
        if(fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1); //destroys fade panel after 1 second
        }
    }

    public IEnumerator FadeCo() {
        if(fadeOutPanel != null) {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad); //loads scene
        while(!asyncOperation.isDone)
            yield return null;
    }
}


