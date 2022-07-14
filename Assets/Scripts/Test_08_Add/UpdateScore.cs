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

    //�������ִ���ʽȱ��
    //1.����ÿһ֡���ᶼ��ˢ��UI����������û�з����仯
    //2.ÿһ֡������һ���ַ���ƴ�ӵĲ�����ͽ��GC
    //3.MoniBehaviour��Update�����ǱȽ��������ܵ�
    //===============================================================
    //�Ż���Ĵ�����
    //1.���á��۲���ģʽ����ֻ���Լ����ĵ����ݸ���Ȥ��ֻ�����ݷ����仯��ʱ�������Ӧ�Ĵ���
    //2.�ַ�����ƴ�Ӳ���string��Format�ӿڣ��ڲ�ʹ��StringBuilder���Ż��ַ���ƴ�ӣ�����GC��
    //3.ȥ��Update������MoniBehaviour��Update��������ʹ�������ǿյģ��ڵ���ʱҲ����Щ���������ģ����Բ�ʹ�õ�ʱ��һ��Ҫɾ����

    public TMP_Text scoreText;
    public TMP_InputField scoreInput;
    public Button scoreUpdateBtn;

    //ģ������仯�¼�
    public Action<int> onScoreChange;

    private int _socre;

    //ģ���ⲿ���·����ӿ�
    public int Score
    {
        get
        {
            return _socre;
        }
        set
        {
            //ģ�����ݷ����仯
            if (_socre != value)
            {
                _socre = value;
                //ģ���ɷ������仯�¼�
                onScoreChange?.Invoke(_socre);
            }
        }
    }

    private void Start()
    {
        scoreUpdateBtn.onClick.AddListener(OnClickUpdateScoreBtn);
        //ģ����������仯�¼�
        onScoreChange += OnScoreChange;

        //��ʼ��UI
        scoreText.text = string.Format("{0}��{1}", "�÷�", 0);
    }

    private void OnScoreChange(int newScore)
    {
        //ģ�⴦���¼���������
        scoreText.text = string.Format("{0}��{1}", "�÷�", newScore);
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
