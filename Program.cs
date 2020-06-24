using System;

namespace _2048
{
	class Program
    {
	    static void Main(string[] args)
        {
		    var map = new Map(4,4);
            map.AddRandomValue(2);
            map.AddRandomValue(2);
            var draw = new ConsoleDraw();
            bool isRun = true;
            int stepCount = 0;
            while (isRun){
                draw.Draw(map);
                Console.WriteLine("Step count: "+stepCount);
                bool stepFl = false;
                bool endGame = false;
                var key = Console.ReadKey();
                switch(key.Key){
                    case ConsoleKey.Escape:
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
                    }
                }
            }
            Console.Clear();
            Console.WriteLine("End game!");
            Console.WriteLine("Step count: "+stepCount);
        }
    }
}
