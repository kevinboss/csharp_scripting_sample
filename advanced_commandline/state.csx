#load "enemy.csx"

State currentState = null;

void SetUpState() {
    currentState = new State {
        PlayerLife = 20,
        DistanceToTarget = 50
    };
    Console.WriteLine("New game has begun");
}
SetUpState();

class State {
    public Enemy CurrentEnemy;
    public int PlayerLife;
    public int DistanceToTarget;
}