using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPanel : MonoBehaviour
{

    public Image photoImage;
    public Text description;
    public StoryData storyData;

    // Start is called before the first frame update
    void Start()
    {
        setStoryData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changePanel()
    {
        if (storyData.nextStoryData != null)
        {
            storyData = storyData.nextStoryData;
            setStoryData();
        } else
        {
            gameObject.SetActive(false);
        }
    }

    private void setStoryData()
    {
        photoImage.sprite = storyData.photo;
        description.text = storyData.description;
    }
}
