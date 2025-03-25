using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour
{
    public GameObject statsPanel;
    public Text organismCountText;
    public Text foodCountText;
    public Text averageSpeedText;
    public Text averagePerceptionText;
    public Text averageMaxEnergyText;
    public Text RuntimeText;

    private bool menuOpen = false;
    private float runtime = 0f;
    void Start()
    {
        statsPanel.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            menuOpen = !menuOpen;
            statsPanel.SetActive(menuOpen);
        }

        // Stats don't need to update every frame, only when the menu is open
        if (menuOpen)
        {
            UpdateStats();
        }

        runtime += Time.deltaTime;
    }
    
    void UpdateStats()
    {
        GameObject[] organisms = GameObject.FindGameObjectsWithTag("Organism");
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Food");

        organismCountText.text = "Current Organism Count: " + organisms.Length;
        foodCountText.text = "Current Food Count: " + organisms.Length;

        float totalSpeed = 0f;
        float totalPerception = 0f;
        float totalMaxEnergy = 0f;

        // Add up to find totals of all values, to then divid to find average
        foreach (GameObject organism in organisms)
        {
            Organism iOrganism = organism.GetComponent<Organism>();


            if (iOrganism != null)
            {
                totalSpeed += iOrganism.speed;
                totalPerception += iOrganism.perception;
                totalMaxEnergy += iOrganism.maxEnergy;
            }
        }

            float avgSpeed = 0f;
            float avgPerception = 0f;
            float avgMaxEnergy = 0f;

            if (organisms.Length > 0)
            {
                avgSpeed = totalSpeed / organisms.Length;
                avgPerception = totalPerception / organisms.Length;
                avgMaxEnergy = totalMaxEnergy / organisms.Length;
            }

            // Display the averages to 2 d.p. 
            averageSpeedText.text = "Avg Speed: " + avgSpeed.ToString("F2");
            averagePerceptionText.text = "Avg Perception: " + avgPerception.ToString("F2");
            averageMaxEnergyText.text = "Avg Max Energy: " + avgMaxEnergy.ToString("F2");

            RuntimeText.text = "Runtime: " + runtime.ToString("F2");
    }
}
