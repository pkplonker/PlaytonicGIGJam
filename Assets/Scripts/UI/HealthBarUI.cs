namespace UI
{
	public class HealthBarUI : StatBarUI
	{
		protected override void Awake()
		{
			base.Awake();

		}

		protected override void Start()
		{
			base.Start();
			slider.maxValue = stats.maxHealth;

		}

		protected virtual void OnEnable()
		{
			stats.OnHealthChanged += SetCurrentStat;
			stats.OnDeath += Death;
		}

		protected virtual void OnDisable()
		{
			stats.OnHealthChanged -= SetCurrentStat;
			stats.OnDeath += Death;
		}
		
	}
}