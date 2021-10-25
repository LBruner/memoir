﻿using System;
using System.Collections.Generic;
using RPG.Dialogue;
using RPG.Map;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover = null;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance = 1f;
        [SerializeField] float raycastRadius = 1f;

        void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (GetComponent<PlayerConversant>().IsTalking())
            {
                return;
            }


            if (InteractWithComponent())
            {
                return;
            }

            //  if (InteractWithMovement()) { return; }


            if (InteractWithUI())
            {
                return;
            }

            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            // List<RaycastResult> hits = RaycastScreenElements();

            // if (hits.Count > 0)
            // {
            //     foreach (var go in hits)
            //     {
            //         if (go.gameObject.GetComponent<AIConversant>() || go.gameObject.GetComponent<MapQuest>())
            //         {
            //             InteractWithComponent();
            //             return false;
            //         }
            //     }
            // }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            List<RaycastResult> hits = RaycastScreenElements();

            if (hits.Count > 0)
            {
                foreach (RaycastResult hit in hits)
                {
                    IRaycastable[] raycastables = hit.gameObject.GetComponents<IRaycastable>();

                    foreach (IRaycastable raycastable in raycastables)
                    {
                        if (raycastable.HandleRaycast(this))
                        {
                            SetCursor(raycastable.GetCursorType());
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static List<RaycastResult> RaycastScreenElements()
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, hits);
            return hits;
        }

        // private bool InteractWithMovement()
        // {
        //     Vector3 target;
        //     bool hasHit = RaycastNavMesh(out target);

        //     if (hasHit)
        //     {
        //         if (!GetComponent<Mover>().CanMoveTo(target)) { return false; }

        //         if (Input.GetMouseButton(0))
        //             mover.StartMoveAction(target);

        //         SetCursor(CursorType.Movement);
        //         return true;
        //     }
        //     return false;
        // }

        // private float GetPathLenght(NavMeshPath path)
        // {
        //     float total = 0;

        //     if (path.corners.Length < 2) return total;

        //     for (int i = 0; i < path.corners.Length - 1; i++)
        //     {
        //         total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        //     }
        //     return total;
        // }

        // private bool RaycastNavMesh(out Vector3 target)
        // {
        //     target = new Vector3();

        //     RaycastHit hit;
        //     bool hashit = Physics.Raycast(GetMouseRay(), out hit);

        //     if (!hashit) return false;
        //     NavMeshHit navMeshHit;

        //     bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);

        //     if (!hasCastToNavMesh) return false;

        //     target = navMeshHit.position;

        //     return true;
        // }

        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];

            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, new Vector2(0, 0), CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
