﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        if (planeForWalk)
            PlaneVertices = planeForWalk.GetComponent<MeshFilter>()?.sharedMesh.vertices;
    }

    public GameObject planeForWalk;
    internal Vector3[] PlaneVertices;

    public bool hasGameStarted;
    
    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        if (Bank.Instance.money >= 100 && Bank.Instance.food >= 1000)
        {
            go.SetActive(true);
        }
    }
}