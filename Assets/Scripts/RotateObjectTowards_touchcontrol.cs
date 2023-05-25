using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectTowards_touchcontrol : MonoBehaviour
{
    // public Joystick joystick;

        public bool tochControl = false;    

        private Transform Parent;

        public bool facingRight = true; // направление на старте сцены, вправо?
        private float invert;
        //private Vector3 mouse;


        public float speed = 5f;

        public float minAngle = -40; // ограничение по углам
        public float maxAngle = 40;

        private float angle;

        [HideInInspector]
        public Vector3 mousePosMain;


        private static RotateObjectTowards_touchcontrol instance;

        public static RotateObjectTowards_touchcontrol Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<RotateObjectTowards_touchcontrol>();
                }
                return instance;
            }
        }
        public float ReturnGunAngle()
        {
            return angle;
        }


    public void Initialized()
    {
        mousePosMain = new Vector3(0, 0, -Camera.main.transform.position.z);
        facingRight = true;
        if (!facingRight) invert = -1; else invert = 1;

       
    }

    private void Start()
        {
            Parent = GetComponentInParent<Character>().transform;
            Time.timeScale = 1;
            if (!facingRight) invert = -1; else invert = 1;

        }

        private void Update()
        {

        if (tochControl)
        {
            invert = Mathf.Sign(Parent.localScale.x);

            /////////////////////////////////////////////////////////
            angle = Mathf.Atan2(mousePosMain.y, mousePosMain.x * invert) * Mathf.Rad2Deg;

            ///////////////
            angle = Mathf.Clamp(angle, minAngle, maxAngle);

            if (mousePosMain.y >= -0.2f && mousePosMain.y <= 0.2f)
            {
                angle = 0;
            }
            /////////////////////////////////////////////////

            Quaternion rotation = Quaternion.AngleAxis(angle * invert, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);



            if (mousePosMain.x < -0.2 && facingRight)
            {
                Flip();
            }
            else
            if (mousePosMain.x > 0.2 && !facingRight)
            {
                Flip();
            }
        }

        else
        {
            Vector3 mousePosMain = Input.mousePosition;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(mousePosMain.x, mousePosMain.y, -Camera.main.transform.position.z));

            invert = Mathf.Sign(Parent.localScale.x);


            Vector2 direction = Camera.main.ScreenToWorldPoint(new Vector3(mousePosMain.x, mousePosMain.y, -Camera.main.transform.position.z)) - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x * invert) * Mathf.Rad2Deg;
            ///////////////
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            ///////////////////////////
            Quaternion rotation = Quaternion.AngleAxis(angle * invert, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            /////////////////////////

        }

    }

        void Flip() // отражение по горизонтали
        {
            facingRight = !facingRight;
            Vector3 theScale = Parent.localScale;
            theScale.x *= -1;
            invert *= -1;
            Parent.localScale = theScale;

        }

    

}

