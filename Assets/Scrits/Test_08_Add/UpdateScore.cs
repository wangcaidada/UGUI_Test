using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UpdateScore : MonoBehaviour
{

    //using UnityEngine;
    //using System.Collections;

    //public class ExampleScript : MonoBehaviour
    //{
    //    public GUIText scoreBoard;
    //    public int score;

    //    void Update()
    //    {
    //        string scoreText = "Score: " + score.ToString();
    //        scoreBoard.text = scoreText;
    //    }

    //上面这种处理方式缺点
    //1.程序每一帧都会都会刷新UI，哪怕数据没有发生变化
    //2.每一帧都进行一次字符串拼接的操作，徒增GC
    //3.MoniBehaviour的Update函数是比较消耗性能的
    //===============================================================
    //优化后的处理方案
    //1.采用“观察者模式”，只对自己关心的内容感兴趣，只有数据发生变化的时候才做相应的处理
    //2.字符串的拼接采用string的Format接口（内部使用StringBuilder，优化字符串拼接，减少GC）
    //3.去除Update函数（MoniBehaviour的Update函数，即使函数体是空的，在调用时也会有些许性能消耗，所以不使用的时候一定要删掉）

    public TMP_Text scoreText;
    public TMP_InputField scoreInput;
    public Button scoreUpdateBtn;

    //模拟分数变化事件
    public Action<int> onScoreChange;

    private int _socre;

    //模拟外部更新分数接口
    public int Score
    {
        get
        {
            return _socre;
        }
        set
        {
            //模拟数据发生变化
            if (_socre != value)
            {
                _socre = value;
                //模拟派发分数变化事件
                onScoreChange?.Invoke(_socre);
            }
        }
    }

    private void Start()
    {
        scoreUpdateBtn.onClick.AddListener(OnClickUpdateScoreBtn);
        //模拟监听分数变化事件
        onScoreChange += OnScoreChange;

        //初始化UI
        scoreText.text = string.Format("{0}：{1}", "得分", 0);
    }

    private void OnScoreChange(int newScore)
    {
        //模拟处理事件触发处理
        scoreText.text = string.Format("{0}：{1}", "得分", newScore);
    }

    private void OnClickUpdateScoreBtn()
    {
        string str = scoreInput.text;
        if (!str.Equals(string.Empty))
        {
            int num = Convert.ToInt32(str);
            Score += num;
        }
    }
}
