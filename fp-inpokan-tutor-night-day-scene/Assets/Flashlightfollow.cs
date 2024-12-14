using UnityEngine;
using UnityEngine.UI;

public class FlashlightFollow : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Light flashlight; // Reference to the Light component
    public Button toggleButton; // Reference to the UI Button
    public float depth = 10.0f; // Distance from the camera

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found. Please ensure the camera is tagged as 'MainCamera'.");
            }
        }

        // Assign the toggle method to the button's onClick event
        toggleButton.onClick.AddListener(ToggleFlashlight);

        // Get the Light component
        flashlight = GetComponent<Light>();
    }

    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void LateUpdate()
    {
        if (mainCamera != null && flashlight.enabled)
        {
            PointLightAtCursor();
        }
    }

    void PointLightAtCursor()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 targetPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, depth));
        Vector3 direction = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    void ToggleFlashlight()
    {
        flashlight.enabled = !flashlight.enabled;
    }
}
