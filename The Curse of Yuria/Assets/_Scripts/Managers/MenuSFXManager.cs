using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

public class MenuSFXManager : MonoBehaviour
{
    public static MenuSFXManager Instance { get; private set; }

    [SerializeField] AssetLabelReference equipReference;
    [SerializeField] AssetLabelReference unequipReference;

    [SerializeField] AudioSourceManager audioSourceManager;

    [SerializeField] AudioClip mainMenuOpen;
    [SerializeField] AudioClip mainMenuClose;
    [SerializeField] AudioClip equipmentMenuOpen;
    [SerializeField] AudioClip equipmentMenuClose;
    [SerializeField] AudioClip generalOpen;
    [SerializeField] AudioClip generalClose;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;

    List<AudioClip> equip;
    List<AudioClip> unequip;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Addressables.LoadAssetsAsync<AudioClip>(equipReference, (i) =>
        {
            equip.Add(i);

        }).WaitForCompletion();
        Addressables.LoadAssetsAsync<AudioClip>(unequipReference, (i) =>
        {
            unequip.Add(i);

        }).WaitForCompletion();
    }

    public void PlayMainMenuOpen()
    {
        audioSourceManager.PlaySFX(mainMenuOpen);
    }

    public void PlayMainMenuClose()
    {
        audioSourceManager.PlaySFX(mainMenuClose);
    }

    public void PlayEquipmentMenuOpen()
    {
        audioSourceManager.PlaySFX(equipmentMenuOpen);
    }

    public void PlayChangeEquipmentPart()
    {
        
    }

    public void PlayEquipmentMenuClose()
    {
        audioSourceManager.PlaySFX(equipmentMenuClose);
    }

    public void PlayCyclePartyMembers()
    {

    }

    public void PlayGenericOpen()
    {
        audioSourceManager.PlaySFX(generalOpen);
    }

    public void PlayGenericClose()
    {
        audioSourceManager.PlaySFX(generalClose);
    }

    public void PlayHover()
    {
        audioSourceManager.PlaySFX(hover);
    }

    public void PlayClick()
    {
        audioSourceManager.PlaySFX(click);
    }

    public void PlayEquip()
    {
        audioSourceManager.PlaySFX(equip);
    }

    public void PlayUnequip()
    {
        audioSourceManager.PlaySFX(unequip);
    }
}
