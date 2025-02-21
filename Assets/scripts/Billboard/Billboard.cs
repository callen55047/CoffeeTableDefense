using UnityEngine;

namespace Billboard
{
    public class Billboard : MonoBehaviour
    {
        void Update()
        {
            // Make the object face the camera
            if (Camera.main != null)
            {
                // Option 1: Use LookAt, then adjust if needed.
                // transform.LookAt(Camera.main.transform);
                // Alternatively, create a rotation that faces the camera but keeps the bar upright:
                Vector3 direction = transform.position - Camera.main.transform.position;
                direction.y = 0; // Lock the Y-axis to keep it upright
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}