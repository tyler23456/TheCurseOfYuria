using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using FirstGearGames.SmoothCameraShaker;
using HeroEditor.Common.Enums;
using System;

namespace TCOY.UserActors
{
    [RequireComponent(typeof(BoxCollider2D), typeof (CoroutineBehaviour))]
    
    public class Actor : MonoBehaviour, IActor
    {
        [SerializeField] protected bool _useDefaultItems = true;
        [SerializeField] protected Stats stats;
        [SerializeField] protected ATBGuage aTBGuage;
        [SerializeField] protected List<Reactor> counters;
        [SerializeField] protected List<Reactor> interrupts;
      
        protected CoroutineBehaviour coroutineBehaviour;
        protected DefaultItems editorEquipper;
        protected new Collider2D collider2D;
        protected SpriteRenderer[] spriteRenderers;
        protected GroundChecker groundChecker;
        protected Inventory equipment;
        protected Inventory skills;
        protected StatusEffects statusEffects;
        protected HitAnimator hitAnimator;
        protected FadeAnimator fadeAnimator;
        protected SpriteFlipper spriteFlipper;

        public Collider2D getCollider2D => collider2D;
        public GameObject obj => gameObject;
        public IStats getStats => stats;
        public IATBGuage getATBGuage => aTBGuage;
        public IInventory getEquipment => equipment;
        public IInventory getScrolls => skills;
        public IStatusEffects getStatusEffects => statusEffects;
        public HitAnimator getHitAnimator => hitAnimator;
        public IFadeAnimator getFadeAnimator => fadeAnimator;
        public ISpriteFlipper getSpriteFlipper => spriteFlipper;
        public List<Reactor> getCounters => counters;
        public List<Reactor> getInterrupts => interrupts;

        public bool useDefaultItems { get { return _useDefaultItems; } set { _useDefaultItems = value; } }

        protected void Awake()
        {
            coroutineBehaviour = GetComponent<CoroutineBehaviour>();
            editorEquipper = GetComponent<DefaultItems>();
            collider2D = GetComponent<Collider2D>();
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            aTBGuage = new ATBGuage();
            groundChecker = new GroundChecker(gameObject);
            equipment = new Inventory();
            skills = new Inventory();
            
            statusEffects = new StatusEffects();
            statusEffects.onAdd = (name) => StatFXDatabase.Instance.Get(name).OnAdd(this);
            statusEffects.onUpdate = (name) => { };
            statusEffects.onRemove = (name) => StatFXDatabase.Instance.Get(name).OnRemove(this);

            hitAnimator = new HitAnimator(this, spriteRenderers);
            fadeAnimator = new FadeAnimator(this, spriteRenderers);
            spriteFlipper = new SpriteFlipper(spriteRenderers);

            stats.Initialize();
            stats.onHPDamage = (damage) => { }; //play a hit soundFX
            stats.onHPDamage += (damage) => PopupManager.Instance.AddHPDamagePopup(damage, collider2D.bounds.center);
            stats.onHPDamage += (damage) => CameraShakerHandler.Shake(ShakeDatabase.Instance.Get("Hit"));
            stats.onHPDamage += (damage) => hitAnimator.Start();

            stats.onZeroHealth = () => StatFXDatabase.Instance.Get("KnockOut").Activate(this);

            stats.onHPRecovery = (recovery) => PopupManager.Instance.AddHPRecoveryPopup(recovery, collider2D.bounds.center);

            stats.onMPDamage = (damage) => PopupManager.Instance.AddMPDamagePopup(damage, collider2D.bounds.center);

            stats.onMPRecovery = (recovery) => PopupManager.Instance.AddMPRecoveryPopup(recovery, collider2D.bounds.center);
        }

        protected void Start()
        {
            if (editorEquipper == null || _useDefaultItems == false)
                return;

            ItemBase[] items = editorEquipper.GetDefaultItems();

            foreach (ItemBase item in items)
                item.Equip(this);
        }


        protected void Update()
        {
            if (!GameStateManager.Instance.isPlaying)
                return;

            aTBGuage.Update();
            groundChecker.Update();
            statusEffects.Update();
        }
    }
}