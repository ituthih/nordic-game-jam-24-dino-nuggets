using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BeamGenerator : MonoBehaviour
{

    [SerializeField] private Vector3 leftUpCorner;
    [SerializeField] private Vector3 leftDownCorner;
    [SerializeField] private Vector3 rightUpCorner;
    [SerializeField] private Vector3 rightDownCorner;

    public GameObject beam;

    private enum Sides
    {
        Left,
        Right,
        Up,
        Down
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(beam,new Vector3(0, 1.5f, -8), Quaternion.identity);
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnBeam();
        }
    }

    private void SpawnBeam()
    {
        var side = (Sides)Random.Range(0, 4);
        Vector3 spawnPoint = Vector3.zero;
        //Vector3 spawnPoint = new Vector3(0,5,-10);
   
        switch (side)
        {
            case Sides.Left:
                spawnPoint = Utils.GetRandomVector3Between(leftUpCorner, leftDownCorner);
                break;
            case Sides.Right:
                spawnPoint = Utils.GetRandomVector3Between(rightUpCorner, rightDownCorner);
                break;
            case Sides.Up:
                spawnPoint = Utils.GetRandomVector3Between(leftUpCorner, rightUpCorner);
                break;
            case Sides.Down:
                spawnPoint = Utils.GetRandomVector3Between(leftDownCorner, rightDownCorner);
                break;
            default:
                break;
        }

        spawnPoint.y = transform.position.y;
        
        Vector2 randomPoint =  Random.insideUnitCircle * 6;
        Vector3 spawnDirection =  new Vector3(randomPoint.x, transform.position.y,randomPoint.y ) - spawnPoint;
        
        GameObject clone = Instantiate(beam, spawnPoint, beam.transform.rotation);
        clone.transform.forward = spawnDirection.normalized;
    }
    
}
