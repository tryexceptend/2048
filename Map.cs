using System;
using System.Linq;
using System.Collections.Generic;

namespace _2048
{
	public class Map
    {
        public List<List<int>> MapTable {get; private set;}
	    public Map(int w, int h)
        {
		    MapTable = new List<List<int>>(h);
            for (int r=0; r<h; r++){
                var tmp = new List<int>(w);
                for (int c=0; c<w; c++){
                    tmp.Add(0);
                }
                MapTable.Add(tmp);
            }
        }

        public bool AddRandomValue(int val){
            bool added = false;
            List<Tuple<int,int>> free = new List<Tuple<int,int>>();
            for (int r=0; r<MapTable.Count; r++){
                for (int c=0; c<MapTable[r].Count; c++){
                    if (MapTable[r][c]==0){
                        free.Add(new Tuple<int, int>(r,c));
                        added = true;
                    }
                }
            }
            if (free.Count>0){
                Random rnd = new Random(DateTime.Now.Millisecond);
                var add = free[rnd.Next(0,free.Count)];
                MapTable[add.Item1][add.Item2] = val;
                added = true;
            }
            return added;
        }
    
        public bool MoveLeft(){
            int st = 0;
            for (int r=0; r<MapTable.Count;r++){
                st += MoveRow(r,true);
            }
            return st!=0;
        }
        public bool MoveRight(){
            int st = 0;
            for (int r=0; r<MapTable.Count;r++){
                st += MoveRow(r,false);
            }
            return st!=0;
        }
        public bool MoveUp(){
            int st = 0;
            for (int c=0; c<MapTable[0].Count;c++){
                st += MoveCol(c,true);
            }
            return st!=0;
        }
        public bool MoveDown(){
            int st = 0;
            for (int c=0; c<MapTable[0].Count;c++){
                st += MoveCol(c,false);
            }
            return st!=0;
        }

        private int MoveRow(int rowIndex, bool left){
            int start = 1;
            int end = 3;
            int iter = 1;
            if (!left){
                start = 2;
                end = 0;
                iter = -1;
            }
            bool isEnd = false;
            int stepCount = 0;
            while(!isEnd){
                isEnd = true;
                for (int c = start; left ? c <= end : c >= end; c=c+iter){
                    if (MapTable[rowIndex][c]!=0){
                        if (MapTable[rowIndex][c-iter]==0){
                            MapTable[rowIndex][c-iter] = MapTable[rowIndex][c];
                            MapTable[rowIndex][c]=0;
                            stepCount++;
                            isEnd = false;
                        }else{
                            if (MapTable[rowIndex][c-iter] == MapTable[rowIndex][c]){
                                MapTable[rowIndex][c-iter] = MapTable[rowIndex][c]*2;
                                MapTable[rowIndex][c]=0;
                                stepCount++;
                                isEnd = false;
                            }
                        }
                    }
                }
            }
            return stepCount;
        }
        private int MoveCol(int colIndex, bool up){
            int start = 1;
            int end = 3;
            int iter = 1;
            if (!up){
                start = 2;
                end = 0;
                iter = -1;
            }
            bool isEnd = false;
            int stepCount = 0;
            while(!isEnd){
                isEnd = true;
                for (int r = start; up ? r <= end : r >= end; r=r+iter){
                    if (MapTable[r][colIndex]!=0){
                        if (MapTable[r-iter][colIndex]==0){
                            MapTable[r-iter][colIndex] = MapTable[r][colIndex];
                            MapTable[r][colIndex]=0;
                            stepCount++;
                            isEnd = false;
                        }else{
                            if (MapTable[r-iter][colIndex] == MapTable[r][colIndex]){
                                MapTable[r-iter][colIndex] = MapTable[r][colIndex]*2;
                                MapTable[r][colIndex]=0;
                                stepCount++;
                                isEnd = false;
                            }
                        }
                    }
                }
            }
            return stepCount;
        }

        
    }

    enum MoveType{
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 4
    }
}
