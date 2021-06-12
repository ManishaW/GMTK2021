using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rb;
    private CircleCollider2D boxCol;
    // public Animator maltAnimator;
    private float jumpTimeCounter;
    public float jumpTime;
    private int numJumps = 0;
    public static float jumpForce = 8.66f;
    public float moveSpeed = 2.5f;
    private bool isJumping;
    public SpriteRenderer spriteChar;
    private AudioSource [] sounds;
    public GameObject deathBurst;
    public int characterIndentifier;
    private Animator charAnimator;
    public static bool isFused;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCol = transform.GetComponent<CircleCollider2D>();
        sounds = GetComponents<AudioSource>();
        charAnimator = GetComponent<Animator>();
        isFused = false;
    }
    void handleJumping(){
        if (IsGrounded())
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow)||(Input.GetKey(KeyCode.Space)) && numJumps<2)){
                isJumping =true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                numJumps+=1;
                sounds[0].Play();

            }else{
                numJumps = 0;
            }
            charAnimator.SetBool("InAir",false);
        }else{
            charAnimator.SetBool("InAir",true);

        }

        if ((Input.GetKey(KeyCode.UpArrow)||(Input.GetKey(KeyCode.Space)) && numJumps<3))
        {
            if (jumpTimeCounter>0){
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -=Time.deltaTime;
                numJumps += 1;
                sounds[0].Play();

            }else{
                isJumping =false;
                numJumps = 5;

            }
        }
        
        if (Input.GetKeyUp(KeyCode.UpArrow)||(Input.GetKey(KeyCode.Space))){
            // numJumps = 0;
            isJumping =false;
            numJumps = 5;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.characterCurrent == characterIndentifier){
            handleJumping();
        }
        
        
        
      
    }
    private bool IsGrounded()
    {
        RaycastHit2D rc = Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0f, Vector2.down, 1f, platformsLayerMask);
        return rc.collider != null;
    }

    void FixedUpdate()
    {
        if (GameManager.characterCurrent == characterIndentifier){
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        
        // var speed = 10; 
        // rb.velocity = new Vector3(0,1,0) * speed; 
        float movement = Input.GetAxis("Horizontal");
        
        rb.velocity = new Vector2(movement*moveSpeed, rb.velocity.y);
        
        if (movement < 0f)
        {
            if (characterIndentifier==0) spriteChar.flipX = true; //blue
            else spriteChar.flipX = false; //pink
            charAnimator.SetBool("IsWalking",true);
        }
        else if (movement > 0f)
        {
             if (characterIndentifier==0) spriteChar.flipX = false; //blue
            else spriteChar.flipX = true; //pink
            charAnimator.SetBool("IsWalking",true);

        }else{
            charAnimator.SetBool("IsWalking",false);
        }

        


    }

     /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.name=="CharacterPink"){
          other.gameObject.SetActive(false);
          GameManager.characterCurrent = 0;
          //animation fusion
          isFused =true;
        //   this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
          jumpForce = 8.66f;
          sounds[1].Play();
          charAnimator.SetBool("IsFused",true);

      }
     
        // 
        //restart
    }
  /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        
       
    }
}
