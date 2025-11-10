using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float gravity = 9.81f * 2; //make dino fall faster

    public float jumpForce = 8f;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable(){

        //whenever Player is reenabled, direction = 0
        direction = Vector3.zero;
    }
    // Update is called once per frame
    private void Update()
    {
        // applies gravity, downwards; gravity "builds up" over time
        direction += Vector3.down * gravity * Time.deltaTime;

        // check grounded 
        if (character.isGrounded)
        {
            direction = Vector3.down; // resets gravity 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction = Vector3.up * jumpForce;
                GameManager.Instance.PlaySFX(GameManager.Instance.jumpSFX);
            }
        }
        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.PlaySFX(GameManager.Instance.collideSFX);
            GameManager.Instance.GameOver(); // why instance? 
        }
    }
}
