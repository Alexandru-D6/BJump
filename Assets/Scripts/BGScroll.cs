using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float speed = 4f;
    private Vector3 StartPosition;

    [SerializeField] GameObject fondoPantalla;
    [SerializeField] GameObject platform;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        generateProceduralWorld(transform.Find("newPlatforms").gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(translation: Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -13.50f)
        {
            transform.position = StartPosition;
            movePlatforms();
        }
    }

    void generateProceduralWorld(GameObject newP)
    {
        for (int i = 0; i < 14; ++i)
        {
            if (i % (int)Random.Range(3, 4) == 0)
            {
                GameObject p = Instantiate(platform, new Vector3(0, 0, -1), new Quaternion());
                p.transform.parent = newP.transform;

                p.transform.localPosition = new Vector3(Random.Range(-4f, 4f), (float)(i) - 1.5f + (Random.value - 0.5f), -1);
            }
        }
    }

    void movePlatforms()
    {
        GameObject oldP = transform.Find("oldPlatforms").gameObject;
        GameObject newP = transform.Find("newPlatforms").gameObject;

        Destroy(oldP);

        newP.transform.Translate(new Vector3(0, -13.5f, 0), Space.World);
        newP.name = "oldPlatforms";
        oldP = newP;

        newP = new GameObject("newPlatforms");
        newP.transform.localPosition = new Vector3(0, 8.5f, 0);
        newP.transform.parent = transform;

        generateProceduralWorld(newP);
    }
}
