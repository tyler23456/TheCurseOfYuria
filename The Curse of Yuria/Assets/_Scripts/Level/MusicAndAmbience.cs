using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Level
{
    public class MusicAndAmbience : MonoBehaviour
    {
        enum MusicState { level, battle }

        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource ambienceSource;

        [SerializeField] AudioClip levelMusic;
        [SerializeField] AudioClip levelAmbience;
        [SerializeField] List<AudioClip> battleMusic;

        MusicState musicState = MusicState.level;

        void Update()
        {
            if (IBattleData.isInBattle && musicState == MusicState.level)
            {
                musicState = MusicState.battle;
                musicSource.clip = battleMusic[Random.Range(0, battleMusic.Count)];
                musicSource.Play();
            }
            else if (!IBattleData.isInBattle && musicState == MusicState.battle)
            {
                musicState = MusicState.level;
                musicSource.clip = levelMusic;
                musicSource.Play();
            }
           
        }

        
    }
}