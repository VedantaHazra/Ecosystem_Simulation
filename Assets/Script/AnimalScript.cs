using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimalScript : MonoBehaviour
{
    
    public AnimalSO animalSO;
   
    private float hunger;
    private float thirst;
    private double health;
    private double speed;
    private double vision;

    public float hungryTime;
    public float thirstyTime;
    public float hungerWeight;
    public float thirstWeight;

    [Header("Surface Check")]
    public float surfaceCheckRadius = 0.3f;
    public Vector3 surfaceCheckOffset;
    public LayerMask waterLayer;
    public LayerMask forestLayer;
    private bool onWater = false;
   // private bool onForest = false;
    private void Start()
    {
        health = animalSO.maxHealth;
        hunger = animalSO.maxHunger;
        thirst = animalSO.maxThirst;
        speed = animalSO.maxSpeed;
        vision = animalSO.maxVision;
    }

    private void Update()
    {
        UpdateFeatures();
        CheckSurface();
        if(onWater && thirst < animalSO.maxThirst)
        {
            DrinkWater();
        }
        
      //  if(onForest && hunger < animalSO.maxHunger)
     //   {
    //        Eat();
     //   }
        
    }

    private void UpdateFeatures()
    {
        health = (Math.Pow(hunger, hungerWeight) * Math.Pow(thirst, thirstWeight) * animalSO.maxHealth)/(Math.Pow(animalSO.maxHunger,hungerWeight) * Math.Pow(animalSO.maxThirst,thirstWeight));

        hunger -= hungryTime * Time.deltaTime;
        thirst -= thirstyTime * Time.deltaTime; 

        speed = Math.Exp((health/animalSO.maxHealth) - 1) * animalSO.maxSpeed;
        vision = Math.Exp((health/animalSO.maxHealth)-1) * animalSO.maxVision;

        if(hunger<=0)
        {
            hunger = 0;
        }
        if(thirst <= 0)
        {
            thirst = 0;
        }

        if(health<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        health = 0;
    }

    private void CheckSurface()
    {
      onWater = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius,waterLayer);
      //onForest = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius,forestLayer);
    }

    private void DrinkWater()
    {
        thirst += animalSO.drinkSpeed * Time.deltaTime;
        if(thirst > animalSO.maxThirst)
        {
            thirst = animalSO.maxThirst;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffset), surfaceCheckRadius);
    }
    public double GetHealth()
    {
        return health;
    }
    public void SetHealth()
    {
        thirst = 1f;
        hunger = 1f;
    }
    
    private void Eat()
    {
        hunger += animalSO.eatSpeed * Time.deltaTime;
        if(hunger > animalSO.maxHunger)
        {
            hunger = animalSO.maxHunger;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        AnimalScript animalCollided = other.GetComponent<AnimalScript>();
        if (animalCollided != null)
        {
            int hisFoodChainPosition = animalCollided.animalSO.foodChainPosition;
            int myFoodChainPosition = animalSO.foodChainPosition;

            if (myFoodChainPosition == hisFoodChainPosition + 1 )
            {
                hunger += animalSO.eatSpeed * animalSO.maxHunger;
               // animalCollided.Die();
                if (other.tag == "Plant")
                {
                    Destroy(other.gameObject);
                }
                else
                {
                    AnimalAgent animalAgent = other.GetComponent<AnimalAgent>();
                    animalAgent.EndEpisode();
                }
            }
        }
        
    }
}
