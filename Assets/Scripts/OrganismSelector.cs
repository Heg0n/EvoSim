using UnityEngine;

public class OrganismSelector : MonoBehaviour
{
    public Camera mainCamera;
    public Material outlineMaterial;

    private GameObject currentTarget = null;
    private Material originalMaterial;

    private bool isTracking = false;

    // UI elements for displaying stats
    public GameObject menuPanel;
    public Text nameText;
    public Text speedText;
    public Text perceptionText;
    public Text maxEnergyText;
    public Text energyText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleTracking();
        }
        
        // If tracking, no need to update highlighting
        if (!isTracking)
        {
            UpdateHighlighting();
        }
        else if (currentTarget != null)
        {
            FollowTarget();
        }
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

            // Hides the menu of the previously highlighted organism
            Organism organism = currentTarget.GetComponent<Organism>();
            if (organism != null && organism.menu != null)
            {
                organism.menu.SetActive(false);
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

            // Show the menu of the newly highlighted organism
            Organism organism = target.GetComponent<Organism>();
            if (organism != null && organism.menu != null)
            {
                organism.menu.SetActive(true);
            }
        }
    }

    void ToggleTracking() {
        if (isTracking)
        {
            isTracking = false;
            Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
            Cursor.visible = true; // Makes the cursor visible

            if (currentTarget != null)
            {
                // Hides the menu of the currently tracked organism
                Organism organism = currentTarget.GetComponent<Organism>();
                if (organism != null && organism.menu != null)
                {
                    organism.menu.SetActive(false);
                }
            }
        }
        else if (currentTarget != null)
        {
            isTracking = true;
            Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
            Cursor.visible = false; // Hides the cursor

            // Shows the menu of the currently tracked organism
            Organism organism = currentTarget.GetComponent<Organism>();
            if (organism != null && organism.menu != null)
            {
                organism.menu.SetActive(true);
            }
        }
    }

    void FollowTarget()
    {
        // Makes main camera lock on the current target
        mainCamera.transform.LookAt(currentTarget.transform.position);
    }
}
