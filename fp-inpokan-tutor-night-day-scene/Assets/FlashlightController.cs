using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;

    public bool isFlashlightOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightOn = !isFlashlightOn;
        }

        // Pindahkan ini di luar pernyataan if
        flashlight.enabled = isFlashlightOn;

        if (isFlashlightOn)
        {
            // Kontrol kecerahan senter
            if (Input.GetKey(KeyCode.UpArrow))
            {
                flashlight.intensity += 0.1f;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                flashlight.intensity -= 0.1f;
            }

            // Kontrol warna senter
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                flashlight.color = Color.red;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                flashlight.color = Color.blue;
            }
        }
    }
}