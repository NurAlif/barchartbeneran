using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChartAxis : MonoBehaviour
{
    [SerializeField]
    private ObjectPool markPool;
    [SerializeField]
    private ObjectPool lineMarkPool;

    [SerializeField]
    private float labelOffset = 1f;
    
    private List<GameObject> marks = new List<GameObject>();
    private List<GameObject> lineMarks = new List<GameObject>();

    private List<string> labels = new List<string>();

    private float gap = 1f;

    private LineRenderer lineRenderer;
    private Vector2 lineOffset;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        
    }

    public void SetMarks(bool vertical, List<string> newLabels, float gap, Vector2 offset)
    {
        this.lineOffset = offset;
        this.gap = gap;

        int count = newLabels.Count - labels.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) { 
                AddMark();
                if (!vertical) AddLineMark();
            }
        else if (count < 0)
            for (int i = 0; i < count * -1; i++) {
                RemoveMark();
                if (!vertical) RemoveLineMark();
            }

        labels = newLabels;

        if(vertical) ArrangeVertical();
        else ArrangeHorizontal();
    }

    private void ArrangeHorizontal()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            GameObject mark = marks[i];
            Label label = mark.GetComponent<Label>();
            label.SetLabel(labels[i]);
            label.SetAlign(Label.ALIGN_CENTER);

            Vector3 newPos = Vector3.right * gap * i;
                
            mark.transform.localPosition = newPos + (Vector3.down * labelOffset); ;

            GameObject lineMark = lineMarks[i];
            lineMark.transform.localPosition = newPos + (Vector3.down * lineOffset.y);
        }

        Vector3[] positions = { (Vector3.down * lineOffset.y) + (Vector3.left * lineOffset.x), 
            (Vector3.right * gap * labels.Count) + (Vector3.down * lineOffset.y)};
        lineRenderer.SetPositions(positions);
    }

    private void ArrangeVertical()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            GameObject mark = marks[i];
            Label label = mark.GetComponent<Label>();
            label.SetLabel(labels[i]);
            label.SetAlign(Label.ALIGN_RIGHT);

            Vector3 newPos = (Vector3.up * gap * i) + (Vector3.left * labelOffset);

            mark.transform.localPosition = newPos;
        }

        Vector3[] positions = { (Vector3.left * lineOffset.x) + (Vector3.down * lineOffset.y), 
            (Vector3.up * gap * labels.Count) + (Vector3.left * lineOffset.x)};

        lineRenderer.SetPositions(positions);
    }

    private void RemoveMark()
    {
        int index = marks.Count - 1;

        marks[index].SetActive(false);

        marks.RemoveAt(index);
    }

    private void AddMark()
    {
        GameObject go = markPool.GetPooled();
        go.transform.SetParent(transform);
        marks.Add(go);
    }

    private void RemoveLineMark()
    {
        int index = lineMarks.Count - 1;

        lineMarks[index].SetActive(false);

        lineMarks.RemoveAt(index);
    }

    private void AddLineMark()
    {
        GameObject go = lineMarkPool.GetPooled();
        go.transform.SetParent(transform);
        lineMarks.Add(go);
    }
}
