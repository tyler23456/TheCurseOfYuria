using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimator
{
    bool isPerformingCommand { get; }
    public void Stand();
    public void Walk();
    public void Run();
    public void Jump();
    public void Crouch();
    public void Climb();
    public void KO();
    public void UseSupply();
    public void Ready();
    public void Unready();
    public void Attack();
    public void Cast();
    public void Hit();
    public void SetWeaponType(int type);
}
