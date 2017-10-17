using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaster : MonoBehaviour {

    float BuildingScale = 1f;

    Vector3 CurrentMousePos = Vector3.zero;

    public GameObject TestBuildingObject;
    BuildingItemController ActiveBuildingController;

    BuildingGrid buildingGrid;
    BuildingGrid bg {
        get { return buildingGrid; }
        set { buildingGrid = value; }
    }

    void Awake()
    {
        bg = new BuildingGrid(10, 10, BuildingScale);
        GameObject activeBuildingController = new GameObject("BuildingItemController");
        ActiveBuildingController = activeBuildingController.AddComponent<BuildingItemController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ActiveBuildingController != null)
            {
                if (ActiveBuildingController.IsPlacingObject)
                {
                    if (bg.UpdateGrid(ActiveBuildingController.GetLocation(), ActiveBuildingController.GetItemSize)) 
                    {
                        ActiveBuildingController.PlaceObject();
                    }
                }
                else
                { 
                    ActiveBuildingController.SetNewBuildItem(TestBuildingObject, BuildingScale, bg);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ActiveBuildingController != null)
            {
                ActiveBuildingController.RotateObject(true);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(bg != null)  
            bg.DebugGizmoDisplay(Vector3.zero);

        CurrentMousePos = MouseUtility.MouseRaycastPosition();
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(CurrentMousePos, 1f);
    }
}
