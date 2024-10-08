using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Item;

//������ â�� UI�� ���� �����ϴ� ��ũ��Ʈ
public class ItemUI : MonoBehaviour
{
    //������ â�� �̷�� UI��
    public Text itemNameText;
    public Text priceText;
    public Text numText;
    public Text marketPrice;
    public Text purchasePrice;
    public Button rejectButton;
    public InputField priceInputField;

    //���� �ź� ��ư ����
    public Color activeColor = Color.gray;
    public Color inactiveColor = Color.red;

    //���� �ź� ��ư Ȱ��ȭ ����
    public bool isOn = false;

    //�������� ����
    public int itemCount;

    //�������� �ʱ� ���� ���� ����, ���� ������ ���� ����
    private int initialItemCount;

    public Item item;

    private PurchaseSystem purchaseSystem;
    

    private void Start()
    {
        RejectButtonClick();
    }

    //������ UI�� �������ִ� �Լ�, PurchaseSystem ��ũ��Ʈ���� �ҷ��� ������.
    public void Setup(PurchaseSystem system, Item newItem, int initialItemCount)
    {
        purchaseSystem = system;
        item = newItem;
        itemNameText.text = newItem.itemName;
        marketPrice.text = newItem.itemPrice.ToString();
        this.initialItemCount = initialItemCount;
        itemCount = initialItemCount;
        UpdateNumText();
    }

    //�Ʒ� �� �Լ��� UI ��ư Ŭ������ �ҷ���

    //������ ���� ���� �Լ�
    public void IncreaseNumber()
    {
        if (itemCount < initialItemCount)
        {
            itemCount++;
            UpdateNumText();
        }
    }

    //������ ���� ���� �Լ�
    public void DecreaseNumber()
    {
        if (itemCount > 0) 
        {
            itemCount--;
            UpdateNumText();
        }
    }

    //���� �ź� ��ư �Լ�
    public void RejectButtonClick()
    {
        isOn = !isOn;

        if(!isOn)
        {
            rejectButton.image.color = activeColor;
        }
        else
        {
            rejectButton.image.color = inactiveColor;
        }
    }



    //������ ������ ǥ�����ִ� �Լ�
    private void UpdateNumText()
    {
        numText.text = itemCount.ToString();
    }

    //������ ������ ǥ�����ִ� �Լ�, PurchaseSystem ��ũ��Ʈ���� �����
    public void UpdatePrice(float newPrice)
    {
        priceText.text = newPrice.ToString();
    }

    //���԰� ���� ĭ�� �ۼ��ϴ� �Լ�
    public void UpdatePriceFromInput()
    {
        if (!string.IsNullOrEmpty(priceInputField.text))
        {
            float newPrice;
            if (float.TryParse(priceInputField.text, out newPrice))
            {
                purchasePrice.text = newPrice.ToString();
            }
            else
            {
                Debug.LogError("Invalid price input!");
            }
        }
    }
}
