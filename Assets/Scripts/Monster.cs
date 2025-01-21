using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public GameObject[] targets;
    public GameObject closestTarget;
    public float monsterSpeed = 7;
    float dist;
    float closestDist;

    public int numVerts;
    public GameObject[] vertObjects;
    private List<Tuple<int, int>>[] adjacencyList;
    public int[] distances;
    public bool[] visited;
    public int endVert;


    // Start is called before the first frame update
    void Start()
    {
        adjacencyList = new List<Tuple<int, int>>[numVerts];

        for (int i = 0; i < numVerts; i++)
        {
            adjacencyList[i] = new List<Tuple<int, int>>();
        }

        distances = new int[numVerts];
        visited = new bool[numVerts];

        AddEdge(0, 1, 10);
        AddEdge(0, 4, 5);
        AddEdge(1, 2, 1);
        AddEdge(1, 4, 2);
        AddEdge(2, 3, 4);
        AddEdge(3, 0, 7);
        AddEdge(3, 2, 6);
        AddEdge(4, 1, 3);
        AddEdge(4, 2, 9);
        AddEdge(4, 3, 2);

        Dijkstra(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClosesetTarget(); 
        }

        transform.Translate(Vector3.Normalize(closestTarget.transform.position - transform.position) * monsterSpeed * Time.deltaTime);
    }

    public void AddEdge(int u, int v, int weight)
    {
        adjacencyList[u].Add(new Tuple<int, int>(v, weight));
    }

    public void Dijkstra(int source)
    {
        for (int i = 0; i < numVerts; i++)
        {
            distances[i] = int.MaxValue;
            visited[i] = false;
        }
        distances[source] = 0;

        for (int count = 0; count < numVerts - 1; count++)
        {
            int u = MinDistance(distances, visited);
            visited[u] = true;

            foreach (var neighbour in adjacencyList[u])
            {
                int v = neighbour.Item1;
                int weight = neighbour.Item2;

                if (!visited[v] && distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                {
                    distances[v] = distances[u] + weight;
                }
            }
        }
    }

    public int MinDistance(int[] distances, bool[] visited)
    {
        int min = int.MaxValue;
        int minIndex = -1;

        for (int v = 0; v < distances.Length; v++)
        {
            if (distances[v] <= min && !visited[v])
            {
                min = distances[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    public void ClosesetTarget()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            dist = Vector3.Distance(player.transform.position, targets[i].transform.position);

            if (i == 0)
            {
                
                closestDist = dist;
                closestTarget = targets[i];
            }
            else
            {
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestTarget = targets[i];
                }
            }
        }
    }
}
