using System;
using System.Threading;

Character jogador = new Character();
Goblin goblin = new Goblin();

while (true)
{
    Console.WriteLine(jogador);
    Console.WriteLine(goblin);
    jogador.AttackTarget(goblin);
    goblin.AttackTarget(jogador);
    Thread.Sleep(1000);
}

public class Status
{
    public Status(int min, int max)
    {
        this.Min = min;
        this.Max = max;
        this.current = max;
    }

    public int Max { get; set; }
    public int Min { get; set; }
    public Event OnMinValueReached { get; set; }
    public EventArgs EventArgs { get; set; }

    private int current = 0;
    public int Current
    {
        get => current;
        set
        {
            if (value > Max)
                current = Max;
            else if (value <= Min)
            {
                current = Min;
                OnMinValueReached.OnTrigger(EventArgs);
            }
            else current = value;
        }
    }
}

public abstract class Event
{
    public abstract void OnTrigger(EventArgs args);
}

public class EventArgs
{

}

public class DeathEventArgs : EventArgs
{
    public Entity DeadEntity { get; set; }
}

public class DeathEvent : Event
{
    public override void OnTrigger(EventArgs args)
    {
        DeathEventArgs e = (DeathEventArgs)args;
        e.DeadEntity.IsAlive = false;
    }
}

public abstract class Entity
{
    public bool IsAlive { get; set; } = true;
    public Status HP { get; set; }
    public int Attack { get; set; }
    public int Armor { get; set; }
    public int CriticalChance { get; set; }
    public int LifeSteal { get; set; }

    public void AttackTarget(Entity target)
    {
        int damage = Attack;
        if (isCritical())
        {
            damage *= 2;
        }
        int posMitigationDamage = 
            target.ReciveDamage(damage);
        int heal = (int)(posMitigationDamage * LifeSteal / 100f);
        this.HP.Current += heal;
    }

    public int ReciveDamage(int damage)
    {
        int posMitigationDamage = 
            (int)(damage * 100f / (100f + Armor));
        this.HP.Current -= posMitigationDamage;
        return posMitigationDamage;
    }

    private bool isCritical()
    {
        int seed = (int)DateTime.Now.Ticks;
        Random rand = new Random(seed);
        int value = rand.Next(0, 100);
        return value < CriticalChance;
    }
}

public class Character : Entity
{
    public Character()
    {
        HP = new Status(0, 100);
        Attack = 10;
        CriticalChance = 5;
        LifeSteal = 10;
        Armor = 20;
        HP.OnMinValueReached = new DeathEvent();
        DeathEventArgs args = new DeathEventArgs();
        args.DeadEntity = this;
        HP.EventArgs = args; 
    }

    public Status Mana { get; set; }

    public override string ToString()
        => $"Jogador HP: {HP.Current} / {HP.Max}";
}

public abstract class Enemy : Entity
{
    
}

public class Goblin : Enemy
{
    public Goblin()
    {
        HP = new Status(0, 50);
        Attack = 5;
        CriticalChance = 1;
        Armor = 0;
        HP.OnMinValueReached = new DeathEvent();
    }

    public override string ToString()
        => $"Goblin HP: {HP.Current} / {HP.Max}";
}