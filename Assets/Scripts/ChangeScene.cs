using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// 画面遷移
public class ChangeScene : MonoBehaviour
{
    public string sceneName;    // 画面遷移先
    public bool selection_flag; // Selectionを表示するか
    private float step_time;    // 経過時間
    private float showed_time;  // Selectionを表示した時間

    // Start is called before the first frame update
    void Start()
    {
        step_time = 0.0f;       // 経過時間初期化
        showed_time = 0.0f;     // Selectionを表示した時間初期化
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間をカウント
        step_time += Time.deltaTime;

        // Selectionを表示しない場合は5秒後に画面遷移
        if (step_time >= 5.0f && !selection_flag)
        {
            Load();
        }
        // Selectionを表示する場合はSelectionを表示してから5秒後に画面遷移
        else if (step_time >= showed_time + 5.0f && SceneManager.GetSceneByName("Selection").IsValid() == true && selection_flag)
        {
            Load();
        }

    }

    // sceneNameへ画面遷移
    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Selectionを表示
    [Obsolete]
    public void ShowSelection()
    {
        Application.LoadLevelAdditive("Selection"); // Selectionを表示
        showed_time = step_time;                    // Selectionを表示した時間を保存
    }
}
