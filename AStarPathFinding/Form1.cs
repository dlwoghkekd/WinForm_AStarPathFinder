using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AStarPathFinding
{
    //타일 속성
    //기본, 출발점, 목적지, 벽, 열린리스트, 닫힌리스트, 길 
    public enum eTileState
    { None, Start, Goal, Wall, Open, Close, Path }



    public partial class Form1 : Form
    {
        //구조체=====================
        struct stCurrentAction//사용자의 세팅(타일배치)종류 확인용 구조체
        {
            public object sender;//호출한 버튼
            public eTileState actionType;//세팅 타입
            public stCurrentAction(object sender, eTileState type)
            {
                this.sender = sender;
                this.actionType = type;
            }
        }
        //======================구조체

        //상수=======================
        const int TABLE_SIZE = 16;//테이블 사이즈
        readonly Point defaultStartPoint = new Point(5, 6);//디폴트 시작점
        readonly Point defaultGoalPoint = new Point(9, 6);//디폴트 목적지
        readonly Point[] defaultWallPoint = new Point[3]//디폴트 벽 위치
            { new Point(7, 5), new Point(7, 6), new Point(7, 7) };
        readonly Color defaultButtonColor;//디폴트 버튼 색
        //========================상수

        //렌더링======================
        readonly int tableNodeSize;//노드사이즈
        readonly Pen pen = new Pen(Color.DimGray, 1);//선 그리기용
        readonly Font font = new Font("바탕", 10, FontStyle.Regular);//폰트
        //=======================렌더링

        //길찾기=======================
        eTileState[,] tableData;//테이블 데이터
        AstarPathFinder pathFinder;//길찾기 클래스
        Point startPoint;//시작점
        Point goalPoint;//목표점
        //=======================길찾기

        //세팅=========================
        stCurrentAction? curAction = null;//현재 사용자가 선택한 배치타입
        //=========================세팅


        protected override CreateParams CreateParams
        { get { var cp = base.CreateParams; cp.ExStyle |= 0x02000000; return cp; } }

        public Form1()
        {
            InitializeComponent();

            defaultButtonColor = Color.LightGray;

            tableNodeSize = pn_Table.Size.Width / TABLE_SIZE;

            startPoint = defaultStartPoint;
            goalPoint = defaultGoalPoint;

            tableData = new eTileState[TABLE_SIZE, TABLE_SIZE];
            SetDefault();

            pathFinder = new AstarPathFinder(new Point(TABLE_SIZE, TABLE_SIZE), tableData);
        }

        //테이블 초기화
        void SetDefault()
        {
            for(int i = 0; i < TABLE_SIZE; i++)
            {
                for(int j = 0; j < TABLE_SIZE; j++)
                {
                    tableData[i, j] = eTileState.None;
                }
            }

            tableData[defaultStartPoint.X, defaultStartPoint.Y] = eTileState.Start;
            tableData[defaultGoalPoint.X, defaultGoalPoint.Y] = eTileState.Goal;

            foreach(Point iter in defaultWallPoint)
                tableData[iter.X, iter.Y] = eTileState.Wall;

            startPoint = defaultStartPoint;
            goalPoint = defaultGoalPoint;
        }

        //노드 그리기
        void DrawNode(PaintEventArgs e, Color color, AstarNode node)
        {
            Rectangle rect = new Rectangle(node.axis.X * tableNodeSize, node.axis.Y * tableNodeSize, tableNodeSize, tableNodeSize);

            e.Graphics.FillRectangle(new SolidBrush(color), rect);
            e.Graphics.DrawRectangle(pen, rect);

            e.Graphics.DrawString(node.F.ToString(), font, Brushes.Black, rect.X + 1, rect.Y + 4);
            e.Graphics.DrawString(node.G.ToString(), font, Brushes.Black, rect.X + 1, rect.Y + tableNodeSize * .7f);
            e.Graphics.DrawString(node.H.ToString(), font, Brushes.Black, rect.X + tableNodeSize - 1, rect.Y + tableNodeSize * .7f, new StringFormat(StringFormatFlags.DirectionRightToLeft));

            if(node.parent != null)
            {

                Point center = new Point(rect.X + tableNodeSize / 2, rect.Y + tableNodeSize / 2);
                RectangleF centerRect = new RectangleF(new PointF(center.X - 2.5f, center.Y - 2.5f), new SizeF(5, 5));
                e.Graphics.DrawLine(pen, center, pathFinder.AddPoint(center, node.ParentDirection, -8));
                e.Graphics.FillRectangle(new SolidBrush(Color.DimGray), centerRect);
            }
        }

        /// <summary>
        /// 버튼 선택시 들어옴, 현재 선택한 배치타입 설정
        /// </summary>
        /// <param name="sender">null 일경우 초기화</param>
        /// <param name="type">None 일경우 취소</param>
        void SetCurrentAction(object sender, eTileState type)
        {
            if(type != eTileState.Open)//오픈은 스텝모드,,..
                pathFinder.Initialize();

            if(curAction != null)
            {
                (curAction.Value.sender as Button).BackColor = defaultButtonColor;

                if(type == eTileState.None || type == curAction.Value.actionType)
                {
                    curAction = null;
                    return;
                }
            }

            if(sender != null)
            {
                curAction = new stCurrentAction(sender, type);

                if(type != eTileState.None)
                    (sender as Button).BackColor = Color.LightSeaGreen;
            }
        }

        private void pn_Table_Paint(object sender, PaintEventArgs e)
        {
            for(int i = 0; i < TABLE_SIZE; i++)
            {
                for(int j = 0; j < TABLE_SIZE; j++)
                {
                    Color color = new Color();

                    switch(tableData[i, j])
                    {
                        case eTileState.None:
                            break;
                        case eTileState.Start:
                            color = Color.IndianRed;
                            break;
                        case eTileState.Goal:
                            color = Color.LightGreen;
                            break;
                        case eTileState.Wall:
                            color = Color.CornflowerBlue;
                            break;
                        case eTileState.Open:
                            color = Color.LemonChiffon;
                            break;
                        case eTileState.Close:
                            color = Color.BurlyWood;
                            break;
                        case eTileState.Path:
                            color = Color.Plum;
                            break;
                    }
                    DrawNode(e, color, pathFinder.GetAstarNode(new Point(i, j)));
                }
            }
        }

        private void btn_SetStartPoint_Click(object sender, EventArgs e)
        {
            SetCurrentAction(sender, eTileState.Start);
        }

        private void btn_SetGoalPoint_Click(object sender, EventArgs e)
        {
            SetCurrentAction(sender, eTileState.Goal);
        }

        private void btn_SetWallPoint_Click(object sender, EventArgs e)
        {
            SetCurrentAction(sender, eTileState.Wall);
        }

        private void btn_SetDefault_Click(object sender, EventArgs e)
        {
            SetCurrentAction(sender, eTileState.None);

            SetDefault();
            pn_Table.Refresh();
        }

        private void pn_Table_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X / tableNodeSize;
            int y = e.Y / tableNodeSize;

            if(tableData[x, y] == eTileState.Start || tableData[x, y] == eTileState.Goal)
            {
                return;
            }

            if(curAction != null)
            {
                switch(curAction.Value.actionType)
                {
                    case eTileState.Start:
                        if(tableData[x, y] == eTileState.Wall) break;

                        tableData[startPoint.X, startPoint.Y] = eTileState.None;

                        startPoint = new Point(x, y);
                        SetCurrentAction(null, eTileState.None);
                        tableData[startPoint.X, startPoint.Y] = eTileState.Start;

                        break;
                    case eTileState.Goal:
                        if(tableData[x, y] == eTileState.Wall) break;

                        tableData[goalPoint.X, goalPoint.Y] = eTileState.None;

                        goalPoint = new Point(x, y);
                        SetCurrentAction(null, eTileState.None);
                        tableData[goalPoint.X, goalPoint.Y] = eTileState.Goal;

                        break;
                    case eTileState.Wall:
                        if(tableData[x, y] == eTileState.Start || tableData[x, y] == eTileState.Goal)
                            break;

                        if(tableData[x, y] == eTileState.Wall)
                        {
                            tableData[x, y] = eTileState.None;
                            break;
                        }

                        tableData[x, y] = eTileState.Wall;
                        break;
                    default:
                        break;
                }
            }
            (sender as Panel).Refresh();
        }

        private void btn_PathFind_Click(object sender, EventArgs e)
        {
            SetCurrentAction(null, eTileState.None);
            pathFinder.PathFind(pn_Table, startPoint, goalPoint);
        }

        private void btn_Step_Click(object sender, EventArgs e)
        {
            SetCurrentAction(null, eTileState.Open);

            pathFinder.PathFind(pn_Table, startPoint, goalPoint, true);
        }
    }
}