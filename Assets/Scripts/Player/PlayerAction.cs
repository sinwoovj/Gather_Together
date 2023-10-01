//PlayerAction.cs

using DI;
using UnityEngine;


public class PlayerAction : MonoBehaviour
{

    [SerializeField] 
    public float moveSpeed = 1f;
    public float runSpeed = 1.5f;

    [Inject]
    PlayData playData;

    [Inject]
    GameManager Manager;

    Rigidbody2D rb;
    Animator anim;
    SaveSettingValues saveSettingValues;

    public float h, v; //수직, 수평
    bool isHorizonMove; //수평이동인지 아닌지 플래그

    Vector2 dirVec; //현재 바라보고 있는 방향 값을 가진 변수가 필요
    GameObject scanObject; //스캔된 오브젝트

    public LayerMask ObjectLayer; //선택한 레이어에 속한 객체들에 대해서만 레이캐스팅 검사를 진행하게 된다.

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(StartGame.isSaving)
            saveSettingValues = GameObject.Find("SaveSettingValues").GetComponent<SaveSettingValues>();
    }

    private void Start()
    {
        DIContainer.Inject(this);   
    }

    void Update()
    {

        //#Move Setting Value
        //Main Scene에서 받아온 Setting 값

        if (StartGame.isSaving){
            string Up = saveSettingValues.UpKeySettingValue;
            string Down = saveSettingValues.DownKeySettingValue;
            string Left = saveSettingValues.LeftKeySettingValue;
            string Right = saveSettingValues.RightKeySettingValue;
            string Interaction = saveSettingValues.interactionKeySettingValue;
        }
        //#Move Value

        h = playData.isAction ? 0 : Input.GetAxisRaw("Horizontal"); //isAction이라는 플래그 값을 활용함.
        v = playData.isAction ? 0 : Input.GetAxisRaw("Vertical");
        // KeySettingValue.keys[1]
        //#수평, 수직 이동 버튼이벤트를 변수로 저장
        //상태 변수를 사용하여 플레이어 이동을 제한함. 
        bool hDown = playData.isAction ? false : Input.GetButtonDown("Horizontal"); 
        bool vDown = playData.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = playData.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = playData.isAction ? false : Input.GetButtonUp("Vertical");

        //#수평 이동 체크

        if (hDown) //수평 키를 누르거나 수직 키를 뗄 때
            isHorizonMove = true;
        else if (vDown) //수직 키를 누르거나 수평 키를 뗄 때
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0; //동시 키 입력 오류 방지 >> 현재 AxisRaw 값에 따라 수평, 수직 판단하여 해결

        //#애니메이션
        if(anim.GetInteger("hAxisRaw") != h) //hAxisRaw 파라미터 값과 h가 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h); //명시적 형변환
        }
        else if (anim.GetInteger("vAxisRaw") != v) //vAxisRaw 파라미터 값과 v가 다를 때
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v); //명시적 형변환
        }
        else
        {
            anim.SetBool("isChange", false); // 그외 상황에는 isChange 값을 false로 돌린다.
        }

        //Direction
        if (vDown && v == 1) //수직이동 키를 눌렀을 때, v값이 1이 될때이므로 상단 이동 키를 눌렀을 때
            dirVec = Vector2.up; //현재 바라보고 있는 방향의 값을 위로 설정
        else if (vDown && v == -1)
            dirVec = Vector2.down;
        else if (hDown && h == 1)
            dirVec = Vector2.right;
        else if (hDown && h == -1)
            dirVec = Vector2.left;  

        //Scan Object
        if ((Input.GetButtonUp("Interaction") && scanObject != null && !playData.isAction) || 
            (Input.GetButtonUp("Jump") && scanObject != null && !playData.isAction)){
            Manager.Action(scanObject);
        }
            
    }

    private void FixedUpdate()
    {
        //#Move
        Vector2 moveVec = isHorizonMove ? new Vector3(h, 0, 0) : new Vector3(0, v, 0); //플래그 변수(isHorizonMove) 하나로 수평, 수직이동을 결정 [삼항 연산자 사용]
        rb.velocity = moveVec * (this.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Static ? 0 : (Input.GetKey("left shift") ? runSpeed : moveSpeed)); //부드러운 움직임

        Vector2 dirOfRaycast = rb.position;
        dirOfRaycast.y -= (float)0.2;
        //Ray 
        Debug.DrawRay(dirOfRaycast,dirVec * 0.25f, new Color(0,1,0)); // DrawRay(기준 위치, 쏘는 방향 * 길이, 색상)
        RaycastHit2D rayHit = Physics2D.Raycast(dirOfRaycast, dirVec, 0.7f, ObjectLayer); // Raycast(기준 위치, 쏘는 방향, 길이, 인식할 레이어)

        if(rayHit.collider != null) //collider 값이 null이 아닐때 >> 즉, 뭔가 있을 때
        {
            scanObject = rayHit.collider.gameObject; //RatCast 된 오브젝트를 변수로 저장하여 활용!
        }
        else
        {
            scanObject = null;
        }
    }
}
