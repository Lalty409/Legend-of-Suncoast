using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable {

    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public SignalSender raiseItem;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.E) && playerInRange) {
            if(!isOpen) {
                // Open the chest
                OpenChest();
            }
            else {
                ChestAlreadyOpen();
            }
        }
        
    }

    public void OpenChest()
    {
       
        dialogBox.SetActive(true); // Dialog window on
        dialogText.text = contents.itemDesc; // dialog text = contents text
        playerInventory.addItem(contents); 
        playerInventory.currentItem = contents; // add contents to the inventory
        raiseItem.Raise(); // Raise the signal to the player to animate
        context.Raise(); // raise the context clue
        isOpen = true; 
        anim.SetBool("IfOpen", true); // set the chest to opened
    }

    public void ChestAlreadyOpen()
    {
        dialogBox.SetActive(false);// Dialog off
        raiseItem.Raise();// raise signal to player to cancel raise animation
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = true;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            playerInRange = false;
        }
    }
}


