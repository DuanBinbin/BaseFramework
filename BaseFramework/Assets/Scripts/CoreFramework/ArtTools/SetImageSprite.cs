using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SetImageSprite : MonoBehaviour {

    public Sprite _SrcSp;
    public Sprite _DestSp;

    public bool _Do = false;
	// Use this for initialization
    [ExecuteInEditMode]
	void Update () {

       if(_Do)
        {
            Do();
            _Do = false;
        }

	}

    [ExecuteInEditMode]
    void Do()
    {
        Image[] arr = gameObject.GetComponentsInChildren<Image>(true);

        foreach (Image image in arr)
        {
            if (image.sprite == _SrcSp)
            {
                image.sprite = _DestSp;
            }
        }
    }
	
}
