using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMaterialOffset : MonoBehaviour
{
    public float speed = 2;
    private Vector2 currentOffset;
    private Vector2 rand;
    private MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rand = Random.insideUnitCircle + Vector2.one/3;
        rand.Normalize();
        renderer.material.SetTextureOffset("_MainTex", currentOffset + speed * rand* Time.deltaTime);
        currentOffset +=  rand * speed * Time.deltaTime;
    }
}
