using UnityEngine;

public class OrganismSelector : MonoBehaviour
{
    public Camera mainCamera;
    public Material outlineMaterial;

    private GameObject currentTarget = null;
    private Material originalMaterial;

    void Update()
    {
        UpdateHighlighting();
    }

    void UpdateHighlighting()
    {
        // Creates from the centre of the screen, crosshair position, at W/2 H/2
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit; // Info on object of collision

        // If the scanned object is an Organism, and different from the last scanned object, apply a new shader
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Organism"))
            {
                if (currentTarget != hit.collider.gameObject)
                {
                    ClearHighlight(); // Remove highlight from previous target
                    ApplyHighlight(hit.collider.gameObject); // Highlight new target
                }
                return;
            }
        }

        ClearHighlight();
    }

    void ClearHighlight()
    {
        if (currentTarget != null)
        {
            Debug.Log("Target is not null");
            Renderer renderer = currentTarget.GetComponent<Renderer>();
            if (renderer != null)
            {
                Debug.Log("Renderer is not null");
                renderer.material = originalMaterial; // Restore original material
            }
            currentTarget = null; // Cleared stored target
        }
    }

    void ApplyHighlight(GameObject target)
    {
        Renderer renderer = target.GetComponent<Renderer>();
        if (renderer != null)
        {
            currentTarget = target;
            
            // Store the original material temporarily to replace after highlight is removed
            originalMaterial = renderer.material;
            renderer.material = outlineMaterial;
        }
    }
}
