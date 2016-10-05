using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class Reci_list_up : MonoBehaviour
{
    public GameObject manobj;
    ItemRecipeClass info;
    #region
    public GameObject[] Reci_pan_names;
    public GameObject[] Reci_pan_mats;
    public GameObject[] Reci_pan_matimgs;
    public int current_req_mat;
    public Text Reci_Create;

    public GameObject Create_btn;

    public Image will_create_back;

    public Image will_create_goods;

    bool material_leak;
    #endregion
    public void item_info_up(ItemRecipeClass _info, List<MaterialClass> _minfo, Sprite goods, Sprite ageimg)
    { // ㅇㄴㅇㅁㄴㅇㄴㅁㅇ
        current_req_mat = 0;
        info = _info;
        this.gameObject.SetActive(true);
        material_leak = false;
        /* Reci_pan_names[0].GetComponent<Text>().text = info.M_name;
         Reci_pan_names[1].GetComponent<Text>().text = info.S_name;*/
        for (int i = 0; i < 4; i++)
        {
			if (info.m_count[i] == 0)
            {
                Reci_pan_mats[i].SetActive(false);
                Reci_pan_matimgs[i].SetActive(false);
            }
            else
            {
                Reci_pan_mats[i].SetActive(true);
                Reci_pan_matimgs[i].SetActive(true);
                current_req_mat++;
				MaterialClass a = _minfo.Find(x => x.Material_id.Equals(info.m_id[i]));
				Reci_pan_mats[i].GetComponent<Text>().text = "" + a.Material_count + " / " + info.m_count[i];
                will_create_goods.sprite = goods;
                will_create_back.sprite = ageimg;
                if(a.Material_count < info.m_count[i])
                {
                    material_leak = true;
                }

                switch_btn_state();
            }
        }

        Reci_Create.text = "" + info.price;
    }
    Image[] btnimages;
    Button Create_btn_act;


    void Awake()
    {
        btnimages = Create_btn.GetComponentsInChildren<Image>();
        Create_btn_act = Create_btn.GetComponent<Button>();
    }

    void switch_btn_state()
    {
        if (material_leak)
        {
            Create_btn_act.enabled = false;
            for(int i = 0; i < btnimages.Length;i++)
            {
                btnimages[i].color = Color.gray;
            }
        }
        else
        {
            Create_btn_act.enabled = true;
            for (int i = 0; i < btnimages.Length; i++)
            {
                btnimages[i].color = Color.white;
            }
        }
    }

    public void Init_Create()
    {
        StartCoroutine(this.Yogurt_create());
    }

    public IEnumerator Yogurt_create()
    {
                manobj.GetComponent<Crafting>().Create_Complete();
        yield return null;
    }
    }
