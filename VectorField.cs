using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VectorField
{
    public Vector2[,] Vectors;

    public Node[,] FeildNodes;

    public void GenerateField(int _mapWidth, int _MapHeight, int DesX, int DesY)
    {
        FeildNodes = new Node[_mapWidth, _MapHeight];
        Vectors = new Vector2[_mapWidth, _MapHeight];


        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapWidth; y++)
            {
                FeildNodes[x, y] = new Node();
                FeildNodes[x, y].gridX = x;
                FeildNodes[x, y].gridY = y;
            }
        }

        Update(_mapWidth, _MapHeight, DesX, DesY);
    }

    public void Update(int _mapWidth, int _MapHeight, int DesX, int DesY)
    {
        GenerateCostmap(FeildNodes[DesX, DesY], _mapWidth, _MapHeight);
        GetVectorField(FeildNodes[DesX, DesY], _mapWidth, _MapHeight);

        for(int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapWidth; y++)
            {
                Vectors[x, y] = FeildNodes[x, y].Vectorfield;
            }
        }
    }


    public void GenerateCostmap(Node _destination, int _mapWidth, int _mapHeight)
    {
        Queue<Node> Frontier = new Queue<Node>();
        List<Node> Marked = new List<Node>();

        foreach (Node node in FeildNodes)
        {
            if (node.Passable == false)
            {
                node.PathDistance = 200;
            }
            else
            {
                node.PathDistance = 0;
            }
            node.Parent = null;
        }

        _destination.PathDistance = 0;
        Frontier.Enqueue(_destination);
        Marked.Add(_destination);

        while (Frontier.Count > 0)
        {
            Node Current = Frontier.Dequeue();

            List<Node> NeighbouringNodes = GetNSEWNeighbouringNodes(Current, _mapHeight, _mapWidth);

            foreach (Node _node in NeighbouringNodes)
            {

                if (Marked.Contains(_node) || _node.Passable == false)
                {
                    continue;
                }

                _node.PathDistance = _node.PathDistance + (Current.PathDistance + 1 + _node.Weight);
                _node.Parent = Current;
                Frontier.Enqueue(_node);
                Marked.Add(_node);
            }
        }
    }

    public void GetVectorField(Node _DistinationNode, int _mapWidth, int _mapHeight)
    {
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                if (FeildNodes[x, y].Passable && FeildNodes[x, y] != _DistinationNode)
                {
                    Vector3 CheapsetNode = GetCheapestNodeDirection(FeildNodes[x, y], _mapWidth, _mapHeight);
                    FeildNodes[x, y].Vectorfield.x = CheapsetNode.x;
                    FeildNodes[x, y].Vectorfield.y = CheapsetNode.y;
                }
                else
                {
                    FeildNodes[x, y].Vectorfield = Vector2.zero;
                }
            }
        }
    }


    private List<Node> GetNeighbouringNodes(Node _node, int _width, int _heigth)
    {
        List<Node> Neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int CheckX = _node.gridX + x;
                int CheckY = _node.gridY + y;

                if (CheckX >= 0 && CheckX < _width && CheckY >= 0 && CheckY < _heigth)
                {
                    Neighbours.Add(FeildNodes[CheckX, CheckY]);
                }
            }
        }

        return Neighbours;
    }

    private Vector3 GetCheapestNodeDirection(Node _node, int _width, int _heigth)
    {
        Node CheapestNode = new Node();
        CheapestNode.PathDistance = 200;
        Vector3 ReturnVector = new Vector3();
        List<Node> NeighbouringNode = GetNeighbouringNodes(_node, _width, _heigth);

        foreach (Node _temp in NeighbouringNode)
        {
            if (_temp.PathDistance < CheapestNode.PathDistance)
            {
                CheapestNode = _temp;
            }
        }

        ReturnVector = new Vector3(CheapestNode.gridX - _node.gridX, CheapestNode.gridY - _node.gridY, 0);

        return ReturnVector;
    }

    private List<Node> GetNSEWNeighbouringNodes(Node _node, int _width, int _heigth)
    {
        List<Node> Neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            if (x == 0)
            {
                continue;
            }

            int CheckX = _node.gridX + x;

            if (CheckX >= 0 && CheckX < _width)
            {
                Neighbours.Add(FeildNodes[CheckX, _node.gridY]);
            }
        }

        for (int y = -1; y <= 1; y++)
        {
            if (y == 0)
            {
                continue;
            }

            int CheckY = _node.gridY + y;

            if (CheckY >= 0 && CheckY < _heigth)
            {
                Neighbours.Add(FeildNodes[_node.gridX, CheckY]);
            }
        }

        return Neighbours;
    }
}
