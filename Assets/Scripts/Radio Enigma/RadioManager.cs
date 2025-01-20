using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RadioManager : MonoBehaviour, IInteractable
{
    [SerializeField] private CursorManager cursorManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject morseText;

    [SerializeField] private AudioSource songSource;
    [SerializeField] private AudioSource sizzleSource;
    [SerializeField] private AudioSource morseSource;

    [SerializeField] private AudioClip song;
    [SerializeField] private AudioClip sizzle;
    [SerializeField] private AudioClip morse;

    private bool isRadioOn = false;
    private Coroutine morseCoroutine;

    [SerializeField] private float morseButton = 0.5f;
    [SerializeField] private float songButton = 0.5f;
    [SerializeField] private float sizzleButton = 0.5f;


    void Update()
    {
        if(!songSource.isPlaying) 
        {
            sizzleSource.Stop();
            morseSource.Stop();
        }
    }

    public void OnHoverEnter()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnHoverExit()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    public void ChangeView(Camera camera, GameObject player)
    {
        player.SetActive(false);
        camera.transform.SetParent(gameObject.transform);

        camera.transform.localPosition = new Vector3(0f, -0.085f, 0.02f);
        camera.transform.localRotation = Quaternion.Euler(-90, -90, -90);

        cursorManager.isSpecificView = true;
        cursorManager.UnlockCursor();
    }

    public void ResetView(Camera camera, GameObject player)
    {
        player.SetActive(true);
        camera.transform.SetParent(player.transform);

        camera.transform.localPosition = new Vector3(0f, 2.0f, 0f);

        cursorManager.isSpecificView = false;
        cursorManager.LockCursor();
    }

    public bool Interact()
    {
        if(!isRadioOn)
        {
            songSource.clip = song;
            songSource.Play();

            isRadioOn = true;   

            if(sizzle.length < song.length)
            {
                sizzleSource.clip = sizzle;
                sizzleSource.loop = true;
                sizzleSource.Play();
            }

            morseCoroutine = StartCoroutine(PlayMorse());
        }
        else
        {
            songSource.Stop();
            isRadioOn = false;

        }


        return false;
    }

    public IEnumerator PlayMorse()
    {
        yield return new WaitForSeconds(73.0f);

        morseSource.clip = morse;
        morseSource.Play();
    }

    public void UpdateButtonValue(string buttonName, float value)
    {
        switch(buttonName)
        {
            case "MorseButton":
                morseButton = value;
                break;
            case "SongButton":
                songButton = value;
                break;
            case "SizzleButton":
                sizzleButton = value;
                break;
        }

        UpdateAudioMix();
    }

    public void UpdateAudioMix()
    {
        songSource.volume = songButton;
        morseSource.volume = morseButton;
        sizzleSource.volume = sizzleButton;

        if(songButton < 0.2f && sizzleButton < 0.2f && morseButton >= 0.9f)
        {
            if(morseCoroutine != null) StopCoroutine(morseCoroutine);

            if (morseSource.isPlaying)
            {
                morseSource.Stop();
            }

            morseSource.clip = morse;
            morseSource.Play();

            inventoryManager.AddItemToInventory(morseText.GetComponent<IItem>());
        }
    }


}