using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI; 

public class DementiaGame2_UIManager : MonoBehaviour
{
    [Header("DrawLine")]
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    public bool DrawLine_GameRun, DrawLine_Click;
    [Header("UI")]
    
    public Image[] ProverbImage; //문제의 위쪽 이미지
    public Image[] Proverb_shadow; //문제의 아래쪽 이미지 (그림자)
    
    public Sprite[] Sprite_Kinds_1;//1-4
    public Sprite[] Sprite_shadowKinds_1;

    public Sprite[] Sprite_Kinds_2;//5-8
    public Sprite[] Sprite_shadowKinds_2;

    public Sprite[] Sprite_Kinds_3;//9-12
    public Sprite[] Sprite_shadowKinds_3;

    public Sprite[] Sprite_Kinds_4;//13-16
    public Sprite[] Sprite_shadowKinds_4;

    public GameObject[] startPos;
    public GameObject[] endPos;

    public LineRenderer TwoDotRenderer;
  // public List<GameObject> LineOB; 

    private void Start()
    {
        DrawLine_Start();
    }
    //그림 그리는 함수 
    public void DrawLine_Start()
    {
        StopCoroutine(_DrawLine_Start());
        StartCoroutine(_DrawLine_Start());
    }
    IEnumerator _DrawLine_Start()
    {
        DrawLine_GameRun = true;
        while (DrawLine_GameRun)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tempFingerPos.z = 0;
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                {
                    if(lineRenderer!= null)
                    UpdateLine(tempFingerPos);
                }
            }
            yield return null;
        }
    }

    //선그리기
    void CreateLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositions.Clear();

        Vector3 line_StartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(line_StartPos);
        fingerPositions.Add(line_StartPos);
       // Debug.Log("line_StartPos" + line_StartPos);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
        edgeCollider.points = fingerPositions.ToArray();
    }
    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
        edgeCollider.points = fingerPositions.ToArray();
    }
}
