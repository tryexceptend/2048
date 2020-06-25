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

        public bool IsEndGame(){
            List<List<int>> tmpMapTable = new List<List<int>>(MapTable.Count);
            for (int r=0; r<MapTable.Count; r++){
                var tmp = new List<int>(MapTable[r]);
                tmpMapTable.Add(tmp);
            }
            bool end = MoveLeft(tmpMapTable);
            if (end){return false;}
            tmpMapTable = new List<List<int>>(MapTable.Count);
            for (int r=0; r<MapTable.Count; r++){
                var tmp = new List<int>(MapTable[r]);
                tmpMapTable.Add(tmp);
            }
            end = MoveRight(tmpMapTable);
            if (end){return false;}
            tmpMapTable =new List<List<int>>(MapTable.Count);
            for (int r=0; r<MapTable.Count; r++){
                var tmp = new List<int>(MapTable[r]);
                tmpMapTable.Add(tmp);
            }
            end = MoveUp(tmpMapTable);
            if (end){return false;}
            tmpMapTable =new List<List<int>>(MapTable.Count);
            for (int r=0; r<MapTable.Count; r++){
                var tmp = new List<int>(MapTable[r]);
                tmpMapTable.Add(tmp);
            }
            end = MoveDown(tmpMapTable);
            if (end){return false;}
            return true;
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
            return MoveLeft(MapTable);
        }
        public bool MoveLeft(List<List<int>> mapTable){
            int st = 0;
            for (int r=0; r<mapTable.Count;r++){
                st += MoveRow(mapTable, r,true);
            }
            return st!=0;
        }
        public bool MoveRight(){
            return MoveRight(MapTable);
        }
        public bool MoveRight(List<List<int>> mapTable){
            int st = 0;
            for (int r=0; r<mapTable.Count;r++){
                st += MoveRow(mapTable, r,false);
            }
            return st!=0;
        }
        public bool MoveUp(){
            return MoveUp(MapTable);
        }
        public bool MoveUp(List<List<int>> mapTable){
            int st = 0;
            for (int c=0; c<mapTable[0].Count;c++){
                st += MoveCol(mapTable, c,true);
            }
            return st!=0;
        }
        public bool MoveDown(){
            return MoveDown(MapTable);
        }
        public bool MoveDown(List<List<int>> mapTable){
            int st = 0;
            for (int c=0; c<mapTable[0].Count;c++){
                st += MoveCol(mapTable, c,false);
            }
            return st!=0;
        }

        private int MoveRow(List<List<int>> mapTable, int rowIndex, bool left){
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
                    if (mapTable[rowIndex][c]!=0){
                        if (mapTable[rowIndex][c-iter]==0){
                            mapTable[rowIndex][c-iter] =mapTable[rowIndex][c];
                            mapTable[rowIndex][c]=0;
                            stepCount++;
                            isEnd = false;
                        }else{
                            if (mapTable[rowIndex][c-iter] == mapTable[rowIndex][c]){
                                mapTable[rowIndex][c-iter] = mapTable[rowIndex][c]*2;
                                mapTable[rowIndex][c]=0;
                                stepCount++;
                                isEnd = false;
                            }
                        }
                    }
                }
            }
            return stepCount;
        }
        private int MoveCol(List<List<int>> mapTable, int colIndex, bool up){
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
                    if (mapTable[r][colIndex]!=0){
                        if (mapTable[r-iter][colIndex]==0){
                            mapTable[r-iter][colIndex] = mapTable[r][colIndex];
                            mapTable[r][colIndex]=0;
                            stepCount++;
                            isEnd = false;
                        }else{
                            if (mapTable[r-iter][colIndex] == mapTable[r][colIndex]){
                                mapTable[r-iter][colIndex] = mapTable[r][colIndex]*2;
                                mapTable[r][colIndex]=0;
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
