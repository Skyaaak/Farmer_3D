using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellDetail : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI sellDetail;
    [SerializeField]
    private TextMeshProUGUI sellDetailPrice;
    [SerializeField]
    private Image coinImage;

    public void setDetail(string detail)
    {
        sellDetail.text = detail;
    }

    public void setPrice(string price)
    {
        sellDetailPrice.text = price;
    }

    public void clearDetail()
    {
        sellDetail.text = "";
        sellDetailPrice.text = "";
        coinImage.enabled = false;
    }
}
