using UnityEngine;

public class BuildingGrid {

    private bool[,] Grid;

    float Scale;

    int width { get { return Grid.GetLength(0); } }
    int length { get { return Grid.GetLength(1); } }

    public BuildingGrid(int _width, int _length, float _Scale)
    {
        Grid = new bool[_width, _length];
        Scale = _Scale;
        PopulateGrid(true);
    }

    private void PopulateGrid(bool PopulationValue)
    {
        for (int w = 0; w < Grid.GetLength(0); w++)
            for (int l = 0; l < Grid.GetLength(1); l++)
                Grid[w,l] = PopulationValue;
    }

    public bool CheckGrid(Vector2 location) {
        return Grid[Mathf.RoundToInt(location.x), Mathf.RoundToInt(location.y)];
    }

    public bool CheckGrid(Vector2 location, Vector2 Size)
    {
        if (!CheckBounds(location) || !CheckBounds(location + Size))
            return false;

        for (int w = 0; w < Mathf.RoundToInt(Size.x); w++)
        {
            for (int l = 0; l < Mathf.RoundToInt(Size.y); l++)
            {
                Vector2 CheckPoint = location + new Vector2(w, l);
                if (Grid[Mathf.RoundToInt(CheckPoint.x), Mathf.RoundToInt(CheckPoint.y)] == false)
                    return false;
            }
        }

        return true;
    }

    public bool UpdateGrid(Vector2 location)
    {
        return UpdateGrid(location, Vector2.one);
    }

    public bool UpdateGrid(Vector2 location, Vector2 Size)
    {
        if (!CheckGrid(location, Size)) return false;

        Debug.Log("Checking Out");

        for (int w = 0; w < Mathf.RoundToInt(Size.x); w++)
        {
            for (int l = 0; l < Mathf.RoundToInt(Size.y); l++)
            {
                Vector2 CheckPoint = location + new Vector2(w, l);
                Grid[Mathf.RoundToInt(CheckPoint.x), Mathf.RoundToInt(CheckPoint.y)] = false;
            }
        }

        return true;
    }

    bool CheckBounds(Vector2 location)
    {
        if (location.x > Grid.GetLength(0)) return false;
        if (location.y > Grid.GetLength(1)) return false;
        if (location.x < 0) return false;
        if (location.y < 0) return false;

        return true;
    }

    public void DebugGizmoDisplay(Vector3 origin)
    {
        for (int w = 0; w < Grid.GetLength(0); w++)
        {
            for (int l = 0; l < Grid.GetLength(1); l++)
            {
                Gizmos.color = Grid[w, l] ? Color.green : Color.red ;
                Gizmos.DrawCube(origin + new Vector3(w * Scale, -.2f, l * Scale) + new Vector3(.5f * Scale,0, .5f * Scale), new Vector3(Scale, 0.4f, Scale) * 0.9f);
            }
        }
    }
}
