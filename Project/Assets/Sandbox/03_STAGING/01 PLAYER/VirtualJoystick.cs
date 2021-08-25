using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

//match directory
namespace SandBox.Staging.Player
{

    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [Header("Tweaks")]
        [SerializeField] private float joystickVisualDistance = 25;

        [Header("Logic")]
        private Image container;
        private Image joystick;

        private Vector3 direction;
        public Vector3 Direction { get { return direction; } }

        public UnityEvent<Vector2> OnJoystickMove;

        private bool isDragHandeling = false;
        //private bool isJoystickVisible = true;

        public bool showVirtualControls = true;
        public bool ShowVirtualControls
        {
            get { return showVirtualControls; }
            set
            {
                GetComponentsInChildren<Image>()
                    .ToList()
                    .ForEach(img => img.enabled = value);
                showVirtualControls = value;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            Image[] imgs = GetComponentsInChildren<Image>();
            container = imgs[0];
            joystick = imgs[1]; //isn't there only 1 image?


            // sync our property with the value we have from inspector
            ShowVirtualControls = showVirtualControls;

            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                ShowVirtualControls = true;
            }

        }

        public virtual void OnDrag(PointerEventData ped)
        {
            isDragHandeling = true;

            //setting up return value
            Vector2 pos = Vector2.zero;

            //what is the rectangle?? the joystick background?

            //checking if mouse pointer is inside a certain rectangle                           //input mousePos                    //store mouse pos in out pos
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(container.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x = (pos.x / container.rectTransform.sizeDelta.x);
                pos.y = (pos.y / container.rectTransform.sizeDelta.y);

                //pivot? what is this referring to ? what the joystick pivots around on the controller

                //pivot might give offset, adjust here
                //Vector2 refPivot = new Vector2(0.5f, 0.5f);
                Vector2 p = container.rectTransform.pivot;
                pos.x += p.x - 0.5f;
                pos.y += p.y - 0.5f;

                //clamp values
                float x = Mathf.Clamp(pos.x, -1, 1);
                float y = Mathf.Clamp(pos.y, -1, 1);

                direction = new Vector3(x, 0, y);
                //Debug.Log(direction);

                joystick.rectTransform.anchoredPosition = new Vector3(direction.x * joystickVisualDistance, direction.z * joystickVisualDistance);

                OnJoystickMove?.Invoke(new Vector2(direction.x, direction.z));
            }
        }

        public virtual void OnPointerDown(PointerEventData ped)
        {
            OnDrag(ped);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            //when pointer is released, it will return to 0
            direction = default(Vector3);
            joystick.rectTransform.anchoredPosition = default(Vector3);

            isDragHandeling = false;
        }

        public void Update()
        {
            if (isDragHandeling == false)
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");

                direction = new Vector3(x, 0, y);
                joystick.rectTransform.anchoredPosition = new Vector3(direction.x * joystickVisualDistance, direction.z * joystickVisualDistance);

                OnJoystickMove?.Invoke(new Vector2(direction.x, direction.z));
            }

        }
    }
}
