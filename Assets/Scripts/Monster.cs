using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> vertObjects;
    public List<GameObject> caveTargets;
    private List<int> listOfTargets = new List<int>();
    private GameObject closestTarget;
    public float monsterSpeed = 7;
    float dist;
    float closestDist;
    
    public int numVerts;
    private List<Tuple<int, int>>[] adjacencyList;
    private int[] distances;
    private bool[] visited;
    private int[] predecessors;
    public int endVert;
    public int startVert;


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
        predecessors = new int[numVerts];

        AddEdge(0, 7, 1);
        AddEdge(1, 8, 1);
        AddEdge(1, 9, 1);
        AddEdge(2, 10, 1);
        AddEdge(2, 11, 1);
        AddEdge(2, 13, 1);
        AddEdge(3, 14, 1);
        AddEdge(3, 15, 1);
        AddEdge(4, 12, 1);
        AddEdge(5, 16, 1);
        AddEdge(5, 17, 1);
        AddEdge(6, 18, 1);
        AddEdge(7, 8, 1);
        AddEdge(9, 10, 1);
        AddEdge(11, 12, 1);
        AddEdge(13, 14, 1);
        AddEdge(15, 16, 1);
        AddEdge(17, 18, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startVert = vertObjects.IndexOf(FindClosestPointTo(gameObject));
            endVert = vertObjects.IndexOf(FindClosestPointTo(player));
            if (startVert == endVert)
            {
                //kill
                Debug.Log("die");
            }
            else
            {
                listOfTargets = Dijkstra(startVert, endVert);
            }
        }

        if (listOfTargets.Count > 0)
        {
            GameObject currentTarget = vertObjects[listOfTargets[listOfTargets.Count - 1]];
            transform.Translate(Vector3.Normalize(currentTarget.transform.position - transform.position) * monsterSpeed * Time.deltaTime);

            if (Vector3.Distance(currentTarget.transform.position, gameObject.transform.position) < 1)
            {
                listOfTargets.RemoveAt(listOfTargets.Count - 1);
            }
        }
        
    }

    public void AddEdge(int u, int v, int weight)
    {
        adjacencyList[u].Add(new Tuple<int, int>(v, weight));
        adjacencyList[v].Add(new Tuple<int, int>(u, weight));
    }

    public List<int> Dijkstra(int source, int end)
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

            if (u == end)
            {
                return GetPath(predecessors, source, end); ;
            }

            foreach (var neighbour in adjacencyList[u])
            {
                int v = neighbour.Item1;
                int weight = neighbour.Item2;

                if (!visited[v] && distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                {
                    distances[v] = distances[u] + weight;
                    predecessors[v] = u;
                }
            }
        }
        return GetPath(predecessors, source, end); ;
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

    public List<int> GetPath(int[] predecessors, int source, int end)
    {
        List<int> path = new List<int>();
        int current = end;
        while (current != source)
        {
            path.Add(current);
            current = predecessors[current];
        }
        path.Add(current);

        /*for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(path[i]);
        }*/
        return path;
    }

    public GameObject FindClosestPointTo(GameObject sourceObj)
    {
        for (int i = 0; i < caveTargets.Count; i++)
        {
            dist = Vector3.Distance(sourceObj.transform.position, caveTargets[i].transform.position);

            if (i == 0)
            {
                
                closestDist = dist;
                closestTarget = vertObjects[i];
            }
            else
            {
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestTarget = vertObjects[i];
                }
            }
        }

        return closestTarget;
    }
}
