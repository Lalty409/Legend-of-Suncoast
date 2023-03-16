using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerState {
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState; 
    public float speed; 
    private Rigidbody2D rigb;
    private Vector3 change;
    private Animator anim;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public VectorValue startingPos;
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    public string sceneToLoad;


    void Start() {
        currentState = PlayerState.walk;
        anim = GetComponent<Animator>();
        rigb = GetComponent<Rigidbody2D>();
        anim.SetFloat("MoveX", 0);
        anim.SetFloat("MoveY", -1);
        transform.position = startingPos.initialValue;
        playerInventory.numberOfKeys = 0;
    }


    void Update() {

        //interaction
        if(currentState == PlayerState.interact) {
            return;
        }

        //movement
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        change.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        
        //attack
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack 
        && currentState != PlayerState.stagger) {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)  {
            UpdateAnimationAndMove();
        }
        
    }   

    //attack coroutine
    private IEnumerator AttackCo()  {
        anim.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return new WaitForSeconds(.1f);
        anim.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        if(currentState != PlayerState.interact) {
            currentState = PlayerState.walk;
        }
    }


    public void RaiseItem() {
        if (playerInventory.currentItem != null) {
            if(currentState != PlayerState.interact) {
                anim.SetBool("Recieved", true);
                currentState = PlayerState.interact;
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite; //sets the item sprite to above player sprite
            }
            else
                anim.SetBool("Recieved", false);
                currentState = PlayerState.idle;
                recievedItemSprite.sprite = null;
        }
    }


    //animations for moving
    void UpdateAnimationAndMove() {
        if (change != Vector3.zero) {
            transform.Translate(new Vector3(change.x, change.y));
            anim.SetFloat("MoveX", change.x*200);
            anim.SetFloat("MoveY", change.y*200);
            anim.SetBool("Moving", true);
        }
        else    {
            anim.SetBool("Moving", false);
        }
    }

    //currently unused (REWATCH PLAYERMOVEMENT TUTORIAL)
    void MoveCharacter() {
        change.Normalize();
        rigb.MovePosition(transform.position + change * speed * Time.deltaTime); 
    }

    //controls being hit
    public void Knock(float knockTime, float damage) {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if(currentHealth.runtimeValue > 0) {
            StartCoroutine(KnockCo(knockTime));
        }
        else {
            this.gameObject.SetActive(false);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        }
    }

    //knockback coroutine
    private IEnumerator KnockCo(float knockTime) {
        if(rigb != null) {
            yield return new WaitForSeconds(knockTime);
            rigb.velocity = Vector2.zero;

            currentState = PlayerState.idle;
            rigb.velocity = Vector2.zero;
        }
    }

}