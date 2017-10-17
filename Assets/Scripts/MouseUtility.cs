using UnityEngine;

public static class MouseUtility {
    public static Vector3 MouseRaycastPosition()
    {
        return MouseRaycastPosition(Physics.AllLayers);
    }

    public static Vector3 MouseRaycastPosition(int mask)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public static Vector3 MouseRaycastPositionRounded(float Scale)
    {
        return MouseRaycastPositionRounded(Scale, Physics.AllLayers);
    }

    public static Vector3 MouseRaycastPositionRounded(float Scale, int mask)
    {
        Vector3 pos = MouseRaycastPosition(mask);

        float x, y, z;
        x = Mathf.Round(pos.x * (1 / Scale)) * Scale;
        y = Mathf.Round(pos.y * (1 / Scale)) * Scale;
        z = Mathf.Round(pos.z * (1 / Scale)) * Scale;

        return new Vector3(x, y, z);
    }
}
