using UnityEngine;

public class MouseHoverMaterialChange : MonoBehaviour
{
    private Renderer modelRenderer;
    public Material grayedMaterial;  // The material for the original "grayed" look
    public Material coloredMaterial; // The material to switch to on hover

    void Start()
    {
        // Get the Renderer component of the model
        modelRenderer = GetComponent<Renderer>();
        // Set the initial material to the grayed material
        modelRenderer.material = grayedMaterial;
    }

    void Update()
    {
        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit this object
            if (hit.transform == transform)
            {
                // Change the material to the colored one when the mouse is over the model
                modelRenderer.material = coloredMaterial;
            }
            else
            {
                // Revert to the grayed material if the mouse is not over the model
                modelRenderer.material = grayedMaterial;
            }
        }
        else
        {
            // Revert to the grayed material if the raycast doesn't hit anything
            modelRenderer.material = grayedMaterial;
        }
    }
}
