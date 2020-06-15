using System;
using System.Linq;
using System.Collections.Generic;

namespace _2048
{
    
    public interface IDrawing{
        void Draw(Map map);
    }
    public class ConsoleDraw : IDrawing
    {
        private Dictionary<int,ConsoleColor> colors = new Dictionary<int, ConsoleColor>(){
            {2, ConsoleColor.White},
            {4, ConsoleColor.Yellow},
            {8, ConsoleColor.Green},
            {16, ConsoleColor.Cyan},
            {32, ConsoleColor.Red},
            {64, ConsoleColor.Magenta},
            {128, ConsoleColor.Blue},
            {256, ConsoleColor.DarkYellow},
            {512, ConsoleColor.DarkGreen},
            {1024, ConsoleColor.DarkCyan},
            {2048, ConsoleColor.DarkRed},
            {4096, ConsoleColor.DarkMagenta}
        };
        public void Draw(Map map)
        {
            Console.Clear();
            int rowCount = map.MapTable.Count;
            int colCount = map.MapTable[0].Count;
            string linep = "═══════";
            string lines = "       ";
            //draw
            Console.Write("╔");for(int c=0;c<colCount;c++){Console.Write(linep);if (c<colCount-1){Console.Write("╦");}}Console.Write("╗\n");
            for (int r = 0; r<rowCount; r++){
                Console.Write("║");for(int c=0;c<colCount;c++){Console.Write(lines);if (c<colCount-1){Console.Write("║");}}Console.Write("║\n");
                Console.Write("║");
                for (int c=0;c<colCount;c++){
                    if (map.MapTable[r][c]!=0){
                        if ((int)(map.MapTable[r][c] / 10) == 0){
                            Console.Write("   ");DrawColortext(map.MapTable[r][c].ToString(),colors[map.MapTable[r][c]]);Console.Write("   ");
                        }else{
                            if ((int)(map.MapTable[r][c] / 100) == 0){
                                Console.Write("   ");DrawColortext(map.MapTable[r][c].ToString(),colors[map.MapTable[r][c]]);Console.Write("  ");
                            }else{
                                if ((int)(map.MapTable[r][c] / 1000) == 0){
                                    Console.Write("  ");DrawColortext(map.MapTable[r][c].ToString(),colors[map.MapTable[r][c]]);Console.Write("  ");
                                }else{
                                    if ((int)(map.MapTable[r][c] / 10000) == 0){
                                        Console.Write("  ");DrawColortext(map.MapTable[r][c].ToString(),colors[map.MapTable[r][c]]);Console.Write(" ");
                                    }else{
                                        if ((int)(map.MapTable[r][c] / 100000) == 0){
                                            Console.Write(" ");DrawColortext(map.MapTable[r][c].ToString(),colors[map.MapTable[r][c]]);Console.Write(" ");
                                        }else{
                                            Console.Write(" ");DrawColortext(map.MapTable[r][c].ToString(),colors[map.MapTable[r][c]]);
                                        }
                                    }
                                }
                            }
                        }
                    }else{
                        Console.Write(lines);
                    }
                    if (c<colCount-1){
                        Console.Write('║');
                    }
                }
                Console.Write("║\n");
                Console.Write("║");for(int c=0;c<colCount;c++){Console.Write(lines);if (c<colCount-1){Console.Write("║");}}Console.Write("║\n");
                if (r<rowCount-1){
                    Console.Write("╠");for(int c=0;c<colCount;c++){Console.Write(linep);if (c<colCount-1){Console.Write("╬");}}Console.Write("╣\n");
                }
            }
            Console.Write("╚");for(int c=0;c<colCount;c++){Console.Write(linep);if (c<colCount-1){Console.Write("╩");}}Console.Write("╝\n");
        }
        public void DrawColortext(string mess, ConsoleColor color){
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(mess);
            Console.ForegroundColor = old;
        }
    }

}
