static class EnemyFactory {
    private static Random rnd = new Random();
    private static readonly IList<Enemy> enemies = new List<Enemy>{
        new Enemy {Name="Skeleton", Attack=1, Defense=1},
        new Enemy {Name="Skeleton", Attack=2, Defense=1},
        new Enemy {Name="Skeleton", Attack=1, Defense=2},
        new Enemy {Name="Skeleton", Attack=2, Defense=2},
        new Enemy {Name="Dragon", Attack=4, Defense=10}
    };

    public static Enemy CreateEnemy() {
        var pick = rnd.Next(0, enemies.Count);
        return enemies[pick].Clone();
    }
}

class Enemy {
    public string Name;
    public int Attack;
    public int Defense;

    public Enemy Clone() {
        var newEnemy = new Enemy();
        newEnemy.Name = this.Name;
        newEnemy.Attack = this.Attack;
        newEnemy.Defense = this.Defense;
        return newEnemy;
    }
}