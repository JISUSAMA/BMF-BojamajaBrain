using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DementiaGame7_UIManager : MonoBehaviour
{
    public Touch_TimerManager TimerManager_sc;
    [Header("UI")]
    public Image[] Question_Kinds;
    public GameObject[] Maze1_Trigger;
    public GameObject[] Maze2_Trigger;
    public GameObject[] Maze3_Trigger;
    public GameObject[] Maze4_Trigger;
    public GameObject[] Maze5_Trigger;

    public bool[] triggerCount; 

  //  public Vector3 handle_vec;
  //  public Vector3 handle_vec_orgin;
    [Header("DrawLine")]
    public GameObject linePrefab;
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> fingerPositions;
    
    public bool DrawLine_GameRun, DrawLine_Click;
    public static DementiaGame7_UIManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;
    }
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
        //handle_vec = handle.gameObject.transform.position;
        //handle_vec.z = 0;
        DrawLine_GameRun = true;

        while (DrawLine_GameRun)
        {
            int Temp_finger = triggerCount.Count(c => c);

            if (Input.GetMouseButtonDown(0))
            {
                CreateLine();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (Temp_finger.Equals(5))
                {
                    TimerManager_sc.Question_Success();
                    fingerPositions.Clear();
                }
                else
                {
                    TimerManager_sc.Question_Fail();
                    fingerPositions.Clear();
                }
                Destroy(currentLine.gameObject);
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                tempFingerPos.z = 0;
                if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .1f)
                {
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
