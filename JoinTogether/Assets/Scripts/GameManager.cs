using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int characterCurrent;
    public GameObject characterBlue;
    private Rigidbody2D rbChar;
    private Animator charAnimator;
    public Animator charPinkAnimator; 
    void Start()
    {
        characterCurrent = 0;
        rbChar =characterBlue.GetComponent<Rigidbody2D>();
        charAnimator = characterBlue.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !CharacterMovement.isFused){
            charAnimator.SetBool("IsWalking",false);
            charAnimator.SetBool("InAir",false);
            charPinkAnimator.SetBool("IsWalking",false);
            charPinkAnimator.SetBool("InAir",false);
            
            if (characterCurrent<1) {
                characterCurrent = characterCurrent +1;
                CharacterMovement.jumpForce = CharacterMovement.jumpForce *-1;
            }else{
                characterCurrent = 0;
                CharacterMovement.jumpForce = 8.66f;
            }
        }

        // if (Input.GetKeyDown(KeyCode.Z)){
        //     Debug.Log(CharacterMovement.jumpForce + " " + rbChar.gravityScale);
        //     CharacterMovement.jumpForce = CharacterMovement.jumpForce *-1;
        //     // rbChar.gravityScale = rbChar.gravityScale*-1;
        // }

        if (Input.GetKeyDown(KeyCode.X) && CharacterMovement.isFused){
            rbChar.gravityScale = rbChar.gravityScale*-1;
            CharacterMovement.jumpForce = CharacterMovement.jumpForce *-1;
            Debug.Log(CharacterMovement.jumpForce);
            characterBlue.transform.eulerAngles = new Vector3(
            characterBlue.transform.eulerAngles.x + 180,
            characterBlue.transform.eulerAngles.y,
            characterBlue.transform.eulerAngles.z
            );
        }
    }
}
