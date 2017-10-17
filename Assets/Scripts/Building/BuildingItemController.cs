using UnityEngine;

public class BuildingItemController : MonoBehaviour{

    GameObject OriginalItemToPlace;
    GameObject ItemToPlace;
    public Vector2 GetItemSize { get { return CalculateBounds(ItemToPlace); } }

    float SavedRotation = 0;

    public bool IsPlacingObject { get { return ItemToPlace != null; } }
    float Scale;
    public LayerMask Mask;
    BuildingGrid grid;

    void Start()
    {
        Mask = ~(1 << LayerMask.NameToLayer("PlacingObject"));
    }

	public void SetNewBuildItem(GameObject _ItemToPlace, float _Scale, BuildingGrid _Grid)
    {
        OriginalItemToPlace = _ItemToPlace;
        ItemToPlace = Instantiate(_ItemToPlace);
        SetLayerOnChildren(ItemToPlace, LayerMask.NameToLayer("PlacingObject"));
        Scale = _Scale;
        grid = _Grid;
        SavedRotation = 0;
    }

    void SetLayerOnChildren(GameObject SetObject, LayerMask Layer)
    {
        ItemToPlace.layer = Layer;
        foreach(Transform t in SetObject.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.layer = Layer;
        }
    }

    void Update()
    {
        if (ItemToPlace != null)
        {
            ItemToPlace.transform.position = MouseUtility.MouseRaycastPositionRounded(Scale, Mask);
            VisualUpdate();
        }
    }

    void VisualUpdate()
    {
        Color MatColor = grid.CheckGrid(GetLocation(), GetItemSize) ? Color.green : Color.red;
        ItemToPlace.GetComponentInChildren<Renderer>().material.color = MatColor;
    }

    public void RotateObject(bool ClockWise)
    {
        if (ItemToPlace == null)
            return;

        float Rotation = ClockWise ? 90f : -90f;
        SavedRotation += Rotation;
        ItemToPlace.transform.Rotate(Vector3.up, Rotation);
    }

    Vector2 CalculateBounds(GameObject CalculationObject)
    {
        Renderer[] children = CalculationObject.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(CalculationObject.transform.position, Vector3.zero);
        foreach (Renderer r in children)
            bounds.Encapsulate(r.bounds);


        //Rotate the bounds
        Vector2 Size = new Vector2(bounds.size.x, bounds.size.z);

        return RotateVector2(Size, SavedRotation);
    }

    Vector2 RotateVector2(Vector2 aPoint, float aDegree)
    {
        return Quaternion.Euler(0, 0, aDegree) * aPoint;
    }

    public Vector2 GetLocation()
    {
        Vector3 MousePos = MouseUtility.MouseRaycastPositionRounded(Scale, Mask);
        return new Vector2(MousePos.x / Scale, MousePos.z / Scale);
    }

    public GameObject PlaceObject()
    {
        GameObject ReturningItem = Instantiate(OriginalItemToPlace);

        ReturningItem.transform.position = ItemToPlace.transform.position;
        ReturningItem.transform.rotation = ItemToPlace.transform.rotation;

        Destroy(ItemToPlace);
        ItemToPlace = null;
        OriginalItemToPlace = null;
        return ReturningItem;
    }

    public void Cleanup()
    {
        Destroy(ItemToPlace);
        ItemToPlace = null;
    }

    
}
