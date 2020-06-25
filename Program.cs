using System;
using Microsoft.Extensions.Configuration;

namespace _2048
{
	class Program
    {
	    static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("application.json").Build();
            bool replays = config["replays"].ToLower()=="true";
            bool runApp = true;
            while(runApp){
                int esc = NewGame();
                if (replays){
                    if (esc==1){
                        runApp = false;
                        break;
                    }
                    Console.WriteLine("New game? (Y or N): ");
                    var yes = Console.ReadKey();
                    while (yes.Key != ConsoleKey.Y && yes.Key != ConsoleKey.N){
                        Console.WriteLine("New game? (Y or N): ");
                        yes = Console.ReadKey();
                    }
                    if (yes.Key != ConsoleKey.Y){
                        runApp = false;
                    }
                }else{
                    runApp = false;
                }
            }
            
        }
        static int NewGame(){
            var map = new Map(4,4);
            map.AddRandomValue(2);
            map.AddRandomValue(2);
            var draw = new ConsoleDraw();
            bool isRun = true;
            int stepCount = 0;
            int rez = 0;
            while (isRun){
                draw.Draw(map);
                Console.WriteLine("Step count: "+stepCount);
                bool stepFl = false;
                bool endGame = false;
                var key = Console.ReadKey();
                switch(key.Key){
                    case ConsoleKey.Escape:
                        rez = 1;
                        isRun=false;
                        break;
                    case ConsoleKey.LeftArrow:
                        stepFl = map.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        stepFl = map.MoveRight();
                        break;
                    case ConsoleKey.UpArrow:
                        stepFl = map.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        stepFl = map.MoveDown();
                        break;
                }
                if (stepFl){
                    stepCount++;
                    endGame = map.AddRandomValue(2);
                }else{
                    //end game test
                    if (map.IsEndGame()){
                        isRun = false;
                        rez = 2;
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("End game!");
            Console.WriteLine("Step count: "+stepCount);
            return rez;            
        }
    }
}
