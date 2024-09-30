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
    [SerializeField] AssetLabelReference obtainAllReference;

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
    [SerializeField] AudioClip notification;

    List<AudioClip> equip = new List<AudioClip>();
    List<AudioClip> addScroll = new List<AudioClip>();
    List<AudioClip> unequip = new List<AudioClip>();
    List<AudioClip> obtain = new List<AudioClip>();
    List<AudioClip> obtainAll = new List<AudioClip>();

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
        Addressables.LoadAssetsAsync<AudioClip>(obtainAllReference, (i) =>
        {
            obtainAll.Add(i);

        }).WaitForCompletion();
    }

    public void PlayOptionsMenuOpen()
    {
        audioSourceManager.PlayUI(optionsMenuOpen);
    }

    public void PlayOptionsMenuClose()
    {
        audioSourceManager.PlayUI(optionsMenuClose);
    }

    public void PlayEquipmentMenuOpen()
    {
        audioSourceManager.PlayUI(equipmentMenuOpen);
    }

    public void PlayChangeEquipmentPart()
    {
        audioSourceManager.PlayUI(changeEquipmentPart);
    }

    public void PlayEquipmentMenuClose()
    {
        audioSourceManager.PlayUI(equipmentMenuClose);
    }

    public void PlayCyclePartyMembers()
    {
        audioSourceManager.PlayUI(cyclePartyMembers);
    }

    public void PlayGenericOpen()
    {
        audioSourceManager.PlayUI(generalOpen);
    }

    public void PlayGenericClose()
    {
        audioSourceManager.PlayUI(generalClose);
    }

    public void PlayHover()
    {
        audioSourceManager.PlayUI(hover);
    }

    public void PlayClick()
    {
        audioSourceManager.PlayUI(click);
    }

    public void PlayNotification()
    {
        audioSourceManager.PlayUI(notification);
    }

    public void PlayEquip()
    {
        audioSourceManager.PlayUI(equip);
    }

    public void PlayUnequip()
    {
        audioSourceManager.PlayUI(unequip);
    }

    public void PlayObtainSFX()
    {
        audioSourceManager.PlayUI(obtain);
    }

    public void PlayObtainAllSFX()
    {
        audioSourceManager.PlayUI(obtainAll);
    }

    public void PlayAddScroll()
    {
        audioSourceManager.PlayUI(addScroll);
    }
}
