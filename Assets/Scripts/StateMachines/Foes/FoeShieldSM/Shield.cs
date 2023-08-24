namespace StateMachines.Foes.FoeShieldSM
{
    public class Shield
    {
        public float MaxShieldStrength { get; private set; }
        public float CurrentShieldStrength { get; private set; }
        public bool IsShieldBroken { get; private set; }

        public Shield(float maxShieldStrength)
        {
            MaxShieldStrength = maxShieldStrength;
            CurrentShieldStrength = maxShieldStrength;
            IsShieldBroken = false;
        }

        public void TakeDamage(float damage)
        {
            if (IsShieldBroken) return;

            CurrentShieldStrength -= damage;

            if (CurrentShieldStrength <= 0)
            {
                IsShieldBroken = true;
                CurrentShieldStrength = 0;
            }
        }
    }

}