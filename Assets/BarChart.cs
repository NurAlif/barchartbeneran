using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChart : MonoBehaviour
{
    [SerializeField]
    private ObjectPool barPool;
    
    [SerializeField]
    private ObjectPool labelPool;

    [SerializeField]
    private ChartAxis axisY;
    [SerializeField]
    private ChartAxis axisX;

    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 axisOffset;
    [SerializeField]
    private float labelOffset;

    private List<ChartDataset2> dataset = new List<ChartDataset2>();

    private List<GameObject> barBlocks = new List<GameObject>();
    private List<Label> valueLabels = new List<Label>();

    private float yDist = 0.8f;
    private float xScale = 0.06f;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetData(List<ChartDataset2> newDataset)
    {
        int count = newDataset.Count - dataset.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) AddBlock();
        if (count < 0)
            for (int i = 0; i < count * -1; i++) RemoveBlock();

        if(count != 0)
        {
            yDist = size.y/newDataset.Count;
        }

        // Fill data
        dataset = newDataset;
        int maxValX = 0;
        for (int i = 0; i < dataset.Count; i++)
        {
            ChartDataset2 dataset = newDataset[i];
            if (dataset.x > maxValX) maxValX = dataset.x;
        }
        Vector2Int max = CalcRes(maxValX);

        xScale = size.x / max.y;

        for (int i = 0; i < dataset.Count; i++)
        {
            GameObject block = barBlocks[i];
            ChartDataset2 dataset = newDataset[i];

            Vector3 position = Vector3.up * i * yDist;
            block.transform.localPosition = position;
            Vector3 length = Vector3.right * xScale * dataset.x;
            block.transform.localScale = Vector3.up + Vector3.forward + length;

            Label label = valueLabels[i];
            label.transform.localPosition = position + length + (Vector3.right * labelOffset);
            label.SetLabel(dataset.x.ToString());
            label.SetAlign(Label.ALIGN_LEFT);
        }

        List<string> labelsY = new List<string>();
        dataset.ForEach(x => labelsY.Add(x.y));

        axisY.SetMarks(true, labelsY, yDist, axisOffset);

        ArrangeAxisX(max);
    }

    private Vector2Int CalcRes(int maxVal)
    {
        int limit;
        int ceil;
        if (maxVal > 500)
        {
            // 1000
            limit = 1000;
            ceil = (int) Mathf.Ceil(maxVal/1000)*100;
        }
        else if (maxVal > 100)
        {
            // 500
            limit = 500;
            ceil = (int) Mathf.Ceil(maxVal / 1000) * 100;
        }
        else if (maxVal > 50)
        {
            // 100
            ceil = (int) Mathf.Ceil(maxVal / 10);
            limit = (ceil+1)*10;
        }
        else if (maxVal > 10)
        {
            // 50
            limit = 50;
            ceil = (int) Mathf.Ceil(maxVal / 10) * 10;
        }
        else
        {
            // 10
            limit = 10;
            ceil = (int) Mathf.Ceil(maxVal/10);
        }
        ceil++;
        Debug.Log(ceil);
        Debug.Log(limit);
        return new Vector2Int(ceil, limit);
    }

    private void ArrangeAxisX(Vector2Int ceilLimit)
    {
        float maxVal = ceilLimit.y;
        float length = size.x;
        int res = ceilLimit.x;


        float gap = (maxVal / res) / maxVal * length;

        List<string> labels = new List<string>();

        for (int i = 0; i <= res; i++) labels.Add(Mathf.FloorToInt(maxVal/res*i).ToString());

        axisX.SetMarks(false, labels, gap, axisOffset);
    }

    private void RemoveBlock()
    {
        int index = barBlocks.Count - 1;
        
        GameObject goBlock = barBlocks[index];
        barPool.Return(goBlock);
        goBlock.SetActive(false);
        barBlocks.RemoveAt(index);

        GameObject goLabel = valueLabels[index].gameObject;
        labelPool.Return(goLabel);
        goLabel.SetActive(false);
        valueLabels.RemoveAt(index);
    }

    private void AddBlock()
    {   
        //block
        GameObject go = barPool.GetPooled();
        go.transform.SetParent(transform);
        barBlocks.Add(go);

        // label
        GameObject valueLabel = labelPool.GetPooled();
        valueLabel.transform.SetParent(transform);
        Label label = valueLabel.GetComponent<Label>();
        valueLabels.Add(label);
        
    }


}
