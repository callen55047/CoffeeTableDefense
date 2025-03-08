using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace BoardPlacement
{
    public class BoardPlacementManager : MonoBehaviour
    {
        [Header("AR Components")] 
        public ARRaycastManager arRaycastManager;
        public GameObject boardPreview; // The ghost board in the AR world

        [Header("UI Controls")] 
        public Slider heightSlider;
        public Slider scaleSlider;
        public Slider rotationSlider;
        public Button confirmButton;

        void Start()
        {
            // Optionally set initial slider values based on the boardPreview
            if (boardPreview != null)
            {
                heightSlider.value = boardPreview.transform.position.y;
                scaleSlider.value = boardPreview.transform.localScale.x;
                rotationSlider.value = boardPreview.transform.eulerAngles.y;
            }

            // Attach Confirm button listener
            confirmButton.onClick.AddListener(ConfirmPlacement);
        }

        void Update()
        {
            // Update the board preview based on slider values
            if (boardPreview != null)
            {
                // Adjust height (Y position)
                Vector3 pos = boardPreview.transform.position;
                pos.y = heightSlider.value;
                boardPreview.transform.position = pos;

                // Adjust scale (assuming uniform scaling)
                boardPreview.transform.localScale = Vector3.one * scaleSlider.value;

                // Adjust rotation around Y axis
                boardPreview.transform.rotation = Quaternion.Euler(0, rotationSlider.value, 0);
            }

            // Optionally, integrate AR raycasting to adjust boardPreview's horizontal position
            // (Your existing AR raycast code can run here to update boardPreview.position on X and Z)
        }


        private void ConfirmPlacement()
        {
            // Finalize board placement, e.g., instantiate a permanent board, hide placement UI, etc.
            Debug.Log("Board placement confirmed!");
            // Additional logic to lock in placement goes here.
        }
    }

}