using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorFieldGrids : MonoBehaviour
{
    public VectorField[,] VectorFeilds;
    public int gridSizeX;
    public int gridSizeY;
    public int VectorGridSize = 1;

    //Testing Nodes
    public GameObject TestingTile;
    public GameObject[,] TestingTiles;

    public bool VectorFieldUpdate = false;

    void Start()
    {
        VectorFeilds = new VectorField[VectorGridSize, VectorGridSize];
        TestingTiles = new GameObject[gridSizeX, gridSizeY];

        for (int x = 0; x < VectorGridSize; x++)
        {
            for (int y = 0; y < VectorGridSize; y++)
            {
                VectorFeilds[x, y] = new VectorField();
            }
        }

        VectorFeilds[0, 0].GenerateField(gridSizeX, gridSizeY, 5, 5);

        DebugDisplay();
    }

    void Update()
    {
        if(VectorFieldUpdate)
        {
            VectorFeilds[0, 0].Update(gridSizeX,gridSizeY, 5, 5);
            VectorFieldUpdate = false;

        }
    }

    void DebugDisplay()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                GameObject Temp = Instantiate(TestingTile, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                TextMesh TempMesh = Temp.GetComponentInChildren<TextMesh>();
                TempMesh.text = VectorFeilds[0, 0].FeildNodes[x, y].PathDistance.ToString();
                TestingTiles[x, y] = Temp;
            }
        }
    }
}
