using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DiscoTiler : MonoBehaviour
{
    private BoxCollider col;
    public GameObject baseTile;
    public int Rows = 2, Colums = 2;
    public List<Color> colorsToLoopThrough;

    private List<MeshRenderer> childrenMeshRenderers = new List<MeshRenderer>();
    private GonzakoUtils.DataStructures.Pool<GameObject> pool;

    private Vector3 gridPos;
    private Vector3 oddCheck;
    private Vector3 tileSize;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        pool = new GonzakoUtils.DataStructures.Pool<GameObject>(Rows * Colums, baseTile, this.transform);

        fillCollider(Rows, Colums);
        foreach(MeshRenderer n in childrenMeshRenderers)
        {
            setRandomColorOnlist(n);
        }
    }

    private void fillCollider(int rows, int colums)
    {
        tileSize.x = col.size.x/rows;
        tileSize.y = col.size.y;
        tileSize.z = col.size.z/colums;

        

        oddCheck.x = rows % 2 == 1 ? 0f : 0.5f;
        oddCheck.y = 0;
        oddCheck.z = colums % 2 == 1 ? 0f : 0.5f;

        int listIndex = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < colums; j++)
            {
                var newTile = pool.getNextObj();
                newTile.transform.localScale = tileSize;
                newTile.transform.localPosition = new Vector3(
                ((i - rows/2 + 1) * tileSize.x + col.center.x),
                col.center.y,
                ((j + 1f - colums/2) * tileSize.z + col.center.z));

                childrenMeshRenderers.Add(newTile.GetComponentInChildren<MeshRenderer>());
                newTile.SetActive(true);
            }
        }
    }

    private void setRandomMaterialColor(MeshRenderer renderer)
    {
        MaterialPropertyBlock newMatPropBlock = new MaterialPropertyBlock();
        newMatPropBlock.SetColor("_Color", Random.ColorHSV());
        renderer.SetPropertyBlock(newMatPropBlock);
    }

    private void setRandomColorOnlist(MeshRenderer renderer)
    {
        MaterialPropertyBlock newMatPropBlock = new MaterialPropertyBlock();
        newMatPropBlock.SetColor("_Color", colorsToLoopThrough[Random.Range(0, colorsToLoopThrough.Count)]);
        renderer.SetPropertyBlock(newMatPropBlock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum DicotilerMode
{
    Pattern, Random

}
