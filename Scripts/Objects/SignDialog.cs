using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDialog : Interactable {

    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;

	
	void Update () {
        if(Input.GetKeyDown(KeyCode.E) && playerInRange) {
            if(dialogBox.activeInHierarchy) {
                dialogBox.SetActive(false);
            }
            else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
	}

    public override void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            context.Raise();
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

}