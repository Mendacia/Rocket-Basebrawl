using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class godsMixtape : MonoBehaviour
{
    [SerializeField] private AudioSource theCamera;
    [SerializeField] private List<AudioClip> theMixtape;
    [SerializeField] private Text uISongNameText;
    private AudioClip currentSong;
    private int selectedSongInt = 0;


    private void Start()
    {
        currentSong = theMixtape[selectedSongInt = Random.Range(0, theMixtape.Count)];
        theCamera.clip = currentSong;
        theCamera.Play(0);
        uISongNameText.text = ("Playing: " + theMixtape[selectedSongInt].name);
    }

    private void Update()
    {
        if (!theCamera.isPlaying)
        {
            int newSelectedSongThatCannotBeTheSameAsSelectedSongInt = Random.Range(0, theMixtape.Count);
            while(newSelectedSongThatCannotBeTheSameAsSelectedSongInt == selectedSongInt)
            {
                //IT CANNOT BE THE SAME
                newSelectedSongThatCannotBeTheSameAsSelectedSongInt = Random.Range(0, theMixtape.Count);
            }
            selectedSongInt = newSelectedSongThatCannotBeTheSameAsSelectedSongInt;
            currentSong = theMixtape[selectedSongInt];
            theCamera.clip = currentSong;
            theCamera.Play(0);
            uISongNameText.text = ("Playing: " + theMixtape[selectedSongInt].name);
        }
    }
}
