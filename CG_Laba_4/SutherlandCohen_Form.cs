using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Laba_4
{
    public partial class SutherlandCohen_Form : Form
    {
        private const int GridWidth = 630;
        private const int GridHeight = 630;
        private const int CellSize = 30;
        private Graphics g;
        private Bitmap bitmap;
        List<PointF> polygonPoints = new List<PointF>();
        List<PointF> segmentPoints = new List<PointF>();
        List<PointF> startSegmentPoints;
        List<int> segment1Code = new List<int>();
        List<int> segment2Code = new List<int>();
        List<float> window = new List<float>();
        List<PointF> drawPoints = new List<PointF>();
        private bool invisible = false;
        

        public SutherlandCohen_Form()
        {
            InitializeComponent();
            bitmap = new Bitmap(GridWidth, GridHeight);
            g = Graphics.FromImage(bitmap);
            FileInput();
            windowFilling();
            SutherlandCohen();
        }

        private void windowFilling()
        {
            float xL = int.MaxValue, xR = int.MinValue, yB = int.MaxValue, yT = int.MinValue;
            foreach (PointF point in polygonPoints)
            {
                if (xL > point.X) xL = point.X;
                if (xR < point.X) xR = point.X;
                if (yB > point.Y) yB = point.Y;
                if (yT < point.Y) yT = point.Y;
            }
            window.Add(xL);
            window.Add(xR);
            window.Add(yB);
            window.Add(yT);
        }
        private void FileInput()
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("SutherlandCohenInput.txt"))
                {
                    string line;
                    bool segmentFlag = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == "segment")
                        {
                            segmentFlag = true;
                            continue;
                        }
                        string[] coordinates = line.Split(' ');
                        float x = float.Parse(coordinates[0]);
                        float y = float.Parse(coordinates[1]);
                        if (!segmentFlag)
                        {
                            polygonPoints.Add(new PointF(x, y));
                            continue;
                        }
                        segmentPoints.Add(new PointF(x, y));
                    }
                }
                startSegmentPoints = new List<PointF>(segmentPoints);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void SutherlandCohen()
        {
            int orientation = 1; // индикатор координатной ориентации отрезка -1 - вертикальный, 0 - горизонтальный 
            float incline = 0;
            if (segmentPoints[1].X - segmentPoints[0].X == 0) orientation = -1;
            else
            {
                incline = (segmentPoints[1].Y - segmentPoints[0].Y) / (segmentPoints[1].X - segmentPoints[0].X);
                if (incline == 0) orientation = 0;
            }
            for(int i = 0; i < 4; i++)
            {
                int visibility = Cohen(segmentPoints, window);
                if(visibility == 2)
                {
                    DrawSutherlandCohen();
                }
                if(visibility == 0)
                {
                    invisible = true;
                    break;
                }
                if (segment1Code[3 - i] == segment2Code[3 - i]) continue;
                if (segment1Code[3-i] == 0)
                {
                    float tmpX = segmentPoints[0].X;
                    float tmpY = segmentPoints[0].Y;
                    segmentPoints[0] = segmentPoints[1];
                    segmentPoints[1] = new PointF(tmpX, tmpY);
                }
                if (orientation != -1 && i <= 1)
                {
                    float tmpY = incline * (window[i] - segmentPoints[0].X) + segmentPoints[0].Y;
                    float tmpX = window[i];
                    segmentPoints[0] = new PointF(tmpX, tmpY);
                }
                else
                {
                    if (orientation != 0)
                    {
                        float tmpX = segmentPoints[0].X;
                        float tmpY = window[i];
                        if (orientation != -1)
                        {
                            tmpX = ((1 / incline) * (window[i] - segmentPoints[0].Y)) + segmentPoints[0].X;
                        }
                        segmentPoints[0] = new PointF(tmpX, tmpY);
                    }
                }
            }
            DrawSutherlandCohen();
        }
        private int Cohen(List<PointF> segmentPoints, List<float> window)
        {
            segment1Code = End(segmentPoints[0].X, segmentPoints[0].Y, window);
            segment2Code = End(segmentPoints[1].X, segmentPoints[1].Y, window);
            int sum1 = Sum(segment1Code);
            int sum2 = Sum(segment2Code);
            int visibility = 1; // 0 - невидим,1 - частично видим, 2 - полностью видим
            if (sum1 == 0 && sum2 == 0) visibility = 2;
            else if (LogicalMultiplication(segment1Code, segment2Code) != 0) visibility = 0;
            return visibility;
        }

        private List<int> End(float x, float y, List<float> window)
        {
            List<int> segmentCodeResult = new List<int>() {0, 0, 0, 0};
            if (x < window[0]) segmentCodeResult[3] = 1;
            else segmentCodeResult[3] = 0;
            if (x > window[1]) segmentCodeResult[2] = 1;
            else segmentCodeResult[2] = 0;
            if (y < window[2]) segmentCodeResult[1] = 1;
            else segmentCodeResult[1] = 0;
            if (y > window[3]) segmentCodeResult[0] = 1;
            else segmentCodeResult[0] = 0;
            return segmentCodeResult;
        }
        
        private int Sum(List<int> segmentCode)
        {
            int sum = 0;
            foreach (int code in segmentCode)
            {
                sum += code;
            }
            return sum;
        }

        private int LogicalMultiplication(List<int> segment1Code, List<int> segment2Code)
        {
            int multiplication = 0;
            for (int i = 0;i < 4;i++)
            {
                multiplication += (int)((segment1Code[i] + segment2Code[i]) >> 1);
            }
            return multiplication;
        }
        private void DrawSutherlandCohen()
        {
            ClearCanvas();
            DrawGrid();
            List<PointF> convertedPolygonPoints = PointConvertionToCoordinates(polygonPoints);
            List<PointF> convertedSegmentPoints = PointConvertionToCoordinates(segmentPoints);
            List<PointF> convertedStartSegmentPoints = PointConvertionToCoordinates(startSegmentPoints);
            Pen startPen = new Pen(Color.Blue,3);
            Pen visiblePen = new Pen(Color.Green,3);
            Pen polygonPen = new Pen(Color.Red, 3);
            g.DrawPolygon(polygonPen, convertedPolygonPoints.ToArray());
            g.DrawLines(startPen, convertedStartSegmentPoints.ToArray());
            if (!invisible) g.DrawLines(visiblePen, convertedSegmentPoints.ToArray());
        }

        List<PointF> PointConvertionToCoordinates(List<PointF> points)
        {
            List<PointF> newPoints = new List<PointF>();
            foreach (PointF point in points)
            {
                float newX = GridWidth / 2 + point.X * CellSize - CellSize / 2;
                float newY = GridHeight / 2 - point.Y * CellSize - CellSize / 2;
                newPoints.Add(new PointF(newX, newY));
            }
            return newPoints;
        }

        private void DrawGrid()
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int xPos = 0; xPos <= GridWidth; xPos += CellSize)
                {
                    g.DrawLine(Pens.Black, xPos, 0, xPos, GridHeight);
                    int value = (xPos / CellSize) - 10;
                    string text = value.ToString();
                    SizeF textSize = g.MeasureString(text, this.Font);
                    float textX = xPos + (CellSize - textSize.Width) / 2;
                    float textY = GridHeight - 15;
                    g.DrawString(text, this.Font, Brushes.Black, textX, textY);
                }

                for (int yPos = 0; yPos <= GridHeight; yPos += CellSize)
                {
                    g.DrawLine(Pens.Black, 0, yPos, GridWidth, yPos);
                    int value = 10 - (yPos / CellSize);
                    g.DrawString(value.ToString(), this.Font, Brushes.Black, 0, yPos + 2);
                }
            }
            draw_pictureBox.Image = bitmap;
        }

        private void ClearCanvas()
        {
            draw_pictureBox.Refresh();
            g.Clear(Color.White);
        }
    }
}
