using UnityEngine;

public class ThirdPersonCameraCOntroller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform orientationTransform;
    [SerializeField] private Transform playerVisualTransform;
    
    [Header("Settings")]
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play 
            && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        Vector3 viewDÝrection = 
            playerTransform.position - new Vector3 (transform.position.x,playerTransform.position.y,transform.position.z);

        orientationTransform.forward = viewDÝrection.normalized;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection
            = orientationTransform.forward * verticalInput + orientationTransform.right * horizontalInput;

        if (inputDirection != Vector3.zero)
        {
            playerVisualTransform.forward
                = Vector3.Slerp(playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
        }
    } 
}
