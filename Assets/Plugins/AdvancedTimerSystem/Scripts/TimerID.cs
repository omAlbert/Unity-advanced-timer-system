public static class TimerID
{
    public static class Jump
    {
        public const string Buffer = "Jump.Buffer";
        public const string Coyote = "Jump.Coyote";
        public const string lastPressedAirJumpTime = "Jump.lastPressedAirJumpTime";

        public const string LastOnWallTime = "Move.LastOnWallTime";
        public const string LastOnWallRightTime = "Move.LastOnWallRightTime";
        public const string LastOnWallLeftTime = "Move.LastOnWallLeftTime";
        
        public const string LastPressedAirJumpTime = "Move.LastPressedAirJumpTime";
    }

    public static class Dash
    {
        public const string Cooldown = "Dash.Cooldown";
        public const string Buffer = "Dash.Buffer";
        public const string Performing = "Dash.Perform";
    }

    public static class Damage
    {
        public const string Invuln = "Damage.Invulnerability";
        public const string Stun = "Damage.Stun";
        public const string Knockback = "Damage.Knockback";
    }

    public static class Glide
    {
        public const string Stamina = "Glide.Stamina";
    }

    public static class Movement
    {
        public const string FallTime = "Move.FallTime";
        public const string KnockbackOnHit = "Move.KnocbackOnhit";
    }

    public static class LookAround
    {
        public const string LookUp = "Move.LookUp";
        public const string LookDown = "Move.LookDown";
    }

    public static class Attacks
    {
        public const string ThrowCooldown = "Attacks.ThrowCooldown";
        public const string HoldTime = "Attacks.HoldTime";
    }
}
