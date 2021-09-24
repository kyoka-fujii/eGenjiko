using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerController : MonoBehaviour
{
    private LineRenderer lineRenderer;  // 線を描くためのComponent
    private Camera mainCamera;
    private Vector3 start_drag;     // ドラッグの始点
    private Vector3 end_drag;       // ドラッグの終点
    private bool drag_flag;         // ドラッグかクリックか判別
    private float[] posVerLine;     // 源氏香図縦線のx座標
    private float screenWdt;        // スクリーンの横幅

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        mainCamera = Camera.main;
        drag_flag = false;
        screenWdt = Screen.width;
        posVerLine = new float[5];

        for (int i = 0; i < 5; i++)
        {
            posVerLine[i] = screenWdt * (5 - i) / 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ドラッグの軌跡を描画（クリックの場合は入らない）
        if (Input.GetMouseButton(0) && drag_flag)
        {
            //DrawPoint();
            DrawLine();
        }
        else if(!Input.GetMouseButton(0) && drag_flag)
        {
            // 縦線のindex取得
            int lineIndexUp = ConvertScreenToLineIndex(start_drag.x);
            int lineIndexDown = ConvertScreenToLineIndex(end_drag.x);

            // 取得したペア(lineIndexDown, lineIndexUp)を集合に追加
            if (lineIndexDown != lineIndexUp)
            {
                GameManager.AddPair(lineIndexDown, lineIndexUp);
            }

            // 集合から源氏香の図のインデックスを取得して表示する
            int genjiIndex = GameManager.GetGenjiIndex(GameManager.connectSet1, GameManager.connectSet2);

            // genjiIndex番目の画像を読み込んでキャンバスに表示
            if (genjiIndex >= 0)
            {
                DrawGenjiImg(genjiIndex);
            }

            drag_flag = false;
        }
        // ドラッグしていないときは線を非表示
        else if (!Input.GetMouseButton(0))
        {
            lineRenderer.positionCount = 0;
        }
    }

    // クリックイベント
    public void PointerDown()
    {
        start_drag = Input.mousePosition;
        start_drag.z = 10.0f;
    }

    // ドラッグイベント
    public void Drag()
    {
        end_drag = Input.mousePosition;
        end_drag.z = 10.0f;
        drag_flag = true;
    }

    // Resetボタンイベント
    public void ResetButton()
    {
        GameManager.connectSet1 = new HashSet<int>();
        GameManager.connectSet2 = new HashSet<int>();
        DrawGenjiImg(0);

    }

    // Answerボタンイベント
    public void AnswerButton()
    {
        if (GameManager.currentGenjiIndex == GameManager.correctGenjiIndex)
        {
            DrawImg(GameManager.pathCorrectImg);
        }
        else
        {
            DrawImg(GameManager.pathDiscorrectImg);
        }
    }

    /* 1objectに1lineRendererしか追加できないため保留
    // ポインターを表示
    void DrawPoint()
    {
        var segments = 360;

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = segments;
        lineRenderer.loop = true;

        var points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            var x = Mathf.Sin(rad) * 0.3f + mainCamera.ScreenToWorldPoint(end_drag).x;
            var y = Mathf.Cos(rad) * 0.3f + mainCamera.ScreenToWorldPoint(end_drag).y;
            points[i] = new Vector3(x, y, 10.0f);
        }

        lineRenderer.SetPositions(points);
    }
    */

    // 線を描画
    void DrawLine()
    {
        // 線の色
        Color color = new Color(0.6f, 0.6f, 0.6f, 1);
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        // 線の太さ
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // 頂点の数
        lineRenderer.positionCount = 2;

        // 線の位置
        lineRenderer.SetPosition(0, mainCamera.ScreenToWorldPoint(start_drag));
        lineRenderer.SetPosition(1, mainCamera.ScreenToWorldPoint(end_drag));
    }

    // スクリーン座標から一番近い縦線のindexに変換する
    int ConvertScreenToLineIndex(float screenX)
    {
        var minDistance = screenWdt;
        var minIndex = -1;

        for (var i = 0; i < 5; i++)
        {
            var distance = Math.Abs(screenX - posVerLine[i]);
            if (minDistance > distance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }
        return minIndex;
    }

    void DrawGenjiImg(int index)
    {
        GameManager.currentGenjiIndex = index;
        Sprite sprite = Resources.Load<Sprite>(GameManager.genjiImgList[index]);
        image.sprite = sprite;
    }

    void DrawImg(string path)
    {
        Sprite sprite = Resources.Load<Sprite>(path);
        image.sprite = sprite;
    }
}
