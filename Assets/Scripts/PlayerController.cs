using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // We use this to move the player.
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }
    
   private void FixedUpdate()
   {
       // Get our X movement input from Unity.
       var horizontalInput = Input.GetAxis("Horizontal");
        
        // Move the player based on moveSpeed and horizontal input.
         _rb.MovePosition(_rb.position + new Vector3(horizontalInput * GameManager.Instance.sidewaysMoveSpeed * Time.fixedDeltaTime, 0, 0));
    }
}
