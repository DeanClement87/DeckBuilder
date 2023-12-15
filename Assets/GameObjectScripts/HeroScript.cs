using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroScript : MonoBehaviour
{
    private GameManager gameManager;
    public HeroModel HeroModel { get; set; }
    public TextMeshProUGUI blockText;
    public TextMeshProUGUI thornsText;

    private bool initParticles = false;
    private GameObject initParticlesObject;

    private Image heroImage;

    void Awake()
    {
        gameManager = GameManager.Instance;
        heroImage = GetComponent<Image>();
    }

    public void SetHeroData(HeroModel hero)
    {
        HeroModel = hero;

        Sprite cardSprite = Resources.Load<Sprite>(HeroModel.HeroBoardArt);
        heroImage.sprite = cardSprite;
    }

    void Update()
    {
        if (HeroModel == null) return;

        blockText.text = HeroModel.Block.ToString();
        thornsText.text = HeroModel.Thorns.ToString();

        if (HeroModel.Health <= 0)
        {
            HeroModel.HeroDeath();
        }

        if (HeroModel.heroEnum == HeroEnum.Rogue && HeroModel.Initiative == true && initParticles == false)
        {
            var result = ParticleHelper.PerformParticleSequence(ParticleEnum.Initiative,
                ParticleBehavourEnum.OnTarget,
                gameObject);

            initParticlesObject = result.particleObject;

            initParticles = true;
        }

        if (HeroModel.heroEnum == HeroEnum.Rogue && HeroModel.Initiative == false && initParticles == true)
        {
            Destroy(initParticlesObject);

            initParticles = false;
        }
    }
}
