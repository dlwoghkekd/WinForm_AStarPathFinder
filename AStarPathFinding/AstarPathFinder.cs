using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPathFinding
{
    public class AstarNode//길찾기용 노드클래스
    {
        public Point axis;//노드의 좌표
        public int G { get; private set; } = 0;//시작점부터의 비용, 가로세로 = 10, 대각선 = 14
        public int H { get; private set; } = 0;//목적지 까지의 최단거리
        public int F { get; private set; } = 0;//총 비용
        public Point ParentDirection;
        public AstarNode parent { get; private set; } = null;//부모 노드, 길을 찾고 부모를 따라가면 길이생성됨

        public AstarNode(Point axis)
        {
            this.axis = axis;
            G = 0;
            H = 0;
            F = 0;
        }

        public void SetGCost(int gcost)
        {
            G = gcost;
            F = G + H;

        }

        public void SetHCost(int hcost)
        {
            H = hcost;
            F = G + H;
        }

        public void SetParent(AstarNode parentNode)
        {
            parent = parentNode;
        }

        public void Reset()
        {
            G = 0;
            H = 0;
            F = 0;
            parent = null;
        }
    }

    class AstarPathFinder
    {
        //자식 생성용 주변 인덱스
        //좌 하단부터 반시계방향
        readonly Point[] neerAxis = new Point[8]
        {
            new Point(-1, -1), new Point(0, -1),
            new Point(1, -1), new  Point(1, 0),
            new Point(1, 1), new   Point(0, 1),
            new Point(-1, 1), new  Point(-1, 0)
        };

        eTileState[,] tableStateData;//노드 상태 배열
        AstarNode[,] tableNodeData;//노드 데이터 배열 

        readonly Point tableSize;//테이블의 크기

        bool bStepMode;//스텝모드인가?

        //오픈리스트, 클로즈리스트
        //접근하기 쉽게 딕셔너리로 선언
        Dictionary<Point, AstarNode> dicOpenList = new Dictionary<Point, AstarNode>();
        Dictionary<Point, AstarNode> dicCloseList = new Dictionary<Point, AstarNode>();

        AstarNode focusNode;//현재 보고있는 노드

        public AstarPathFinder(Point tableSize, eTileState[,] tableData)
        {
            this.tableSize = tableSize;
            this.tableStateData = tableData;

            tableNodeData = new AstarNode[this.tableSize.X, this.tableSize.Y];
            for(int i = 0; i < this.tableSize.X; i++)
            {
                for(int j = 0; j < this.tableSize.Y; j++)
                {
                    tableNodeData[i, j] = new AstarNode(new Point(i, j));
                }
            }
        }

        //초기화
        public void Initialize(bool stepMode = false)
        {
            if(stepMode)
                bStepMode = true;
            else
                bStepMode = false;

            dicOpenList.Clear();
            dicCloseList.Clear();

            for(int i = 0; i < tableSize.X; i++)
            {
                for(int j = 0; j < tableSize.Y; j++)
                {
                    tableNodeData[i, j].Reset();
                    switch(tableStateData[i, j])
                    {
                        case eTileState.Start:
                        case eTileState.Goal:
                        case eTileState.Wall:
                            break;
                        default:
                            tableStateData[i, j] = eTileState.None;
                            break;
                    }
                }

            }
        }

        public void PathFind(Panel pn, Point startPoint, Point goalPoint, bool stepMode = false)
        {
            if(!bStepMode && stepMode||!stepMode)
            {
                Initialize(stepMode);
                focusNode = tableNodeData[startPoint.X, startPoint.Y];
                focusNode.SetHCost((Math.Abs(focusNode.axis.X - goalPoint.X) + Math.Abs(focusNode.axis.Y - goalPoint.Y)) * 10);
                dicCloseList.Add(focusNode.axis, focusNode);
            }

            bool bFindPath = false;//길을 찾았으면 true, or false

            while(true)
            {
                if(focusNode.axis.Equals(goalPoint))
                {
                    bFindPath = true;
                    break;
                }

                //오픈리스트 생성
                CreateOpenList(goalPoint);

                //오픈리스트가 비었다면 길이 없다 판단
                if(dicOpenList.Count <= 0)
                {
                    bFindPath = false;
                    bStepMode = false;
                    break;
                }

                //오픈리스트의 첫번째 겂을 임시로 저장(f값 비교용)
                AstarNode tempPathNode = dicOpenList.Values.ToArray()[0];

                //f비용이 가장 작은 노드로 바꿔준다
                foreach(var iter in dicOpenList.Values)
                {
                    if(tempPathNode.F > iter.F)
                        tempPathNode = iter;
                }

                //이제 포커싱하고있는 노드는 f값이 가장 작은노드
                focusNode = tempPathNode;

                //포커싱하는 노드는 오픈리스트에서 뺀 후 클로즈리스트로 보낸다
                dicOpenList.Remove(focusNode.axis);
                dicCloseList.Add(focusNode.axis, focusNode);
                tableStateData[focusNode.axis.X, focusNode.axis.Y] = eTileState.Close;

                if(stepMode) break;
            }

            //길을 찾았는가
            if(bFindPath)
            {
                //만일 찾았다면 포커싱하고있던 노드는 도착점이다.
                while(focusNode != null)
                {
                    tableStateData[focusNode.axis.X, focusNode.axis.Y] = eTileState.Path;
                    focusNode = focusNode.parent;
                    bStepMode = false;
                }
            }

            tableStateData[startPoint.X, startPoint.Y] = eTileState.Start;
            tableStateData[goalPoint.X, goalPoint.Y] = eTileState.Goal;

            pn.Refresh();
        }

        public Point AddPoint(Point left, Point right, int mulRight = 1)
        {
            return new Point(left.X + right.X * mulRight, left.Y + right.Y * mulRight);
        }

        /// <summary>
        /// 오픈리스트 생성
        /// </summary>
        void CreateOpenList(Point goalPoint)
        {
            for(int i = 0; i < 8; i++)
            {
                //예비 자식이 될 노드들의 좌표 계산
                Point tempChildAxis = AddPoint(focusNode.axis, neerAxis[i]);

                //예비 자식노드의 좌표가 테이블좌표 밖일시 후보에서 거른다
                if(tempChildAxis.X < 0 || tempChildAxis.X >= tableSize.X ||
                    tempChildAxis.Y < 0 || tempChildAxis.Y >= tableSize.Y)
                    continue;

                //g의값은 가로세로 10, 대각선 14
                int gCost = i % 2 == 0 ? 14 : 10;

                //만일 해당 노드가 오픈리스트에 있다면 g비용이 더 저렴한 노드를 다음 목적지로 선정
                if(dicOpenList.ContainsKey(tempChildAxis))
                {
                    AstarNode tempOpenNode = dicOpenList[tempChildAxis];

                    if(tempOpenNode.G > gCost + focusNode.G)
                    {
                        tempOpenNode.SetGCost(gCost + focusNode.G);
                        tempOpenNode.SetParent(focusNode);
                        tempOpenNode.ParentDirection = neerAxis[i];
                    }
                    continue;
                }

                //만일 예비 자식의 위치가 벽일시 무시한다
                if(tableStateData[tempChildAxis.X, tempChildAxis.Y] == eTileState.Wall)
                    continue;

                //만일 클로즈 리스느에 있어도 무시한다.
                if(dicCloseList.ContainsKey(tempChildAxis))
                    continue;

                //대각선일시 자신의 주변을 체크 후 벽이있다면 무시한다(벽끼고 대각선이동 불가)
                if(gCost == 14)
                {
                    Point tempNextIndex = AddPoint(focusNode.axis, neerAxis[i + 1]);
                    Point tempPrevIndex = AddPoint(focusNode.axis, neerAxis[i - 1 < 0 ? 7 : i - 1]);

                    if(tableStateData[tempNextIndex.X, tempNextIndex.Y] == eTileState.Wall ||
                        tableStateData[tempPrevIndex.X, tempPrevIndex.Y] == eTileState.Wall)
                        continue;
                }

                //심사에 통과한 노드들은 오픈리스트에 넣어준다.
                AstarNode openNode = tableNodeData[tempChildAxis.X, tempChildAxis.Y];
                openNode.SetParent(focusNode);
                openNode.SetGCost(gCost + openNode.parent.G);
                openNode.SetHCost((Math.Abs(tempChildAxis.X - goalPoint.X) + Math.Abs(tempChildAxis.Y - goalPoint.Y)) * 10);
                openNode.ParentDirection = neerAxis[i];

                dicOpenList.Add(tempChildAxis, openNode);
                tableStateData[tempChildAxis.X, tempChildAxis.Y] = eTileState.Open;
            }
        }

        public AstarNode GetAstarNode(Point point)
        {
            return tableNodeData[point.X, point.Y];
        }
    }
}
