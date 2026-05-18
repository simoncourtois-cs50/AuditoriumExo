using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class CircleShape : MonoBehaviour
{
    #region Show In Inspector

    [Min(3)]
    [SerializeField]
    private int _segmentCount;

    [Min(0)]
    [SerializeField]
    private float _radius;

    #endregion


    #region Properties

    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            OnValidate();
        }
    }

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        LineRenderer.useWorldSpace = false;
        LineRenderer.loop = true;
        LineRenderer.positionCount = Mathf.Max(_segmentCount + 1, 0);
        CreatePoints();

        CircleCollider.radius = _radius + LineRenderer.startWidth;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        LineRenderer.useWorldSpace = false;
        LineRenderer.loop = true;
        LineRenderer.positionCount = Mathf.Max(_segmentCount + 1, 0);
        CreatePoints();

        CircleCollider.radius = _radius + LineRenderer.startWidth;
    }
#endif

    #endregion


    #region Main

    private void CreatePoints()
    {
        if (LineRenderer.positionCount <= 2)
        {
            LineRenderer.SetPositions(new Vector3[0]);
            return;
        }

        Vector3[] positions = new Vector3[LineRenderer.positionCount];

        float x;
        float y;
        float z = 0f;

        float angle = 360f / _segmentCount;

        for (int i = 0; i < LineRenderer.positionCount; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _radius;

            positions[i] = new Vector3(x, y, z);

            angle += 360f / _segmentCount;
        }

        LineRenderer.SetPositions(positions);
    }

    #endregion


    #region Private

    private LineRenderer _lineRenderer;

    private LineRenderer LineRenderer
    {
        get
        {
            if (_lineRenderer == null)
            {
                _lineRenderer = GetComponent<LineRenderer>();
            }
            return _lineRenderer;
        }
    }

    private CircleCollider2D _circleCollider;
    private CircleCollider2D CircleCollider
    {
        get
        {
            if (_circleCollider == null)
            {
                _circleCollider = GetComponent<CircleCollider2D>();
            }
            return _circleCollider;
        }
    }

    #endregion
}