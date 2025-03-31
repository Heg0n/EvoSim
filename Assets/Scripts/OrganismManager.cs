using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class OrganismManager : MonoBehaviour
{
    public int startOrganismCount = 10;
    public GameObject organismPrefab;
    
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            string name = "Organism" + i;
            Vector3 pos = GetRandomPos();
            
            // Assign slightly varying, randomised values for each stat
            float speed = Random.Range(3f, 7f); // 5�2
            float perception = Random.Range(6f, 10f); // 8�2
            float maxEnergy = Random.Range(90f, 110f); // 100�10
            CreateOrganism(name, pos, speed, perception, maxEnergy);
        }
    }
    void CreateOrganism(string name, Vector3 pos, float speed, float perception, float maxEnergy)
    {
        // Creates new GameObject with the Organism prefab, random position, and Quarternion.Identity sets the rotation to (0, 0, 0)
        GameObject newOrganism = Instantiate(organismPrefab, pos, Quaternion.identity);
        newOrganism.tag = "Organism";

        Organism organism = newOrganism.GetComponent<Organism>();

        if (organism != null )
        {
            organism.name = name;
            organism.Initialize(speed, perception, maxEnergy);

            AddOrganismMenu(newOrganism, organism);
        }
    }
    void AddOrganismMenu(GameObject organismObject, Organism organismComponent)
    {
        // Creates a new GameObject to act as the menu and attach it to the organism
        GameObject canvasObject = new GameObject("Menu");
        canvasObject.transform.SetParent(organismObject.transform);

        // Adds a Canvas component to the menu for rendering UI elements
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Adjusts the size and position of the canvas
        RectTransform canvasRect = canvasObject.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(2, 1); // Sets the size of the canvas
        canvasRect.localPosition = new Vector3(0, 2.5f, 0); // Positions the canvas above the organism
        canvasRect.localRotation = Quaternion.Euler(90, 0, 0); // Rotates the canvas to face the camera

        // Creates a panel to hold the text elements
        GameObject panelObject = new GameObject("Panel");
        panelObject.transform.SetParent(canvasObject.transform);

        // Adds an Image component to the panel to give it a background
        Image panelImage = panelObject.AddComponent<Image>();
        panelImage.color = Color.black;

        // Adjusts the size and position of the panel
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.sizeDelta = new Vector2(2, 1); // Matches the size of the canvas
        panelRect.localPosition = Vector3.zero; // Centers the panel within the canvas

        AddTextElement(panelObject, "NameText", organismComponent.name, new Vector2(0, 40));
        AddTextElement(panelObject, "SpeedText", "Speed: " + organismComponent.speed.ToString("F2"), new Vector2(0, 20));
        AddTextElement(panelObject, "PerceptionText", "Perception: " + organismComponent.perception.ToString("F2"), new Vector2(0, 0));
        AddTextElement(panelObject, "MaxEnergyText", "Max Energy: " + organismComponent.maxEnergy.ToString("F2"), new Vector2(0, -20));
        AddTextElement(panelObject, "EnergyText", "Energy: " + organismComponent.energy.ToString("F2"), new Vector2(0, -40));

        // Stores the menu reference in the Organism component
        organismComponent.menu = canvasObject;

        // Hides the menu on default
        canvasObject.SetActive(false);

    }

    void AddTextElement(GameObject panel, string name, string text, Vector2 position) {
        GameObject textObject = new GameObject(name);
        textObject.transform.SetParent(panel.transform);
        
        Text textComponent = textObject.AddComponent<Text>();
        textComponent.text = text;
        textComponent.color = Color.white;
        textComponent.fontSize = 14;
        textComponent.alignment = TextAnchor.MiddleCenter; // Align text to the center horizontally and vertically

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(180, 30); // Adjust size
        textRect.localPosition = position; // Set the position relative to the panel
    }
    Vector3 GetRandomPos()
    {
        float randomX = Random.Range(-50f, 50f);
        float randomZ = Random.Range(-50f, 50f);

        return new Vector3(randomX, 2f, randomZ);
    }
}
