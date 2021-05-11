using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Management : MonoBehaviour
{
    public List<SelectableObject> listOfSelected;

    public SelectableObject hovered;
    public Image frameImage;
    public Camera mainCamera;
    
    Vector2 frameStart;
    Vector2 frameEnd;

    private void Start()
    {
        Resources.UpdateMoneyText();
    }

    void Update()
    { 
        bool cursorOnUIElement = EventSystem.current.IsPointerOverGameObject();

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
                SelectableObject obj = hit.collider.GetComponent<SelectableCollider>().selectableObject;
                if (hovered)
                {
                    if (hovered != obj)
                    {
                        UnhoverCurrent();
                        hovered = obj;
                        hovered.OnHover();
                    }
                }
                else
                {
                    hovered = obj;
                    hovered.OnHover();
                }
            }
            else
            {
                UnhoverCurrent();
            }
        }
        else
        {
            UnhoverCurrent();
        }
        

        if (Input.GetMouseButtonUp(1))
        {
            if (hit.collider.tag == "Ground")
            {
                foreach (var selected in listOfSelected)
                    if (selected) selected.WhenClickOnGround(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(0) && !cursorOnUIElement)
        {
            frameImage.enabled = true;
            frameStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && !cursorOnUIElement)
        {
            frameEnd = Input.mousePosition;
            Vector2 min = Vector2.Min(frameStart, frameEnd);
            Vector2 max = Vector2.Max(frameStart, frameEnd);
            frameImage.rectTransform.anchoredPosition = min;
            Vector2 size = max - min;
            frameImage.rectTransform.sizeDelta = size;
            if (!Input.GetKey(KeyCode.LeftControl) ) 
                UnselectAll();
            Rect rect = new Rect(min, size);
            Unit[] allUnits = FindObjectsOfType<Unit>();
            foreach (Unit unit in allUnits)
            {
                if (rect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)))
                {
                    Select(unit);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!Input.GetKey(KeyCode.LeftControl) && Vector2.Distance(frameEnd, frameStart) < 10 && !cursorOnUIElement)
                UnselectAll();
            frameImage.enabled = false;
            if (hovered)
            {
                Select(hovered);
            }
        }

    }

    private void Select(SelectableObject obj)
    {
        if (!listOfSelected.Contains(obj))
        {
            
            listOfSelected.Add(obj);
            obj.Select();
        }
    }

    private void Unselect(SelectableObject obj)
    {
        if (listOfSelected.Contains(obj))
        {

            listOfSelected.Remove(obj);
            obj.Unselect();
        }
    }

    private void UnhoverCurrent()
    {
        if (hovered)
        {
            hovered.OnUnhover();
            hovered = null;
        }
    }

    public void UnselectAll()
    {
        foreach (var selected in listOfSelected)
            if (selected) selected.Unselect();
        listOfSelected.Clear();
    }

}
