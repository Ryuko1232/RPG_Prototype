using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using System;
using UnityEngine.EventSystems;
using GameDevTV.Inventories;
using UnityEngine.UI;
using InventoryExample.UI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;
        CharacterController controller;
        MouseLook mouseLook;
        bool canMove = true;
        bool inventoryIsOpened = false;

        [SerializeField] GameObject crossHair = null;
        [SerializeField] ShowHideUI inventoryUI;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Sprite image;
            public Vector2 hotspot;
        }

        [SerializeField] float speed = 10f;
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float raycastRadius = 1f;

        private void Awake()
        {
            health = GetComponent<Health>();
            controller = GetComponent<CharacterController>();
            mouseLook = GetComponentInChildren<MouseLook>();

            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update()
        {
            CheckSpecialAbilityKeys();

            if (ToggleInventory())
            {
                return;
            }

            if (InteractWithUI())
            {
                return;
            }
            if (health.IsDead())
            {
                return;
            }
            Movement();
            if (InteractWithComponent())
            {
                return;
            }
            SetCursor(CursorType.None);
            SetCrosshair(CursorType.None);
        }

        private bool ToggleInventory()
        {
            if (Input.GetButtonDown("Inventory"))
            {
                if (!inventoryIsOpened)
                {
                    if (inventoryUI == null)
                    {
                        return false;
                    }

                    inventoryIsOpened = true;
                    inventoryUI.ToggleUI(true);
                    Cursor.lockState = CursorLockMode.None;
                    return true;
                }
                else
                {
                    if (inventoryUI == null)
                    {
                        return false;
                    }

                    inventoryIsOpened = false;
                    inventoryUI.ToggleUI(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    return false;
                }
            } 
            return false;
        }

        private void CheckSpecialAbilityKeys()
        {
            var actionStore = GetComponent<ActionStore>();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                actionStore.Use(0, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                actionStore.Use(1, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                actionStore.Use(2, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                actionStore.Use(3, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                actionStore.Use(4, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                actionStore.Use(5, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                actionStore.Use(6, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                actionStore.Use(7, gameObject);
            }
        }

        private bool InteractWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                SetCrosshair(CursorType.UI);
                mouseLook.enabled = false;
                return true;
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach(IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        SetCrosshair(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            if (canMove)
            {
                return true;
            }
            return false;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];

            for(int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }

            Array.Sort(distances, hits);

            return hits;
        }

        private void Movement()
        {
            if(mouseLook.enabled == false)
            {
                mouseLook.enabled = true;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private void SetCrosshair(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            if(crossHair != null)
            {
                Sprite crossHairSprite = crossHair.GetComponent<Sprite>();
                crossHairSprite = mapping.image;
            }
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
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
