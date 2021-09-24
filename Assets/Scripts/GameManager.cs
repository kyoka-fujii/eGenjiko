using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int[] correctNum;            // 香の番号を保存
    public static int correctGenjiIndex;        // 源氏香図のインデックス
    public static int currentGenjiIndex;
    public static HashSet<int>[][] genjiArray;  // 源氏香の図のペアのリスト(ペアを持たないものは記述しない)
    public static string[] genjiImgList;        // 画像のパスリスト
    public static string pathCorrectImg;        // 正解画像のパス
    public static string pathDiscorrectImg;     // 不正解画像のパス

    // つながっているインデックスの集合
    public static HashSet<int> connectSet1 = new HashSet<int>();
    public static HashSet<int> connectSet2 = new HashSet<int>();

    // Start is called before the first frame update
    void Start()
    {
        correctNum = new int[5];
        SetAnswer();
        currentGenjiIndex = 0;

        // 行ごとに取得したいのでジャグ配列として作成
        genjiArray = new HashSet<int>[52][];
        genjiArray[0] = new HashSet<int>[] { new HashSet<int>(), new HashSet<int>() };                          // hahakigi
        genjiArray[1] = new HashSet<int>[] { new HashSet<int>() { 0, 1 }, new HashSet<int>() { 2, 3 } };        // wakamurasaki
        genjiArray[2] = new HashSet<int>[] { new HashSet<int>() { 0, 1 }, new HashSet<int>() };                 // utsusemi
        genjiArray[3] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 2, 3, 4 }, new HashSet<int>() };        // tenarai
        genjiArray[4] = new HashSet<int>[] { new HashSet<int>() { 0, 2, 4 }, new HashSet<int>() { 1, 3 } };     // kagerou
        genjiArray[5] = new HashSet<int>[] { new HashSet<int>() { 0, 4 }, new HashSet<int>() { 2, 3 } };        // ukifune
        genjiArray[6] = new HashSet<int>[] { new HashSet<int>() { 0, 3, 4 }, new HashSet<int>() };              // adumaya
        genjiArray[7] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 3, 4 }, new HashSet<int>() };           // yadorigi
        genjiArray[8] = new HashSet<int>[] { new HashSet<int>() { 0, 2 }, new HashSet<int>() { 3, 4 } };        // sawarabi
        genjiArray[9] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 4 }, new HashSet<int>() { 2, 3 } };     // agemaki
        genjiArray[10] = new HashSet<int>[] { new HashSet<int>() { 1, 4 }, new HashSet<int>() { 2, 3 } };       // shiigamoto
        genjiArray[11] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 2, 4 }, new HashSet<int>() };          // hashihime
        genjiArray[12] = new HashSet<int>[] { new HashSet<int>() { 0, 4 }, new HashSet<int>() { 1, 2, 3 } };    // takekawa
        genjiArray[13] = new HashSet<int>[] { new HashSet<int>() { 0, 3 }, new HashSet<int>() };                // koubai
        genjiArray[14] = new HashSet<int>[] { new HashSet<int>() { 0, 2 }, new HashSet<int>() { 1, 3, 4 } };    // nioumiya
        genjiArray[15] = new HashSet<int>[] { new HashSet<int>() { 0, 4 }, new HashSet<int>() };                // maboroshi
        genjiArray[16] = new HashSet<int>[] { new HashSet<int>() { 0, 3 }, new HashSet<int>() { 1, 4 } };       // minori
        genjiArray[17] = new HashSet<int>[] { new HashSet<int>() { 0, 2 }, new HashSet<int>() { 1, 4 } };       // yuugiri
        genjiArray[18] = new HashSet<int>[] { new HashSet<int>() { 0, 4 }, new HashSet<int>() { 1, 2 } };       // suzumushi
        genjiArray[19] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 4 }, new HashSet<int>() };             // yokobue
        genjiArray[20] = new HashSet<int>[] { new HashSet<int>() { 0, 2, 4 }, new HashSet<int>() };             // kashiwagi
        genjiArray[21] = new HashSet<int>[] { new HashSet<int>() { 0, 1 }, new HashSet<int>() { 2, 4 } };       // wakana_ge
        genjiArray[22] = new HashSet<int>[] { new HashSet<int>() { 0, 3, 4 }, new HashSet<int>() { 1, 2 } };    // wakana_jou
        genjiArray[23] = new HashSet<int>[] { new HashSet<int>() { 0, 3 }, new HashSet<int>() { 1, 2 } };       // fujinouraba
        genjiArray[24] = new HashSet<int>[] { new HashSet<int>() { 0, 2, 3, 4 }, new HashSet<int>() };          // umegae
        genjiArray[25] = new HashSet<int>[] { new HashSet<int>() { 0, 4 }, new HashSet<int>() { 1, 3 } };       // makibashira
        genjiArray[26] = new HashSet<int>[] { new HashSet<int>() { 1, 4 }, new HashSet<int>() };                // fujibakama
        genjiArray[27] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 3 }, new HashSet<int>() { 2, 4 } };    // miyuki
        genjiArray[28] = new HashSet<int>[] { new HashSet<int>() { 0, 1 }, new HashSet<int>() { 3, 4 } };       // nowaki
        genjiArray[29] = new HashSet<int>[] { new HashSet<int>() { 1, 3 }, new HashSet<int>() };                // kagaribi
        genjiArray[30] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 2 }, new HashSet<int>() };             // tokonatsu
        genjiArray[31] = new HashSet<int>[] { new HashSet<int>() { 1, 3, 4 }, new HashSet<int>() };             // hotaru
        genjiArray[32] = new HashSet<int>[] { new HashSet<int>() { 0, 2, 3 }, new HashSet<int>() { 1, 4 } };    // kochou
        genjiArray[33] = new HashSet<int>[] { new HashSet<int>() { 1, 3 }, new HashSet<int>() { 2, 4 } };       // hatsune
        genjiArray[34] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 2 }, new HashSet<int>() { 3, 4 } };    // tamakazura
        genjiArray[35] = new HashSet<int>[] { new HashSet<int>() { 2, 4 }, new HashSet<int>() };                // otome
        genjiArray[36] = new HashSet<int>[] { new HashSet<int>() { 1, 2, 4 }, new HashSet<int>() };             // asagao
        genjiArray[37] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 2, 3 }, new HashSet<int>() };          // usukumo
        genjiArray[38] = new HashSet<int>[] { new HashSet<int>() { 1, 2 }, new HashSet<int>() { 3, 4 } };       // matsukaze
        genjiArray[39] = new HashSet<int>[] { new HashSet<int>() { 0, 3 }, new HashSet<int>() { 2, 4 } };       // eawase
        genjiArray[40] = new HashSet<int>[] { new HashSet<int>() { 1, 2, 3 }, new HashSet<int>() };             // sekiya
        genjiArray[41] = new HashSet<int>[] { new HashSet<int>() { 2, 3, 4 }, new HashSet<int>() };             // yomogiu
        genjiArray[42] = new HashSet<int>[] { new HashSet<int>() { 0, 1, 3 }, new HashSet<int>() };             // miotsukushi
        genjiArray[43] = new HashSet<int>[] { new HashSet<int>() { 2, 3 }, new HashSet<int>() };                // akashi
        genjiArray[44] = new HashSet<int>[] { new HashSet<int>() { 0, 3 }, new HashSet<int>() { 1, 2, 4 } };    // suma
        genjiArray[45] = new HashSet<int>[] { new HashSet<int>() { 0, 2 }, new HashSet<int>() { 1, 3 } };       // hanachirusato
        genjiArray[46] = new HashSet<int>[] { new HashSet<int>() { 0, 1 }, new HashSet<int>() { 2, 3, 4 } };    // sakaki
        genjiArray[47] = new HashSet<int>[] { new HashSet<int>() { 3, 4 }, new HashSet<int>() };                // aoi
        genjiArray[48] = new HashSet<int>[] { new HashSet<int>() { 0, 2 }, new HashSet<int>() };                // hananoen
        genjiArray[49] = new HashSet<int>[] { new HashSet<int>() { 0, 2, 3 }, new HashSet<int>() };             // momijinoga
        genjiArray[50] = new HashSet<int>[] { new HashSet<int>() { 1, 2, 3, 4 }, new HashSet<int>() };          // suetsumuhana
        genjiArray[51] = new HashSet<int>[] { new HashSet<int>() { 1, 2 }, new HashSet<int>() };                // yuugao

        genjiImgList = new string[] {
            "53_2R9C_hahakigi",
            "50_5R9C_wakamurasaki",
            "52_3R9C_utsusemi",
            "02_5R1C_tenarai",
            "03_4R1C_kagerou",
            "04_3R1C_ukifune",
            "05_2R1C_adumaya",
            "06_1R1C_yadorigi",
            "07_6R2C_sawarabi",
            "08_5R2C_agemaki",
            "09_4R2C_shiigamoto",
            "10_3R2C_hashihime",
            "11_2R2C_takekawa",
            "12_1R2C_koubai",
            "13_6R3C_nioumiya",
            "14_5R3C_maboroshi",
            "15_4R3C_minori",
            "16_3R3C_yuugiri",
            "17_2R3C_suzumushi",
            "18_1R3C_yokobue",
            "19_6R4C_kashiwagi",
            "20_5R4C_wakana_ge",
            "21_4R4C_wakana_jou",
            "22_3R4C_fujinouraba",
            "23_2R4C_umegae",
            "24_1R4C_makibashira",
            "25_6R5C_fujibakama",
            "26_5R5C_miyuki",
            "27_4R5C_nowaki",
            "28_3R5C_kagaribi",
            "29_2R5C_tokonatsu",
            "30_1R5C_hotaru",
            "31_6R6C_kochou",
            "32_5R6C_hatsune",
            "33_4R6C_tamakazura",
            "34_3R6C_otome",
            "35_2R6C_asagao",
            "36_1R6C_usukumo",
            "37_6R7C_matsukaze",
            "38_5R7C_eawase",
            "39_4R7C_sekiya",
            "40_3R7C_yomogiu",
            "41_2R7C_miotsukushi",
            "42_1R7C_akashi",
            "43_6R8C_suma",
            "44_5R8C_hanachirusato",
            "45_4R8C_sakaki",
            "46_3R8C_aoi",
            "47_2R8C_hananoen",
            "48_1R8C_momijonoga",
            "49_6R9C_suetsumuhana",
            "51_4R9C_yuugao"
        };

        pathCorrectImg = "correct";
        pathDiscorrectImg = "discorrect";

        CheckDuplicate();
        correctGenjiIndex = GetGenjiIndex(connectSet1, connectSet2);
        Debug.Log(correctGenjiIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetAnswer()
    {
        var rand = new System.Random(); // ランダムで解答を作成
        for (int i = 0; i < 5; i++)
        {
            correctNum[i] = rand.Next(1, 6);    // 1, 2, 3, 4, 5のどれかが入る
            Debug.Log(correctNum[i]);
        }
    }

    // 重複している要素ごとのインデックスを集合にまとめる
    void CheckDuplicate() {
        for (int i = 0; i < correctNum.Length; i++)
        {
            for (int j = 0; j < correctNum.Length; j++)
            {
                if (i != j && correctNum[i] == correctNum[j])
                {
                    AddPair(i, j);
                }
            }
        }
    }

    // 取得したペアを集合に追加
    public static void AddPair(int index1, int index2)
    {
        // connectSet1が空のとき、connectSet1に追加
        if (connectSet1.Count == 0)
        {
            connectSet1.Add(index1);
            connectSet1.Add(index2);
            return;
        }

        // connectSet1にindex1かindex2があったら
        if (connectSet1.Contains(index1) || connectSet1.Contains(index2))
        {
            // 入力されたペアがconnectSet1とconnectSet2にひとつずつある場合は集合をまとめる
            if (connectSet2.Contains(index1) || connectSet2.Contains(index2))
            {
                connectSet1.UnionWith(connectSet2);
                connectSet2 = new HashSet<int>();
            }
            // connectSet1にindex1かindex2があったらconnectSet1に追加する
            else
            {
                connectSet1.Add(index1);
                connectSet1.Add(index2);
            }
        }
        // connectSet1にindex1かindex2がなければconnectSet2に追加する
        else
        {
            connectSet2.Add(index1);
            connectSet2.Add(index2);
        }
    }

    // 源氏香図のindexを取得
    public static int GetGenjiIndex(HashSet<int> cSet1, HashSet<int> cSet2)
    {
        for (int i = 0; i < genjiArray.Length; i++)
        {
            // genjiArrayのindex番目の要素とconnectSet集合を比較
            if (Compare(cSet1, cSet2, genjiArray[i]))
            {
                // 合っていたらgenjiArrayのindexを返す
                return i;
            }
        }
        // どれとも一致しない場合は-1を返す
        return -1;
    }

    // 2つのセットの比較を行う（セットの順番が異なっていても同じとみなす）
    public static bool Compare(HashSet<int> cSet1, HashSet<int> cSet2, HashSet<int>[] setArray)
    {
        if (CompareSet(cSet1, setArray[0]) && CompareSet(cSet2, setArray[1]))
        {
            return true;
        }
        if (CompareSet(cSet2, setArray[0]) && CompareSet(cSet1, setArray[1]))
        {
            return true;
        }
        return false;
    }

    // Setの比較
    public static bool CompareSet(HashSet<int> set1, HashSet<int> set2)
    {
        if (set1.Count != set2.Count)
        {
            return false;
        }

        foreach (var item in set1)
        {
            // ひとつでも違う要素があればfalse
            if (!set2.Contains(item))
            {
                return false;
            }
        }
        // 全ての要素が同じ時にtrue
        return true;
    }
}
