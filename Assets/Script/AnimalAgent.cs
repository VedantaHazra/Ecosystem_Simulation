using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AnimalAgent : Agent
{
    private AnimalScript animalScript;
    private PlayerMovement playerMovement;

    
    private void Start()
    {
        animalScript = GetComponent<AnimalScript>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public override void OnEpisodeBegin()
    {
        animalScript.SetHealth();
        transform.position = Vector3.zero;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0f;
        float turnAmount = 0f;

        switch(actions.DiscreteActions[0])
        {
            case 0 : forwardAmount = 0f; break;
            case 1 : forwardAmount = 1f; break;
            case 2 : forwardAmount = -1f; break;
        }

        switch(actions.DiscreteActions[1])
        {
            case 0 : turnAmount = 0f; break;
            case 1 : turnAmount = 1f; break;
            case 2 : turnAmount = -1f; break;
        }
        
        playerMovement.MyInput(forwardAmount, turnAmount);
        
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ) forwardAction = 1;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ) forwardAction = 2;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ) turnAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ) turnAction = 2;

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = forwardAction;
        discreteActions[1] = turnAction;
        
    }

    private void Update()
    {

        
        double health = animalScript.GetHealth();

        if(health<0.1f)
        {
            EndEpisode();
        }

        if(health < 0.3)
        {
            AddReward(-1f);
        }
        else if (health < 0.5)
        {
            AddReward(-0.8f);
        }
        else if (health < 0.8f)
        {
            AddReward(0.5f);
        }
        else{
            AddReward(1f);
        }
        
    }
}