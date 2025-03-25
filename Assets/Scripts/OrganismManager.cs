using UnityEngine;

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
        }
    }
    Vector3 GetRandomPos()
    {
        float randomX = Random.Range(-50f, 50f);
        float randomZ = Random.Range(-50f, 50f);

        return new Vector3(randomX, 2f, randomZ);
    }
}
