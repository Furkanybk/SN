using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

enum DivideAxis
{
    X,
    Z
};

public class SpawnOrc : MonoBehaviour
{

    [SerializeField] 
    private DivideAxis DivideAxis;

    [SerializeField] 
    private GameObject Orc;

    [SerializeField]
    private GameObject MoveSpot;

    private Transform parent = null;

    public float OrcNumber; 
    public float DivideNumber;

    [SerializeField] private float Margin = 0;
    private static int OrcCount = 0;

    private void Awake()
    {
        parent = GameObject.Find("OrcSpawn").transform;

        float ScaleX = transform.lossyScale.x - Margin * 2;
        float ScaleZ = transform.lossyScale.z - Margin * 2;

        float startZ = transform.position.z - ScaleZ / 2;
        float endZ = transform.position.z + ScaleZ / 2;
        float startX = transform.position.x - ScaleX / 2;
        float endX = transform.position.x + ScaleX / 2;
         
        switch (DivideAxis)
        {
            case DivideAxis.X:
                for (int j = 0; j < DivideNumber; j++)
                {
                    float pieceS = startX + ScaleX / DivideNumber * j;
                    float pieceE = startX + ScaleX / DivideNumber * (j + 1);

                    Debug.DrawLine(new Vector3(pieceS, transform.position.y, transform.position.z), new Vector3(pieceS, transform.position.y + 5, transform.position.z), Color.red, 100);
                    Debug.DrawLine(new Vector3(pieceE, transform.position.y, transform.position.z), new Vector3(pieceE, transform.position.y + 5, transform.position.z), Color.red, 100);

                    Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, startZ), new Vector3(transform.position.x, transform.position.y + 5, startZ), Color.red, 100);
                    Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, endZ), new Vector3(transform.position.x, transform.position.y + 5, endZ), Color.red, 100);

                    //Debug.Log("PieceS:" + pieceS + " PieceE: " + pieceE);

                    for (int i = 0; i < OrcNumber; i++)
                    {
                        float x = Random.Range(pieceS, pieceE);
                        float z = Random.Range(startZ, endZ);

                        Vector3 position = new Vector3(x, 2f, z);
                        GameObject obj = Instantiate(Orc, position, Quaternion.identity, parent);
                        obj.name = "Orc " + OrcCount++;

                        OrcController oc = obj.GetComponent<OrcController>();
                        oc.MoveSpot = Instantiate(MoveSpot, position, Quaternion.identity, parent).transform;
                        oc.MoveSpot.name = obj.name + " Move Spot";

                        oc.Speed = 3;
                        oc.StartWaitTime = 5;
                        oc.minX = pieceS;
                        oc.maxX = pieceE;
                        oc.minZ = startZ;
                        oc.maxZ = endZ;
                        oc.Setup();
                    }
                }
                break;
            case DivideAxis.Z:
                for (int j = 0; j < DivideNumber; j++)
                {
                    float pieceS = startZ + ScaleZ / DivideNumber * j;
                    float pieceE = startZ + ScaleZ / DivideNumber * (j+1);

                    Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, pieceS), new Vector3(transform.position.x, transform.position.y + 5, pieceS), Color.red, 100);
                    Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, pieceE), new Vector3(transform.position.x, transform.position.y + 5, pieceE), Color.red, 100);


                    Debug.DrawLine(new Vector3(startX, transform.position.y, transform.position.z), new Vector3(startX, transform.position.y + 5, transform.position.z), Color.red, 100);
                    Debug.DrawLine(new Vector3(endX, transform.position.y, transform.position.z), new Vector3(endX, transform.position.y + 5, transform.position.z), Color.red, 100);

                    //Debug.Log("PieceS:" + pieceS + " PieceE: " + pieceE);

                    for (int i = 0; i < OrcNumber; i++)
                    {
                        float x = Random.Range(startX, endX);
                        float z = Random.Range(pieceS, pieceE);

                        Vector3 position = new Vector3(x, 2f, z); 
                        GameObject obj = Instantiate(Orc, position, Quaternion.identity, parent);

                        OrcController oc = obj.GetComponent<OrcController>();
                        oc.MoveSpot = Instantiate(MoveSpot, position, Quaternion.identity, parent).transform;
                        oc.Speed = 3.5f;
                        oc.StartWaitTime = 1.75f;
                        oc.minX = startX;
                        oc.maxX = endX;
                        oc.minZ = pieceS;
                        oc.maxZ = pieceE;
                        oc.Setup();
                    }
                }
                break;
        } 
    }
} 