namespace UI
{
	public class StaminaDrainUI : StatBarUI
	{
		protected override void Awake()
		{
			base.Awake();
			slider.maxValue = stats.maxStamina;

		}
		
		protected override void Start()
		{
			base.Start();
			slider.maxValue = stats.maxStamina;

		}
		private void OnEnable()
		{
			stats.OnStaminaChanged += SetCurrentStat;
			stats.OnDeath += SetEmpty;
		}

		private void OnDisable()
		{
			stats.OnStaminaChanged -= SetCurrentStat;
			stats.OnDeath += SetEmpty;

		}
	}
}