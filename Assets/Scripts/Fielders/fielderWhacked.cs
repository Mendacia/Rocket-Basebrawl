using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderWhacked : MonoBehaviour
{
    [SerializeField] private fielderPeltingScript peltingScript;
    [SerializeField] private BallList ballGod;
    [SerializeField] private GameObject ragdoll;
    [SerializeField] private float hitStrength = 1;

    private Color wantedColorL, wantedColorR;
    private Material wantedMaterial;

    private void Start()
    {
        peltingScript = gameObject.GetComponent<fielderPeltingScript>();
    }

    public void FindAndDestroyFielder(Transform fielder)
    {
        if (peltingScript.fieldingTeam.Contains(fielder))
        {
            wantedMaterial = fielder.GetComponent<UniqueFielderDataHolder>().fielderMat;
            wantedColorL = fielder.GetComponent<UniqueFielderDataHolder>().eyeL;
            wantedColorR = fielder.GetComponent<UniqueFielderDataHolder>().eyeR;
            foreach (masterBallStruct ball in ballGod.masterBallList)
            {
                foreach(Transform ballFielder in ball.myFielders)
                {
                    if(ballFielder == fielder && ball.currentState == ballState.INACTIVE)
                    {
                        //Set it to missed
                    }
                    ball.myFielders.Remove(fielder);
                }
            }
            Vector3 fielderPosition = fielder.position;
            peltingScript.fieldingTeam.Remove(fielder);
            Destroy(fielder.gameObject);

            if (Random.Range(0,2) == 0)
            {
                FielderWhackUp(fielderPosition);
            }
            else
            {
                FielderWhackUp(fielderPosition);
            }
        }
    }

    private void FielderWhackUp(Vector3 spawnLocation)
    {
        var myRagdoll = Instantiate(ragdoll);
        var ragData = myRagdoll.GetComponentInChildren<RagdolllDataHolder>();
        ragData.myMesh.material = wantedMaterial;
        ragData.myEyeL.color = wantedColorL;
        ragData.myEyeR.color = wantedColorR;
        myRagdoll.transform.position = spawnLocation;
        myRagdoll.transform.rotation =  new Quaternion (Camera.main.transform.rotation.y, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, 0);
        myRagdoll.transform.position += new Vector3(0, 0.5f, 1);
        myRagdoll.GetComponent<fielderPeltingBallBehaviour>().changeDirection();
    }

    private void FielderWhackDown(Vector3 spawnLocation)
    {

    }
}
