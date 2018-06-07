using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DubloModelAufbau : MonoBehaviour
{
    private List<Transform> _tr = new List<Transform>(); // List für Transform (Bauteilen)
    private List<Material> _mats = new List<Material>(); // List für Materials (Farbe und Beleuchtungen von den Bauteielen)
    private List<Transform> _nextmodel = new List<Transform>(); // Alle NextModel Objekte
    private List<Transform> _nmTurn = new List<Transform>(); // für zwei NextModelObjekte (zeignende Objekte)

    //Layer 1
    public Transform Long_Yellow_1_1Layer;
    public Transform Long_Yellow_2_1Layer;
    public Transform Long_Green_1_1Layer;
    public Transform Long_Green_2_1Layer;

    //Layer 2
    public Transform Medium_Blue_2Layer;
    public Transform Medium_Blue_2Layer_1;
    public Transform Medium_Orange_2Layer;
    public Transform Medium_Orange_2Layer_1;

    //Layer 3
    public Transform Medium_Orange_3Layer;
    public Transform Medium_Blue_3Layer;
    public Transform SmallTurm_3Layer;
    public Transform SmallTurm_3Layer_1;

    //Layer 4
    public Transform SmallTurm_4Layer;
    public Transform SmallTurm_4Layer_1;

    //Materials
    public Material ShaderMaterial_YellowPlastic;
    public Material ShaderMaterial_GreenPlastic;
    public Material ShaderMaterial_BluePlastic;
    public Material ShaderMaterial_OrangePlastic;
    public Material ShaderMaterial_Global;

    // Model der nächte Bauteilen
    public Transform Long_Yellow_1NM;
    public Transform Long_Yellow_2NM;
    public Transform Long_Green_1NM;
    public Transform Long_Green_2NM;

    public Transform Medium_Blue_1NM;
    public Transform Medium_Blue_2NM;
    public Transform Medium_Orange_1NM;
    public Transform Medium_Orange_2NM;

    public Transform Small_Turm_1NM;
    public Transform Small_Turm_2NM;

    public float speed = 20f;
    //public Transform Text3D;

    private int _clickcounter = 0;
    private float _color_a = 0.11f;




    private void Update()
    {
        int i = 0;
        foreach (var item in _nmTurn)
        {

            if (i % 2 == 0) // Oberste Objekt von NextModel-Objekten
            {
                item.Rotate(Vector3.left, speed * Time.deltaTime);
            }
            else // Unterste Objekt von NextModel-Objekten
            {
                item.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
            }
            i = i + 1;

        }
    }

    void Start()
    {

        // Hier werden alle Liste gefüllt.
        _tr.Add(Long_Yellow_1_1Layer);
        _tr.Add(Long_Yellow_2_1Layer);
        _tr.Add(Long_Green_1_1Layer);
        _tr.Add(Long_Green_2_1Layer);
        _tr.Add(Medium_Blue_2Layer);
        _tr.Add(Medium_Blue_2Layer_1);
        _tr.Add(Medium_Orange_2Layer);
        _tr.Add(Medium_Orange_2Layer_1);
        _tr.Add(Medium_Orange_3Layer);
        _tr.Add(Medium_Blue_3Layer);
        _tr.Add(SmallTurm_3Layer);
        _tr.Add(SmallTurm_3Layer_1);
        _tr.Add(SmallTurm_4Layer);
        _tr.Add(SmallTurm_4Layer_1);

        _mats.Add(ShaderMaterial_YellowPlastic);
        _mats.Add(ShaderMaterial_GreenPlastic);
        _mats.Add(ShaderMaterial_BluePlastic);
        _mats.Add(ShaderMaterial_OrangePlastic);
        _mats.Add(ShaderMaterial_Global);

        _nextmodel.Add(Long_Yellow_1NM);
        _nextmodel.Add(Long_Yellow_2NM);
        _nextmodel.Add(Long_Green_1NM);
        _nextmodel.Add(Long_Green_2NM);
        _nextmodel.Add(Medium_Blue_1NM);
        _nextmodel.Add(Medium_Blue_2NM);
        _nextmodel.Add(Medium_Orange_1NM);
        _nextmodel.Add(Medium_Orange_2NM);
        _nextmodel.Add(Small_Turm_1NM);
        _nextmodel.Add(Small_Turm_2NM);

        //Am Anfang kommt es als erstes Modell
        Long_Yellow_1NM.gameObject.SetActive(true);
        Long_Yellow_2NM.gameObject.SetActive(true);

        _nmTurn.Add(Long_Yellow_1NM);
        _nmTurn.Add(Long_Yellow_2NM);

    }
    /// <summary>
    /// Hier ist für den Sprachbefehl (NEXT), um den nächsten Teil aufzurufen.
    /// </summary>
    public void DuploModelBuildNext()
    {
        DuploModelBuild(true);
        if (_clickcounter < 14)
        {
            _clickcounter = _clickcounter + 1;
        }
    }
    /// <summary>
    /// Hier ist für den Sprachbefehl (PREVIOUS), um den vorherigen Teil aufzurufen.
    /// </summary>
    public void DuploModelBuildPrevious()
    {
        Debug.Log("previous");
        DuploModelBuild(false);
        if ((_clickcounter <= 14) && (_clickcounter >= 0))
        {
            Debug.Log("icerde");
            _clickcounter = _clickcounter - 1;
        }
        if (_clickcounter == 0)
        {
            NextModelOnOff(Long_Yellow_1NM, Long_Yellow_2NM);
        }
    }
    /// <summary>
    /// Hier ist für den Sprachbefehl (COMPLETELY), um das ganzes Model zuzeigen.
    /// </summary>
    public void DuploModelBuildCompletely()
    {
        foreach (var item in _tr)
        {
            item.gameObject.SetActive(true);
            AnimatiorActivation(item, item, false);// Alle Animationen werden beendet.
        }
        // Alle nextModel Objekten werden unsichtbar sein.

        NextModelOnOff(null, null);
        _clickcounter = 13;
    }
    /// <summary>
    /// Hier ist für den Sprachbefehl (RESET), um das ganzes Model wegzunehmen.
    /// </summary>
    public void DuploModelBuildReset()
    {
        foreach (var item in _tr)
        {
            item.gameObject.SetActive(false);

        }
        _clickcounter = 0;

        //Am Anfang kommt es als erstes Modell
        NextModelOnOff(Long_Yellow_1NM, Long_Yellow_2NM);
    }
    /// <summary>
    /// Hier ist für den Sprachbefehl (DARK), um die Farbe des ganzes Models dunkel zumachen.
    /// </summary>
    public void DubloModelColorDark()
    {
        if (_color_a <= 0.97f)
            _color_a = _color_a + 0.03f;
        ColorLighting(_color_a);
    }
    /// <summary>
    /// Hier ist für den Sprachbefehl (DARK), um die Farbe des ganzes Models heller zumachen.
    /// </summary>
    public void DubloModelColorLight()
    {
        if (_color_a >= 0.03f)
            _color_a = _color_a - 0.03f;
        ColorLighting(_color_a);
    }

    void ColorLighting(float colorValue)
    {
        foreach (var item in _mats)
        {
            item.color = new Color(item.color.r, item.color.g, item.color.b, colorValue);
        }
    }

    void AnimatiorActivation(Transform activtr, Transform inactivtr, bool bl)
    {
        Debug.Log("AnimatiorActivation:    " + bl);
        Animator anim_activ = activtr.GetComponent<Animator>();
        Animator anim_Inactiv = inactivtr.GetComponent<Animator>();

        if (bl)
        {
            if (activtr != inactivtr)
                anim_Inactiv.SetBool("Start", false);

            anim_activ.SetBool("Start", true);
        }
        else
        {
            if (activtr != inactivtr)
                anim_Inactiv.SetBool("Start", true);

            anim_activ.SetBool("Start", false);
        }
    }

    /// <summary>
    /// Hier wird der aktuelsten Teil sichbar und sein Animation wird aktiviert. 
    /// Nächste Model wird erscheint.
    /// </summary>
    /// <param name="bl"></param>
    public void DuploModelBuild(bool bl)
    {
        switch (_clickcounter)
        {
            case 0:
                Long_Yellow_1_1Layer.gameObject.SetActive(bl);
                NextModelOnOff(Long_Yellow_1NM, Long_Yellow_2NM);
                
                AnimatiorActivation(Long_Yellow_1_1Layer, Long_Yellow_1_1Layer, bl);

                break;
            case 1:
                Long_Yellow_2_1Layer.gameObject.SetActive(bl);
                AnimatiorActivation(Long_Yellow_2_1Layer, Long_Yellow_1_1Layer, bl);

                if (bl)
                    NextModelOnOff(Long_Green_1NM, Long_Green_2NM);
                else
                    NextModelOnOff(Long_Yellow_1NM, Long_Yellow_2NM);
                break;

            case 2:
                Long_Green_1_1Layer.gameObject.SetActive(bl);

                NextModelOnOff(Long_Green_1NM, Long_Green_2NM);

                AnimatiorActivation(Long_Green_1_1Layer, Long_Yellow_2_1Layer, bl);

                break;

            case 3:
                Long_Green_2_1Layer.gameObject.SetActive(bl);

                AnimatiorActivation(Long_Green_2_1Layer, Long_Green_1_1Layer, bl);
                if (bl)
                    NextModelOnOff(Medium_Blue_1NM, Medium_Blue_2NM);
                else
                    NextModelOnOff(Long_Green_1NM, Long_Green_2NM);
                break;

            case 4:
                Medium_Blue_2Layer.gameObject.SetActive(bl);
                NextModelOnOff(Medium_Blue_1NM, Medium_Blue_2NM);

                AnimatiorActivation(Medium_Blue_2Layer, Long_Green_2_1Layer, bl);


                break;

            case 5:
                Medium_Blue_2Layer_1.gameObject.SetActive(bl);

                AnimatiorActivation(Medium_Blue_2Layer_1, Medium_Blue_2Layer, bl);

                if (bl)
                    NextModelOnOff(Medium_Orange_1NM, Medium_Orange_2NM);
                else
                    NextModelOnOff(Medium_Blue_1NM, Medium_Blue_2NM);
                break;

            case 6:
                Medium_Orange_2Layer.gameObject.SetActive(bl);
                NextModelOnOff(Medium_Orange_1NM, Medium_Orange_2NM);

                AnimatiorActivation(Medium_Orange_2Layer, Medium_Blue_2Layer_1, bl);


                break;

            case 7:
                Medium_Orange_2Layer_1.gameObject.SetActive(bl);
                NextModelOnOff(Medium_Orange_1NM, Medium_Orange_2NM);

                AnimatiorActivation(Medium_Orange_2Layer_1, Medium_Orange_2Layer, bl);

                break;

            case 8:
                Medium_Orange_3Layer.gameObject.SetActive(bl);

                AnimatiorActivation(Medium_Orange_3Layer, Medium_Orange_2Layer_1, bl);

                if (bl)
                    NextModelOnOff(Medium_Blue_1NM, Medium_Blue_2NM);
                else
                    NextModelOnOff(Medium_Orange_1NM, Medium_Orange_2NM);
                break;

            case 9:
                Medium_Blue_3Layer.gameObject.SetActive(bl);

                AnimatiorActivation(Medium_Blue_3Layer, Medium_Orange_3Layer, bl);
                if (bl)
                    NextModelOnOff(Small_Turm_1NM, Small_Turm_2NM);
                else
                    NextModelOnOff(Medium_Blue_1NM, Medium_Blue_2NM);

                break;

            case 10:
                SmallTurm_3Layer.gameObject.SetActive(bl);
                NextModelOnOff(Small_Turm_1NM, Small_Turm_2NM);

                AnimatiorActivation(SmallTurm_3Layer, Medium_Blue_3Layer, bl);

                break;

            case 11:
                SmallTurm_3Layer_1.gameObject.SetActive(bl);
                NextModelOnOff(Small_Turm_1NM, Small_Turm_2NM);

                AnimatiorActivation(SmallTurm_3Layer_1, SmallTurm_3Layer, bl);

                break;

            case 12:
                SmallTurm_4Layer.gameObject.SetActive(bl);
                NextModelOnOff(Small_Turm_1NM, Small_Turm_2NM);

                AnimatiorActivation(SmallTurm_4Layer, SmallTurm_3Layer_1, bl);

                break;

            case 13:
                SmallTurm_4Layer_1.gameObject.SetActive(bl);

                AnimatiorActivation(SmallTurm_4Layer_1, SmallTurm_4Layer, bl);

                if (bl)
                    NextModelOnOff(null, null);
                else
                    NextModelOnOff(Small_Turm_1NM, Small_Turm_2NM);
                break;
            case 14:

                AnimatiorActivation(SmallTurm_4Layer_1, SmallTurm_4Layer_1, false);
                break;
            default:
                break;
        }
    }
   
    
    /// <summary>
    /// Hier wird nächste Teil aktiviert und vorherige Teil deaktiviert.
    /// </summary>
    /// <param name="trsfrm1">Oberste Objekt</param>
    /// <param name="trsfrm2">Unterste Objet</param>
    public void NextModelOnOff(Transform trsfrm1, Transform trsfrm2)
    {
        foreach (var item in _nextmodel)
        {
            item.gameObject.SetActive(false);
            item.Rotate(Vector3.up, 0.0f);
        }
        _nmTurn.Clear();

        if (trsfrm1 != null)
        {
            trsfrm1.gameObject.SetActive(true);
            trsfrm2.gameObject.SetActive(true);

            _nmTurn.Add(trsfrm1);
            _nmTurn.Add(trsfrm2);
        }
    }
}
