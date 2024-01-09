using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Animal", menuName = "Animals")]
public class AnimalSO : ScriptableObject
{
    public new string name;
    public int foodChainPosition;
    [Range(0f,1f)]
    public float intelligence;
    [Range(0f,1f)]
    public float maxHunger;
    [Range(0f,1f)]
    public float maxThirst;
    [Range(0f,1f)]
    public float maxHealth;
    [Range(0f,1f)]
    public float maxSpeed;
    [Range(0f,1f)]
    public float maxVision;
    [Range(0f,1f)]
    public float drinkSpeed;
    [Range(0f,1f)]
    public float eatSpeed;


}
