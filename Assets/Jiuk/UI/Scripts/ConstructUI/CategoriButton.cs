using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class CategoriButton : MonoBehaviour
    {
        public GameObject[] categoryScrollViews;

        public void ShowCategory(int index)
        {
            // 모든 카테고리의 ScrollView를 비활성화
            foreach (GameObject scrollview in categoryScrollViews)
            {
                scrollview.SetActive(false);
            }

            // 선택된 카테고리만 활성화
            if (index >= 0 && index < categoryScrollViews.Length)
            {
                categoryScrollViews[index].SetActive(true);
            }
        }

        public void ExitButton()
        {
            gameObject.SetActive(false);
        }
    }
}