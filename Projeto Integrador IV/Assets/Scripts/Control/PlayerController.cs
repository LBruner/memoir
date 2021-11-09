using System;
using System.Collections.Generic;
using RPG.Dialogue;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;

        private void Update()
        {
            if (GetComponent<PlayerConversant>().IsTalking())
            {
                SetCursor(CursorType.Chatting);
                return;
            }

            if (InteractWithComponent())
            {
                return;
            }

            if (InteractWithUI())
            {
                return;
            }

            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
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

        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), 1f);
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
