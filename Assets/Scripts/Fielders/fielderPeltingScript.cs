﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderPeltingScript : MonoBehaviour
{
    public List<Transform> fieldingTeam;
    public GameObject ball;
    [SerializeField] private Transform player = null;
    private bool canThrow = false;
    private bool hasReadiedAThrow = false;

    private void Start()
    {
        //Populate fieldingTeam list with the children of this gameObject
        foreach (Transform child in transform)
        {
            fieldingTeam.Add(child);
        }
    }

    private void Update()
    {
        //Dev Toggle, replace with hitting the pitcher's ball
        if (Input.GetKeyDown(KeyCode.E))
        {
            canThrow = !canThrow;
        }

        if (canThrow == true)
        {
            throwableUpdate();
        }
    }

    private void throwableUpdate()
    {
        int numberOfBallsToThrow;
        List<Transform> chosenFielders = new List<Transform>();
        if (hasReadiedAThrow == false)
        {

            //Block to choose how many balls to throw
            var throwCountValue = Random.Range(0, 9);
            if(throwCountValue == 8)
            {
                numberOfBallsToThrow = 2;
            }
            else if(throwCountValue == 9)
            {
                numberOfBallsToThrow = 3;
            }
            else
            {
                numberOfBallsToThrow = 1;
            }

            //The variable "numberOfBallsToThrow" is now holding how many balls the fielders will throw, we now need to find who will throw them
            while (numberOfBallsToThrow > 0)
            {
                chosenFielders.Add(fieldingTeam[Random.Range(0, fieldingTeam.Count)]);
                numberOfBallsToThrow--;
            }

            //Cool, we now have a list populated with the fielders that will throw the ball. Now all we need to do is, get them to do that...
            foreach(Transform fielder in chosenFielders)
            {
                fielder.LookAt(player);
                Instantiate(ball, new Vector3(fielder.position.x, fielder.position.y, fielder.position.z), fielder.rotation);
            }





            //hasReadiedAThrow = true;
            canThrow = false;
        }
    }
}