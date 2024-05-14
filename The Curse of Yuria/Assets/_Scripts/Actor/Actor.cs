using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.Scripts.CharacterScripts;
using FirstGearGames.SmoothCameraShaker;

namespace TCOY.Actors
{
    public class Actor : MonoBehaviour, IActor
    {
        protected IGlobal global;
        protected IFactory factory;

        [SerializeField] protected Camera camera;
        [SerializeField] protected SpriteRenderer[] spriteRenderers;
        [SerializeField] protected new Collider2D collider2D;
        [SerializeField] protected Position position;
        [SerializeField] protected Climber climber;
        [SerializeField] protected Rotation rotation;
        [SerializeField] protected Stats stats;
        [SerializeField] protected ATBGuage aTBGuage;
        [SerializeField] protected GroundChecker groundChecker;
        [SerializeField] protected Character character;
        [SerializeField] protected Inventory equipment;
        [SerializeField] protected string attack;
        [SerializeField] protected Inventory skills;
        [SerializeField] protected UserAnimator animator;
        [SerializeField] protected StatusEffects statusEffects;

        public Collider2D getCollider2D => collider2D;
        public GameObject getGameObject => gameObject;
        public IPosition getPosition => position;
        public IClimber getClimber => climber;
        public IRotation getRotation => rotation;
        public IStats getStats => stats;
        public IATBGuage getATBGuage => aTBGuage;
        public Character getCharacter => character;
        public IInventory getEquipment => equipment;
        public string getAttack => attack;
        public IInventory getSkills => skills;
        public IAnimator getAnimator => animator;
        public IStatusEffects getStatusEffects => statusEffects;

        public List<IReactor> counters { get; private set; } = new List<IReactor>();
        public List<IReactor> interrupts { get; private set; } = new List<IReactor>();

        protected void Start()
        {
            GameObject obj = GameObject.Find("/DontDestroyOnLoad");
            global = obj.GetComponent<IGlobal>();
            factory = obj.GetComponent<IFactory>();

            stats.Initialize();

            stats.onApplyDamage = (damage) => animator.Hit();
            stats.onApplyDamage += (damage) => { }; //play a hit soundFX
            stats.onApplyDamage += (damage) => 
            {
                Vector3 position = transform.position + Vector3.up * 0.5f;
                position = global.getCamera.WorldToScreenPoint(position);
                GameObject obj = Instantiate(factory.getDamageTextPrefab, position, Quaternion.identity, global.getCanvas);
                obj.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = damage.ToString();
                Destroy(obj, 2f);
            };
            stats.onApplyDamage += (damage) => CameraShakerHandler.Shake(global.getShakeData);
            stats.onApplyDamage += (damage) => global.StartCoroutine(HitAnimation());
            stats.onZeroHealth = () => factory.GetStatusEffect("KnockedOut").Activate(this);

            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            statusEffects.Initialize(factory);
            statusEffects.onAdd = (name) => factory.GetStatusEffect(name).OnAdd(this);
            statusEffects.onUpdate = (name) => { };
            statusEffects.onRemove = (name) => factory.GetStatusEffect(name).OnRemove(this);
        }

        protected void FixedUpdate()
        {

        }

        protected void Update()
        {
            climber.Update();
            rotation.Update();
            aTBGuage.Update();
            groundChecker.Update();
            statusEffects.Update();
        }

        protected void LateUpdate()
        {
            if (camera != null)
                camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + new Vector3(0f, 0f, -1f), 0.3f);
        }

        protected IEnumerator HitAnimation()
        {
            float accumulator = Time.time;
            while(Time.time < accumulator + 0.5f)
            {
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(0.05f);
            }
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                spriteRenderer.enabled = true;
        }
    }
}