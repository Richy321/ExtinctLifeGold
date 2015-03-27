using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnClickShowInfo : MonoBehaviour 
{
    public GameObject infoPanel;
    public Creature creature;
    public Text geneList;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnMouseDown()
    {
        if (infoPanel.activeSelf)
        {
            infoPanel.SetActive(false);
        }
        else
        {
            infoPanel.SetActive(true);
            UpdatePanel();
        }   
    }


    void UpdatePanel()
    {
        geneList.text = string.Empty;
        foreach (CreatureGene.GeneFlags geneFlag in System.Enum.GetValues(typeof(CreatureGene.GeneFlags)))
        {
            if (geneFlag != CreatureGene.GeneFlags.LastEntry)
            {
                if ((creature.chromosome & (int)geneFlag) == (int)geneFlag)
                {
                    geneList.text += Creature.GetGeneFromFlag(geneFlag).description + System.Environment.NewLine;
                }
            }
        }
    }
}
