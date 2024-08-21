using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Level
{
    public class MusicAndAmbience : MonoBehaviour
    {
        enum MusicState { level, battle }
        enum AmbienceState { exterior, interior }

        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource ambienceSource;

        [SerializeField] AudioClip levelMusic;
        [SerializeField] AudioClip levelAmbience;
        [SerializeField] AudioClip interiorAmbience;
        [SerializeField] List<AudioClip> battleMusic;

        MusicState musicState = MusicState.level;
        AmbienceState ambienceState = AmbienceState.exterior;

        void Update()
        {
            if (ITransformTeleporter.state == ITransformTeleporter.State.Interior && ambienceState == AmbienceState.exterior)
            {
                ambienceState = AmbienceState.interior;
                ambienceSource.clip = interiorAmbience;
                ambienceSource.Play();
            }
            else if (ITransformTeleporter.state == ITransformTeleporter.State.Exterior && ambienceState == AmbienceState.interior)
            {
                ambienceState = AmbienceState.exterior;
                ambienceSource.clip = levelAmbience;
                ambienceSource.Play();
            }

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