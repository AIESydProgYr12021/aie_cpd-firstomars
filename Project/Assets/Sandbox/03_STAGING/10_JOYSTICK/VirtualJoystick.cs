using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sandbox.Staging.Joystick
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("Joystick Movement")]
        [SerializeField] private float joystickVisualDistance = 50;

        [Header("Logic")]
        private Image container;
        private Image joystick;
        private Vector3 direction;
        public Vector2 Direction { get; private set; }

        [Header("Toggle Controls")]
        public bool showControl = true;
        public bool syncWithKeyboardInput = true;
        private bool isControllsShowing = true;
        private bool isDragging = false;       


        // Start is called before the first frame update
        void Start()
        {
            Image[] imgs = GetComponentsInChildren<Image>();
            container = imgs[0];
            joystick = imgs[1];
        }

        private void Update()
        {
            if (showControl != isControllsShowing)
            {
                container.enabled = showControl;
                joystick.enabled = showControl;
                isControllsShowing = showControl;
            }
            
            if (syncWithKeyboardInput && !isDragging)
            {
                Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                
                joystick.rectTransform.anchoredPosition = new Vector3
                    (Direction.x * joystickVisualDistance, 
                    Direction.y * joystickVisualDistance, 
                    0);
            }
        }

        //when you drag object around on canvas
        public virtual void OnDrag(PointerEventData ped)
        {
            isDragging = true;
            Vector2 pos = Vector2.zero;

            //bool + adds value to pos
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(container.rectTransform, ped.position, ped.pressEventCamera, out pos))
                return;
            pos.x = (pos.x / container.rectTransform.sizeDelta.x);
            pos.y = (pos.y / container.rectTransform.sizeDelta.y);

            //adjust pivot offset
            Vector2 p = container.rectTransform.pivot;
            pos.x += p.x - 0.5f;
            pos.y += p.y - 0.5f;

            //clamp values         
            pos.x = Mathf.Clamp(pos.x, -1, 1);
            pos.y = Mathf.Clamp(pos.y, -1, 1);

            //direction = new Vector3(pos.x, 0, pos.y).normalized;
            Direction = pos;
            Debug.Log(direction);

            joystick.rectTransform.anchoredPosition = new Vector3
                (pos.x * joystickVisualDistance,
                pos.y * joystickVisualDistance, 0);
        }

        //mouse click held down
        public virtual void OnPointerDown(PointerEventData ped)
        {
            OnDrag(ped); //points to on drag
        }

        //mouse click release
        public virtual void OnPointerUp(PointerEventData ped)
        {
            direction = default(Vector3); //put joystick back on zero
            joystick.rectTransform.anchoredPosition = default(Vector3);
            isDragging = false;
        }
    }
}