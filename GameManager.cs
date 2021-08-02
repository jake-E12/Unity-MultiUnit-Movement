using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    private Camera cam;
    public List<GameObject> agents;
    private Vector3 pos;
    public Material mat;
    public Material mat2;
    public GameObject box;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int LayerMask = 1 << 8;
        LayerMask = ~LayerMask;

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
            {
                if (hit.collider.tag == "Unit" && !agents.Contains(hit.collider.gameObject))
                {
                    agents.Add(hit.collider.gameObject);
                    hit.collider.GetComponent<Renderer>().material = mat;
                    Debug.Log($"hit collider : {hit.collider.gameObject}, hit position : {hit.transform.position}");
                }
                else if (hit.collider.tag == "Unit" && agents.Contains(hit.collider.gameObject))
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = mat2;
                    agents.Remove(hit.collider.gameObject);
                }
                else if (hit.collider.tag != "Unit")
                {
                    for (var i = 0; i < agents.Count; i++)
                    {
                        agents[i].GetComponent<Renderer>().material = mat2;
                    }
                    agents.Clear();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
            {
                for (var i = 0; i < agents.Count; i++)
                {
                    agents[i].GetComponent<NavMeshAgent>().SetDestination(hit.point);
                    agents[i].GetComponent<Renderer>().material = mat2;
                }
                agents.Clear();
            }
        }

    }
}
