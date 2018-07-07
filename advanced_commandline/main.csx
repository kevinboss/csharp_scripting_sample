#! "netcoreapp2.1"
#r "nuget: McMaster.Extensions.CommandLineUtils, 2.2.5"
#load "actions.csx"

using McMaster.Extensions.CommandLineUtils;
var app = new CommandLineApplication(throwOnUnexpectedArg: false)
{
    ExtendedHelpText = "Hello"
};

app.Command("attack", c => {
    c.OnExecute(() => {
        Attack();
        return 0;
    });
});

app.Command("run", c => {
    c.OnExecute(() => {
        Run();
        return 0;
    });
});

app.Command("walk", c => {
    c.OnExecute(() => {
        Walk();
        return 0;
    });
});

app.Command("eat", c => {
    c.OnExecute(() => {
        Eat();
        return 0;
    });
});

app.Command("exit", c => {
    c.OnExecute(() => {
        return 1;
    });
});

app.OnExecute(() => {
    if (currentState.CurrentEnemy == null)
        Console.WriteLine("Walk [walk] or eat [eat] something, but be aware, you may always get attacked");
    else
        Console.WriteLine("You can attack [attack] or try to run [run], but be aware that if you run and fail, you will loose the game");
    return 0;
});

Console.WriteLine("Welcome to the game");

do {
    Console.WriteLine("What will you do next?");
    var command = Console.ReadLine();

    var result = app.Execute(command);
    if (result == 1) {
        return;
    }

    if (EvaluateGameEnded()) {
        SetUpState();
    }
    Console.WriteLine();
} while (true);