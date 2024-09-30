using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Level
{
    public class MusicAndAmbience : MonoBehaviour
    {
        enum MusicState { none, level, normalBattle, bossBattle }
        enum AmbienceState { none, exterior, interior }

        [SerializeField] AudioClip levelMusic;
        [SerializeField] AudioClip levelAmbience;
        [SerializeField] AudioClip interiorAmbience;
        [SerializeField] List<AudioClip> battleMusic;
        [SerializeField] AudioClip bossBattleMusic;
        [SerializeField] AudioClip gameOver;
        [SerializeField] bool startsWithExteriorAmbience = true;

        MusicState musicState = MusicState.none;
        AmbienceState ambienceState = AmbienceState.none;

        bool isGameover = false;

        void Update()
        {
            if (IBattleData.isGameOver)
            {
                if (!isGameover)
                {
                    isGameover = true;
                    MusicAndAmbienceManager.Instance.SetAndPlayMusic(gameOver);
                }
                return;
            }

            if (ITransformTeleporter.state == ITransformTeleporter.State.Interior && ambienceState != AmbienceState.interior)
            {
                ambienceState = AmbienceState.interior;
                MusicAndAmbienceManager.Instance.SetAndPlayAmbience(interiorAmbience);
            }
            else if (ITransformTeleporter.state == ITransformTeleporter.State.Exterior && ambienceState != AmbienceState.exterior)
            {
                ambienceState = AmbienceState.exterior;
                MusicAndAmbienceManager.Instance.SetAndPlayAmbience(levelAmbience);
            }


            if (IBattleData.isInBossBattle && musicState < MusicState.bossBattle)
            {
                musicState = MusicState.bossBattle;
                MusicAndAmbienceManager.Instance.SetAndPlayMusic(bossBattleMusic);
            }
            else if (IBattleData.isInBattle && musicState < MusicState.normalBattle)
            {
                musicState = MusicState.normalBattle;
                MusicAndAmbienceManager.Instance.SetAndPlayMusic(battleMusic[Random.Range(0, battleMusic.Count)]);
            }
            else if (!IBattleData.isInBattle && musicState != MusicState.level)
            {
                musicState = MusicState.level;
                MusicAndAmbienceManager.Instance.SetAndPlayMusic(levelMusic);
            }
        }  
    }
}