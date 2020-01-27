using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(BoxCollider))]
public class DiscoTiler : MonoBehaviour
{
    private BoxCollider col;
    public GameObject baseTile;
    public int Rows = 2, Colums = 2;
    public List<Color> colorsToLoopThrough;

    private List<GameObject> childrenGameObject;
    private GonzakoUtils.DataStructures.Pool<GameObject> pool;

    private Vector3 gridPos;
    private Vector3 oddCheck;
    private Vector3 tileSize;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        pool = new GonzakoUtils.DataStructures.Pool<GameObject>(Rows * Colums + 5, baseTile, this.transform);



    }

    private void fillCollider(int rows, int colums)
    {
        tileSize.x = col.size.x/rows;
        tileSize.y = col.size.y;
        tileSize.z = col.size.z/colums;

        

        oddCheck.x = gridPos.x % 2 == 1 ? 0f : 0.5f;
        oddCheck.y = gridPos.y % 2 == 1 ? 0f : 0.5f;
        oddCheck.z = gridPos.z % 2 == 1 ? 0f : 0.5f;

        for (float i = 0; i < rows; i++)
        {
            for (float j = 0; j < colums; j++)
            {
                var newTile = pool.getNextObj();
                newTile.transform.localScale = tileSize;
                newTile.transform.position = new Vector3(,,);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SubCollider
{

    public Transform cube;
    GameObject[] cubes;
    // Vector3
    int gridX;
    int gridY;
    int gridZ;
    // our box collider
    BoxCollider bc;
    // even or odd?
    float rtnX;
    float rtnY;
    float rtnZ;

    void Awake()
    {  // find the box collider
    
        // compute if X, Y or Z scale are even or odd
        if (gridX % 2 == 1)
            rtnX = 0.0f;
        else
            rtnX = 0.5f;

        if (gridY % 2 == 1)
            rtnY = 0.0f;
        else
            rtnY = 0.5f;

        if (gridZ % 2 == 1)
            rtnZ = 0.0f;
        else
            rtnZ = 0.5f;
    }

    void Start()
    {  // display for each integer/Vector3 inside the box collider a sub object
        for (int x = 0; x < gridX; x = x + 1)
        {
            for (int y = 0; y < gridY; y = y + 1)
            {
                for (int z = 0; z < gridZ; z = z + 1)
                    Instantiate(cube, new Vector3((transform.position.x + x - (Mathf.FloorToInt(gridX / 2)) + rtnX),
                        (transform.position.y + y - (Mathf.FloorToInt(gridY / 2)) + rtnY),
                        (transform.position.z + z - Mathf.FloorToInt(gridZ / 2)) + rtnZ), Quaternion.identity);
            }
        }
        // hierarchy
        foreach (GameObject cc in cubes)
            cc.transform.parent = this.transform;
        // disable main collider and script at end
        bc.enabled = false;
        this.enabled = false;
    }
}

public enum DicotilerMode
{
    Pattern, Random

}
