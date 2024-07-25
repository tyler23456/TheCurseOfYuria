using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

public class MenuSFXManager : MonoBehaviour
{
    public static MenuSFXManager Instance { get; private set; }

    [SerializeField] AssetLabelReference equipReference;
    [SerializeField] AssetLabelReference addScrollReference;
    [SerializeField] AssetLabelReference unequipReference;
    [SerializeField] AssetLabelReference obtainReference;

    [SerializeField] AudioSourceManager audioSourceManager;

    [SerializeField] AudioClip optionsMenuOpen;
    [SerializeField] AudioClip optionsMenuClose;
    [SerializeField] AudioClip equipmentMenuOpen;
    [SerializeField] AudioClip equipmentMenuClose;
    [SerializeField] AudioClip generalOpen;
    [SerializeField] AudioClip generalClose;
    [SerializeField] AudioClip cyclePartyMembers;
    [SerializeField] AudioClip changeEquipmentPart;
    [SerializeField] AudioClip hover;
    [SerializeField] AudioClip click;

    List<AudioClip> equip = new List<AudioClip>();
    List<AudioClip> addScroll = new List<AudioClip>();
    List<AudioClip> unequip = new List<AudioClip>();
    List<AudioClip> obtain = new List<AudioClip>();

    void Awake()
    {
        Instance = this;

        Addressables.LoadAssetsAsync<AudioClip>(equipReference, (i) =>
        {
            equip.Add(i);

        }).WaitForCompletion();
        Addressables.LoadAssetsAsync<AudioClip>(addScrollReference, (i) =>
        {
            addScroll.Add(i);

        }).WaitForCompletion();
        Addressables.LoadAssetsAsync<AudioClip>(unequipReference, (i) =>
        {
            unequip.Add(i);

        }).WaitForCompletion();
        Addressables.LoadAssetsAsync<AudioClip>(obtainReference, (i) =>
        {
            obtain.Add(i);

        }).WaitForCompletion();
    }

    public void PlayOptionsMenuOpen()
    {
        audioSourceManager.PlaySFX(optionsMenuOpen);
    }

    public void PlayOptionsMenuClose()
    {
        audioSourceManager.PlaySFX(optionsMenuClose);
    }

    public void PlayEquipmentMenuOpen()
    {
        audioSourceManager.PlaySFX(equipmentMenuOpen);
    }

    public void PlayChangeEquipmentPart()
    {
        audioSourceManager.PlaySFX(changeEquipmentPart);
    }

    public void PlayEquipmentMenuClose()
    {
        audioSourceManager.PlaySFX(equipmentMenuClose);
    }

    public void PlayCyclePartyMembers()
    {
        audioSourceManager.PlaySFX(cyclePartyMembers);
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

    public void PlayObtainSFX()
    {
        audioSourceManager.PlaySFX(obtain);
    }

    public void PlayAddScroll()
    {
        audioSourceManager.PlaySFX(addScroll);
    }
}
