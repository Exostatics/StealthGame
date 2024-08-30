
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalController : MonoBehaviour
{

    // Variables
    // Metal Class
    public Metals metals;

    public string activeMetal = "Steel";

    public GameObject lineRenderr;

    public List<GameObject> lineRendererrrs;

    public float distanceMax = 30f;

    public GameObject linePrefab;
    // Start is called before the first frame update
    void Start()
    {
        var metals = GameObject.FindGameObjectsWithTag("Metal");

        for (int i = 0; i < metals.Length; i++)
        {

            GameObject line = Instantiate(lineRenderr);
            lineRendererrrs.Add(line);

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (activeMetal)
        {
            case "Steel":
                var metals = GameObject.FindGameObjectsWithTag("Metal");

                for (int i = 0; i < metals.Length; i++)
                {
                    float dist = Vector3.Distance(metals[i].gameObject.transform.position, transform.position);

                    if (dist < distanceMax)
                    {
                        lineRendererrrs[i].GetComponent<LineRenderer>().SetPosition(0, transform.position);
                        lineRendererrrs[i].GetComponent<LineRenderer>().SetPosition(1, metals[i].gameObject.transform.position);
                        lineRendererrrs[i].SetActive(true); // error

                        if (Input.GetMouseButtonDown(0))
                        {
                            transform.position = Vector3.Lerp(transform.position, metals[i].transform.position, 500f * Time.deltaTime);
                        }

                    }
                    else
                    {
                        lineRendererrrs[i].SetActive(false);
                    }

                }

                break;
            case "Iron":
                break;
        }
    }

}

