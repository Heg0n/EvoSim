using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR.Haptics;
using Random = UnityEngine.Random;

public class Organism : MonoBehaviour
{
    public float speed;
    public float perception;

    public int age;
    public int maxAge;

    public float energy;
    public float maxEnergy;
    public float energyDecreaseRate = 0.1f; // Rate energy decreases while wandering
    public float energyIncreaseAmount = 20f; // Energy gained from food

    public float moveInterval;
    private float timeSinceLastMove;
    private Vector3 moveDirection;

    private GameObject targetFood;

    State activeState;
    private enum State
    {
        Wandering,
        FoundFood
    }

    public void Initialize(float speed, float perception, float maxEnergy)
    {
        age = 0;
        maxAge = 100;

        this.speed = speed;
        this.perception = perception;
        this.maxEnergy = maxEnergy;
        energy = maxEnergy;
    }

        void Start()
    {
        activeState = State.Wandering;
        ChangeRandomDirection(); // Begin with a direction
    }

    void Update()
    {
        switch (activeState)
        {
            case State.Wandering:
                WanderingBehavior(); break;
            case State.FoundFood:
                FoundFoodBehavior(); break;
        }

        if (activeState == State.Wandering)
        {
            energy -= energyDecreaseRate * Time.deltaTime;
            // Add death when energy <= 0
        }
    }
    
    void ChangeRandomDirection()
    {

        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        // Linear Interpolation allows for smooth movement
        moveDirection = new Vector3(randomX, 0f, randomZ).normalized;
    }

    void WanderingBehavior()
    {
        timeSinceLastMove += Time.deltaTime;

        if (timeSinceLastMove > moveInterval)
        {
            ChangeRandomDirection();
            timeSinceLastMove = 0f;
        }

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        CheckForFood();
    }

    void FoundFoodBehavior()
    {
        if (targetFood != null)
        {
            Vector3 foodDirection = (targetFood.transform.position - transform.position).normalized;
            transform.Translate(foodDirection * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, targetFood.transform.position) < 1.5f)
            {
                Eat();
                targetFood = null;
                activeState = State.Wandering;
            }
        } else
        {
            activeState = State.Wandering;
        }
    }

    void CheckForFood()
    {
        // Circle hitbox at pos with radius perception
        Collider[] hitboxColliders = Physics.OverlapSphere(transform.position, perception);

        float minDistance = Mathf.Infinity;
        GameObject closestFood = null;

        foreach(Collider collider in hitboxColliders)
        {
            if (collider.CompareTag("Food"))
            {
                float foodDistance = Vector3.Distance(transform.position, collider.transform.position);

                if (foodDistance < minDistance)
                {
                    minDistance = foodDistance;
                    closestFood = collider.gameObject;
                }
            }
        }
        if (closestFood != null)
        {
            targetFood = closestFood;
            activeState = State.FoundFood;
        }
    }
    void Eat()
    {
        energy += energyIncreaseAmount;

        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }

        // Possible sound effects/animations to demonstrate eating for user

        if (targetFood != null)
        {
            Destroy(targetFood); // Destroy the food object after consumption
        }
    }
}
