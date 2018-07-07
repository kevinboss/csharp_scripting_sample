#load "state.csx"

var runRnd = new Random();
var attackRnd = new Random();
var enemyRnd = new Random();

void Walk() {
    if (currentState.CurrentEnemy != null) {
        Console.WriteLine("You are in a fight");
        return;
    }
    currentState.DistanceToTarget -= 2;
    Console.WriteLine($"Distance to target: {currentState.DistanceToTarget}");
    MaybeCreateEnemy();
}

void Eat() {
    if (currentState.CurrentEnemy != null) {
        Console.WriteLine("You are in a fight");
        return;
    }
    currentState.PlayerLife += 1;
    Console.WriteLine($"Player life: {currentState.PlayerLife}");
    MaybeCreateEnemy();
}
void Run() {
    if (currentState.CurrentEnemy == null) {
        Console.WriteLine("You are not in a fight");
        return;
    }
    var pick = runRnd.Next(0, 10);
    if (pick >= 8) {
        currentState.PlayerLife = 0;
    } else {
        currentState.CurrentEnemy = null;
        Console.WriteLine("You got away");
    }
}
void Attack() {
    if (currentState.CurrentEnemy == null) {
        Console.WriteLine("You are not in a fight");
        return;
    }
    currentState.CurrentEnemy.Defense -= 1;
    if (currentState.CurrentEnemy.Defense <= 0) {
        currentState.CurrentEnemy = null;
        Console.WriteLine("Enemy has been destroyed");
        return;
    }
    var pick = attackRnd.Next(0, 5);
    if (pick >= 3) {
        currentState.PlayerLife -= currentState.CurrentEnemy.Attack;
    Console.WriteLine($"Player life: {currentState.PlayerLife}");
    }
}

void MaybeCreateEnemy() {
    var pick = enemyRnd.Next(0, 10);
    if (pick >= 6) {
        var newEnemy = EnemyFactory.CreateEnemy();
        Console.WriteLine($"A {newEnemy.Name} appeared with an attack value of {newEnemy.Attack} and a defense of {newEnemy.Defense}");
        Console.WriteLine("You can attack [attack] or try to run [run], but be aware that if you run and fail, you will loose the game");
        currentState.CurrentEnemy = newEnemy;
    }
}

bool EvaluateGameEnded() {
    if (currentState.DistanceToTarget <= 0) {
        Console.WriteLine("You are in safety");
        return true;
    }
    if (currentState.PlayerLife <= 0) {
        Console.WriteLine("You died");
        return true;
    }
    return false;
}