using System.Collections;
using UnityEngine;

public class MouseHoverMaterialChange : MonoBehaviour
{
    private Renderer modelRenderer;
    public Material grayedMaterial;  // The material for the original "grayed" look
    public Material coloredMaterial; // The material to switch to on hover
    private Animator animator;
    [SerializeField] private CharacterData characterData;
    [SerializeField] private float selectDelayInSeconds = 2.5f;

    void Start()
    {
        // Get the Renderer component of the model
        modelRenderer = GetComponent<Renderer>();
        // Set the initial material to the grayed material
        modelRenderer.material = grayedMaterial;
        animator = GetComponentInParent<Animator>();


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
                animator.SetBool("IsHovered", true);
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(SelectCharacterAfterDelay());

                    
                }
            }
            else
            {
                // Revert to the grayed material if the mouse is not over the model
                modelRenderer.material = grayedMaterial;
                animator.SetBool("IsHovered", false);
                animator.SetBool("IsClicked", false);

            }
        }
        else
        {
            // Revert to the grayed material if the raycast doesn't hit anything
            modelRenderer.material = grayedMaterial;
            animator.SetBool("IsHovered", false);
            animator.SetBool("IsClicked", false);
        }
    }

    private IEnumerator SelectCharacterAfterDelay()
    {
        // Set the click bool to true to play the click animation
        animator.SetBool("IsClicked", true);
        yield return new WaitForSeconds(selectDelayInSeconds);
        CharacterManager.instance.SelectPlayerChar(characterData);
    }
}
