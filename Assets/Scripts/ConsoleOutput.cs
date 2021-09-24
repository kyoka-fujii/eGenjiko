using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;

public class ConsoleOutput : MonoBehaviour
{
    //public SerialHandler serialHandler;
    public int num;
    private int genjiNum;

    //void Start()
    //{
    //    Debug.Log("ConsoleOutput");
    //    serialHandler.OnDataReceived += OnDataReceived;
    //}

    //void Update()
    //{
    //    if (num > 0 && SceneManager.GetSceneByName("Selection").IsValid() == true)
    //    {
    //        genjiNum = GameManager.correctNum[num - 1];
    //        Debug.Log(genjiNum.ToString());
    //        serialHandler.Write(genjiNum.ToString());
    //        num = 0;    // 1度だけif文に入るようにするため
    //    }
    //}

    //void OnDataReceived(string message)
    //{
    //    var data = message.Split(new string[] { "\n" }, System.StringSplitOptions.None);
    //    if (data.Length != 1)
    //    {
    //        return;
    //    }
    //    Debug.Log(data[0]);
    //    //if (data[0].Length != 4)
    //    //{
    //    //    return;
    //    //}
    //    //if (num > 0 && SceneManager.GetSceneByName("Selection").IsValid() == true)
    //    //{
    //    //    SceneManager.LoadScene("Show" + genjiNum);
    //    //}
    //}

    public string portName;
    public int baurate;

    SerialPort serial;
    bool isLoop = true;

    void Start()
    {
        this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);

        try
        {
            Debug.Log("hai");
   
               this.serial.Open();
            Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
        }
        catch (Exception e)
        {
            Debug.Log("can not open serial port");
        }
    }

    public void ReadData()
    {
        while (this.isLoop)
        {
            string message = this.serial.ReadLine();
            Debug.Log(message);
        }
    }

    void OnDestroy()
    {
        this.isLoop = false;
        this.serial.Close();
    }
}