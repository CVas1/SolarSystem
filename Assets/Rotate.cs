using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 35f;
    [SerializeField] private GameObject trailPrefab;

    public GameObject parent;
    private float distance;

    public int miliseconds;

    List<GameObject> list = new List<GameObject>();

    void Start()
    {

        //parent = transform.parent.gameObject;
        rotateSpeed = UnityEngine.Random.Range(10, 70);
        distance = (parent.transform.position - transform.position).magnitude;

        miliseconds = (int)(200 - rotateSpeed * 2);

        if(trailPrefab != null) { 
            Observable.Interval(TimeSpan.FromMilliseconds(miliseconds))
                .Subscribe(_ => InstantiateCube())
                .AddTo(this);
        }
    }

    private void InstantiateCube()
    {
        GameObject obj =  Instantiate(trailPrefab, transform.position, Quaternion.identity);
        list.Add(obj);
        if(list.Count > 25) { Destroy(list[0]); list.RemoveAt(0); }
    }

    void Update()
    {
        transform.RotateAround(parent.transform.position, Vector3.up ,rotateSpeed * Time.deltaTime);
        
    }


    private Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(parent.transform.position, distance);
    }
}
