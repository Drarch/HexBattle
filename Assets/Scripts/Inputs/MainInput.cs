using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class MainInput : MonoBehaviour
{
    protected HexTile selected;
    protected Vector3 PointerLastPosition;

    void Start()
    {
        selected = null;
        Camera.main.transform.LookAt(Vector3.zero);

        #if UNITY_STANDALONE_WIN
            SetupPC();
        #endif

        #if UNITY_ANDROID
            SetupAndroid();
        #endif
    }

    void FixedUpdate()
    {
        GameObject pieces = GameObject.Find("Pieces");

        if (pieces != null)
        {
            foreach (Piece p in pieces.GetComponentsInChildren<Piece>())
            {
                if (p.IsMoving)
                {
                    float percentageComplete = (Time.time - p.MoveTime) / 1.0f;

                    p.transform.position = Vector3.Lerp(p.MoveFrom, p.MoveTo, percentageComplete);

                    if (percentageComplete >= 1.0f)
                    {
                        p.IsMoving = false;
                    }
                }
            }
        }
    }

    private void SetupPC()
    {
        foreach (MonoBehaviour c in this.GetComponents<MonoBehaviour>())
        {
            if (c is MouseGameController) c.enabled = true;
        }
    }

    private void SetupAndroid()
    {
        foreach (MonoBehaviour c in this.GetComponents<MonoBehaviour>())
        {

        }
    }

    private void CheckInputs()
    {

    }

    protected void Select(Vector3 pointerPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            HexTile s = null;

            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Tiles"))
            {
                s = hit.collider.GetComponentInParent<HexTile>();
            }
            else if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Pieces"))
            {
                s = hit.collider.GetComponent<Piece>().Tile;
            }
            else
            {
                throw new Exception("You click something new?!");
            }

            if (selected != null && selected.OcuppiedBy != null)
            {
                Piece p = selected.OcuppiedBy;

                switch (s.State)
                {
                    case HexTile.eState.Moveable:
                        Vector3 start = p.transform.position;

                        StartMove(p, p.transform.position, s.transform.position);

                        s.OcuppiedBy = p;
                        p.Tile = s;
                        selected.OcuppiedBy = null;
                        ClearSelection();
                        break;
                    case HexTile.eState.Atackable:
                        p.Attack(s.OcuppiedBy);
                        ClearSelection();
                        break;
                    case HexTile.eState.NotSelected:
                        if (s.OcuppiedBy != null)
                            SelectTile(s);
                        else
                            ClearSelection();
                        break;
                }
            }
            else if (s.OcuppiedBy != null)
            {
                SelectTile(s);
            }
        }
        else
        {
            ClearSelection();
        }
    }

    protected void SelectTile(HexTile tile)
    {
        ClearSelection();

        tile.OcuppiedBy.SelectMap(tile);
        tile.State = HexTile.eState.Selected;
        selected = tile;
    }

    protected void ClearSelection()
    {
        selected = null;
        var tiles = GetComponentsInChildren<HexTile>().Where(x => x.State != HexTile.eState.NotSelected);

        foreach (HexTile t in tiles)
        {
            t.State = HexTile.eState.NotSelected;
        }
    }

    protected void StartMove(Piece p, Vector3 from, Vector3 to)
    {
        p.IsMoving = true;
        p.MoveTime = Time.time;
        p.MoveFrom = from;
        p.MoveTo = to;
    }

    protected void RotateCameraPointer(Vector3 pointerPosition, Camera camera)
    {
        float distance = camera.ScreenToViewportPoint(PointerLastPosition).x - camera.ScreenToViewportPoint(pointerPosition).x;
        camera.transform.RotateAround(Vector3.zero, Vector3.up, distance / Time.deltaTime);
        PointerLastPosition = pointerPosition;
    }

    protected void TiltCameraAxis(float distance, Camera camera)
    {
        if (distance != 0f)
        {
            float change = distance / Time.deltaTime;
            float cameraAngle = camera.transform.rotation.eulerAngles.x + change;

            //if (cameraAngle < 15)
            //{
            //    change = 15 - camera.transform.rotation.eulerAngles.x;
            //}
            //else if (cameraAngle > 87)
            //{
            //    change = 87 - camera.transform.rotation.eulerAngles.x;
            //}

            if (Mathf.Abs(change) > 0.1f)
            {
                camera.transform.RotateAround(Vector3.zero, camera.transform.right, change);
                camera.transform.LookAt(Vector3.zero);
            }
        }
    }
}
