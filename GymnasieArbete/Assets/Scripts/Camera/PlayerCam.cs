using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] float mouseSensitivityX = 100f;
    [SerializeField] float mouseSensitivityY = 100f;

    [SerializeField] KeyCode interactKey = KeyCode.F;
    [SerializeField] float interactionDistance = 200f;


    float xRotation = 0f;
    float yRotation = 0f;

    public Transform orientation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();

        if (Input.GetKeyDown(interactKey))
        {
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit, interactionDistance, LayerMask.GetMask("InteractableLayer")))
            {
                Interaction hitObject = hit.collider.gameObject.GetComponent<Interaction>();
                hitObject.Interact();
            }
            Debug.DrawRay(transform.position, Camera.main.transform.forward, Color.red, 2f);
        }
    }

    void LookAround()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
