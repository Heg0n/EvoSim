using UnityEngine;
using Random = UnityEngine.Random;

public class FoodManager : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject food = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            food.name = "Food" + i;
            food.transform.position = new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f));
            food.tag = "Food";
        }
    }
}
