using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class CursorMove : MonoBehaviour
{
    [SerializeField] PlayerNumber _number;

    [Header("Camera")]
    [SerializeField] Transform _mainCamera;

    [Header("Cursor")]
    [SerializeField] RectTransform _cursor;
    [SerializeField] InputActionReference _cursorMove;
    [SerializeField] InputActionReference _decide;
    [SerializeField] float _cursorSpeed = 100.0f;
    [ReadOnly,SerializeField] Vector2 _p1RectPos;

    [Header("Decide")]
    [SerializeField] LayerMask _targetLayer = new LayerMask();

    TextMeshProUGUI _text;
    bool _push = false;

    Vector2 halfScreen;

    private void Start()
    {
        _p1RectPos = _cursor.localPosition;
        _text = _cursor.GetComponent<TextMeshProUGUI>();

        halfScreen = _cursor.root.GetComponent<RectTransform>().sizeDelta / 2;
    }

    private void OnEnable()
    {
        _decide.action.started += Decide;
    }

    private void OnDisable()
    {
        _decide.action.started -= Decide;
    }

    // Update is called once per frame
    void Update()
    {
        _p1RectPos = _cursor.localPosition;

        Vector2 dir = _cursorMove.action.ReadValue<Vector2>() * Time.deltaTime * _cursorSpeed;
        Vector2 targetPos = _p1RectPos + dir;
        _cursor.localPosition = new Vector3(
            Mathf.Clamp(targetPos.x, -halfScreen.x, halfScreen.x),
            Mathf.Clamp(targetPos.y, -halfScreen.y, halfScreen.y),
            0.0f);


        Debug.DrawRay(_mainCamera.position, new Vector3(_p1RectPos.x, _p1RectPos.y, halfScreen.x));
    }

    private void Decide(InputAction.CallbackContext obj)
    {
        if (_push) return;
        _push = true;

        _text.color = Color.red;

        Ray ray = new Ray(_mainCamera.position, new Vector3(_p1RectPos.x, _p1RectPos.y, halfScreen.x));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000.0f, _targetLayer))
        {
            if(hit.transform.TryGetComponent<IMenuTrigger>(out IMenuTrigger trigger))
            {
                trigger.Excute(_number);
            }
        }


        Invoke("WaitPush", 0.2f);
    }

    void WaitPush()
    {
        _text.color = Color.white;
        _push = false;
    }
}
