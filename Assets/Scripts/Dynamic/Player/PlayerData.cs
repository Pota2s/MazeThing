using System;
public enum PlayerState
{
    Attacking,
    Defending,
    Shooting,
    Running
}
public class PlayerData
{
    public event Action<int> OnHealthChanged;
    public event Action<PlayerState> OnStateChanged;
    public event Action<int> OnShieldChanged;
    public event Action<int> OnArrowChanged;
    private PlayerState state;

    private int health;
    public PlayerState State
    {
        get { return state; }
        set {
            if (state == value) return;

            state = value;
            OnStateChanged?.Invoke(state);
        }
    }
    public int Health { get => health; set {
            if (health == value) return;
            int clamped = Math.Clamp(health, 0, MaxHealth);
            health = clamped;
            OnHealthChanged?.Invoke(clamped);
        } 
    }
    public int MaxHealth { get; set; }
    
    private int shields;
    public int Shields { get => shields; 
        set { 
            if (shields == value) return;
            shields = value;
            OnShieldChanged?.Invoke(value);
        } 
    }
    private int arrows;
    public int Arrows { get => arrows; set
        {
            if(arrows == value) return;
            arrows = value;
            OnArrowChanged?.Invoke(value);
        } 
    }

    public PlayerData()
    {
        MaxHealth = 5;
        Health = MaxHealth;
        Shields = 0;
        Arrows = 0;
        State = PlayerState.Attacking; 
    }
}
