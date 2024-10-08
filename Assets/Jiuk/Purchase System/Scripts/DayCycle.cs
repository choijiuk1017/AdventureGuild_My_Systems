using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� �� ���� ����Ŭ�� �����ϴ� ��ũ��Ʈ
public class DayCycle : MonoBehaviour
{
    //������ ���� ������ �˷��ִ� �̺�Ʈ
    public delegate void DayChangedEventHandler();
    public static event DayChangedEventHandler OnDayChanged;
    //��¥ ����
    int year;
    int month;
    int day;
    
    //�� �޸��� �ִ� ��
    int[] maxDay = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    //�̺�Ʈ�� �߻��� ��¥
    int eventDay;

    //��¥�� ǥ���� �ؽ�Ʈ
    public Text dateText;

    public Eventmanager eventSystem;

    private void Start()
    {
        year = 2024;
        month = 1;
        day = 1;
        eventDay = day;
        UpdateDateText();
        StartCoroutine(FlowDayRoutine()) ;
    }

    //��¥�� ������Ʈ ���ִ� �ڷ�ƾ
    IEnumerator FlowDayRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); //���� 1�ʸ� �Ϸ�� ������

            day++;

            if (OnDayChanged != null)
                OnDayChanged();

            //7���� ���������� �̺�Ʈ �߻�
            int daysSinceLastEvent = day - eventDay;
            int eventInterval = 7; // 7�� �ֱ�� �̺�Ʈ �߻�

            //if (daysSinceLastEvent >= 0)
            //{
            //    if (daysSinceLastEvent == eventInterval)
            //    {
            //        Debug.Log("�̺�Ʈ �߻�");
            //        eventSystem.TriggerRandomEvent();
            //        eventDay = day;
            //    }
            //}
            //else
            //{
            //   if(maxDay[month-1] - eventDay + day == eventInterval)
            //   {
            //        Debug.Log("�̺�Ʈ �߻�");
            //        eventSystem.TriggerRandomEvent();
            //        eventDay = day;
            //   }
            //}

            //��, �ذ� ������ ����
            if (day > maxDay[month])
            {
                day = 1;
                month++;

                if (month > 12)
                {
                    month = 1;
                    year++;
                }

                //���⿡ ���� ����
                if (IsLeapYear(year))
                {
                    maxDay[2] = 29;
                }
                else
                {
                    maxDay[2] = 28;
                }

            }


            UpdateDateText();
        }
    }

    //��¥ ������Ʈ �Լ�
    void UpdateDateText()
    {
        dateText.text = string.Format("{0:D4}-{1:D2}-{2:D2}", year, month, day); // �ؽ�Ʈ ������Ʈ
    }

    //�������� Ȯ���ϴ� �Լ�
    bool IsLeapYear(int year)
    {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
