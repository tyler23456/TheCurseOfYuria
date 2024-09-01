using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCOY.Level
{
    public class MusicAndAmbience : MonoBehaviour
    {
        enum MusicState { level, battle, gameOver, none }
        enum AmbienceState { exterior, interior, none }

        [SerializeField] AudioClip levelMusic;
        [SerializeField] AudioClip levelAmbience;
        [SerializeField] AudioClip interiorAmbience;
        [SerializeField] List<AudioClip> battleMusic;
        [SerializeField] AudioClip gameOver;

        MusicState musicState = MusicState.none;
        AmbienceState ambienceState = AmbienceState.none;

        void Update()
        {
            if (IBattleData.isGameOver)
            {
                if (musicState != MusicState.gameOver)
                {
                    musicState = MusicState.gameOver;
                    MusicAndAmbienceManager.Instance.SetAndPlayMusic(gameOver);
                }
                return;
            }

            if (!GameStateManager.Instance.isPlaying)
                return;

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

            if (IBattleData.isInBattle && musicState != MusicState.battle)
            {
                musicState = MusicState.battle;
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