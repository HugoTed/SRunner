using UnityEngine;
using System.Collections;

public class player : Unit
{

    public float runSpeed = 10f;
    public float turnSpeed = 10f;
    public float jumpLong = 8f;
    public float jumpHeight = 5.0f;
    public float camOnWall = 10f;
    //public GameObject cam;
    private float h;
    private float v;
    private float r;
    private float y;
    private Rigidbody rb;
    private bool isOnWall = false;
    private bool Onground = true;
    private bool climbing = false;
    private bool jumping = false;
    private Vector3 wallDirection;
    Vector3 moveDirction;
    private Camera m_camera;
    private Transform m_camTrans;
    private Transform m_chaTrans;
    private Quaternion m_camQutation;
    private Quaternion m_chaQutation;
    private CapsuleCollider m_capsule;
    //一段跳  
    private bool m_jump;
    //二段跳  
    private bool m_jump2;
    // Use this for initialization
    void Start()
    {
        base.Start();
        //max_health = 50;
        Cursor.visible = false;//隐藏鼠标
        m_capsule = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        moveDirction = Vector3.zero;
        wallDirection=new Vector3(0f,0f,0f);
        
        m_camera = Camera.main;
        m_camTrans = m_camera.transform;
        m_chaTrans = transform;

        m_camQutation = m_camTrans.rotation;
        m_chaQutation = m_chaTrans.rotation;
    }
    void Update()
    {
        //视角转动  
        RotateView();
        if (Input.GetButtonDown("Jump"))
        {
            m_jump = true;
        }
        autoClimb();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //h = Input.GetAxis("Horizontal");
        h = Input.GetAxis("Mouse X");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Mouse Y");
        float rightWalk = r * runSpeed;
        float translation = v * runSpeed;
        //transform.Rotate(0, rotation, 0);
        grounded();
        if (true)
        {
            MoveManagement(rightWalk, translation);            
        }

        if (!Onground)
        {
            IsItaWall();
        }
        else
            isOnWall = false;
        JumpManagement();


    }
    void RotateView()
    {
        float xRot = y * turnSpeed ;
        float yRot = h * turnSpeed ;

        //欧拉角使用  
        //{  
        //    m_camRotate += new Vector3(-xRot, 0f, 0f);  
        //需要LocalEulerAnglers，否则摄像机和胶囊体会同时对相机旋转起作用  
        //    m_camTrans.localEulerAngles = m_camRotate;  
        //    m_camRotate += new Vector3(0f, yRot, 0f);  
        //    m_chaTrans.localEulerAngles = m_chaRotate;  
        //}  

        //四元数使用  
        {
            m_camQutation *= Quaternion.Euler(-xRot, 0f, 0f);

            //限制旋转角度在【-90，90】内 
            m_camQutation = ClampRotation(m_camQutation);
            m_camTrans.localRotation = m_camQutation;

            m_chaQutation *= Quaternion.Euler(0f, yRot, 0f);
            m_chaTrans.localRotation = m_chaQutation;
        }
    }
    void MoveManagement(float rightWalk, float translation)
    {
        //transform.Rotate(0, rotationX, 0);
        //transform.Translate(rightWalk * Time.deltaTime, 0, translation * Time.deltaTime);
        if (wallDirection.y == 1 && Input.GetAxis("Fire1")>0)
        {
            transform.Translate(0, translation * 2 * Time.deltaTime, 0);
        }
        //在空上不让后退
        else if(IsInAir())
        {
            translation = 0;
            rightWalk = 0;
        }
        else if (isOnWall)
        {
            rightWalk = 0;
            if (v < 0)
                translation = 0;
        }
        transform.Translate(rightWalk * Time.deltaTime, 0, translation * Time.deltaTime);

        
        //moveDirction = new Vector3(rightWalk, 0, translation);
        //rb.AddForce(transform.TransformDirection(moveDirction));

    }
    
    void JumpManagement()
    {        
        if (Onground || isOnWall)
        {
           
            jumpTime = 0;
            //一段跳  
            if (m_jump)
            {
                JumpUp();               
            }
        }
        else
        {
            if (m_jump)
            {
                jumpTime++;
                //二段跳  
                if (jumpTime < 2)
                {
                   JumpUp();                    
                }
            }
        }

        m_jump = false;
    }
    
    int jumpTime = 0;
    void JumpUp()
    {
        Vector3 jumpDirection = new Vector3(0, 0, 0);
        //防止在墙上原地跳跃或向后跳   
        if (isOnWall)
        {
            if(Mathf.Abs(r)>0 || v > 0)
                jumpDirection = new Vector3(r * jumpLong, jumpHeight, v * jumpLong);
            else
                jumpDirection = new Vector3(0, 0, 0);           
        }
        else
            jumpDirection = new Vector3(r * jumpLong, jumpHeight, v * jumpLong);

        rb.velocity = transform.TransformDirection(jumpDirection);
    }
    void autoClimb()//墙边自动攀爬
    {
        RaycastHit hit;
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y+2f, transform.position.z), transform.forward, Color.red,5f);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 1f)&& 
            !Physics.Raycast(new Vector3(transform.position.x,transform.position.y+2f, transform.position.z), transform.TransformDirection(Vector3.forward),5f))
        {
            climbing = true;
            rb.velocity = transform.TransformDirection(new Vector3(0,7f,0));//在墙边，给一个力，自动推上去            
        }
        if (climbing)
        {
            Debug.Log("ddddd");
            transform.Translate(0, 0, 1f*Time.deltaTime);//推上去后还要向前有位移，才算爬完墙
            //climbing = false;
        }
        if (Onground)
            climbing = false;//确保爬上地面才行


    }
    void IsItaWall()
    {
        float force = 0f;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, m_capsule.radius / 2f, -transform.right, out hit, 2f))
        //if (Physics.Raycast(transform.position, -transform.TransformDirection(Vector3.right), 3f))
        {
            
            force = -10f;          
            wallDirection = new Vector3(-1, 0, 0);
            Debug.Log("wall");
            isOnWall = true;
            //Debug.Log(hit.distance);
        }
        else if (Physics.SphereCast(transform.position, m_capsule.radius / 2f, transform.right, out hit, 2f))
        //else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 3f))
        {
            force = 10f;
            wallDirection = new Vector3(1, 0, 0);
            Debug.Log("wall");
            isOnWall = true;
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 3f))
        {

            wallDirection = new Vector3(0, 1, 0);
            isOnWall = false;
        }
        else
        {
            force = 0f;
            wallDirection = new Vector3(0, 0, 0);
            isOnWall = false;
        }
        if (v < 0)
        {
            force = 0f;
        }
        if (Mathf.Abs(v)>0)
            rb.AddForce(transform.TransformDirection(Vector3.right) * force);
        

        Debug.Log(v);
        //rb.AddForce(transform.TransformDirection(Vector3.right) * force);
    }
    //检查地面
    void grounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, m_capsule.radius / 2f, transform.TransformDirection(Vector3.down), out hit, 1.1f))
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.1f))
        {
            rb.AddForce(transform.TransformDirection(Vector3.down) * 1f);
            //Debug.Log("ground");
            Onground = true;

        }
        else
        {
            Onground = false;
        }
    }
    //不在墙面也不在地面在空中的状态
    bool IsInAir()
    {
        if(Onground == false)
        {
            if (isOnWall == false)
            {
                Debug.Log("air");
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    Quaternion ClampRotation(Quaternion q)
    {
        //四元数的xyzw，分别除以同一个数，只改变模，不改变旋转  
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1;

        /*给定一个欧拉旋转(X, Y, Z)（即分别绕x轴、y轴和z轴旋转X、Y、Z度），则对应的四元数为 
x = sin(Y/2)sin(Z/2)cos(X/2)+cos(Y/2)cos(Z/2)sin(X/2) 
y = sin(Y/2)cos(Z/2)cos(X/2)+cos(Y/2)sin(Z/2)sin(X/2) 
z = cos(Y/2)sin(Z/2)cos(X/2)-sin(Y/2)cos(Z/2)sin(X/2) 
w = cos(Y/2)cos(Z/2)cos(X/2)-sin(Y/2)sin(Z/2)sin(X/2) 
         */

        //得到推导公式[欧拉角x=2*Aan(q.x)]  
        float angle = 2 * Mathf.Rad2Deg * Mathf.Atan(q.x);
        float anglez = 2 * Mathf.Rad2Deg * Mathf.Atan(q.z);
        //限制速度  
        float angleOnWall=0f;
        //控制墙上摄像机旋转
        
        if (isOnWall)
        {
            if (wallDirection.x == -1)
            {
                angleOnWall = camOnWall;            

            }
            else if (wallDirection.x == 1)
            {
                angleOnWall = -camOnWall;
            }
            Debug.Log("onwall");         
        }
        else if (Onground)
        {
            angleOnWall = 0f;
        }
        anglez = Mathf.Lerp(anglez, angleOnWall, 1f);
        angle = Mathf.Clamp(angle, -60f, 50f);
        //反推出q的新x的值  
        q.x = Mathf.Tan(Mathf.Deg2Rad * (angle / 2));
        q.z = Mathf.Tan(Mathf.Deg2Rad * (anglez / 2));
        return q;
    }
    //接口，供音频动画使用
    public bool ifGrounded()
    {
        return Onground;
    }
    public bool ifAir()
    {
        return IsInAir();
    }
    public bool ifOnWall()
    {
        return isOnWall;
    }
    public float vspeed()
    {
        return v;
    }
    public bool ifJumping()
    {
        return jumping;
    }
}