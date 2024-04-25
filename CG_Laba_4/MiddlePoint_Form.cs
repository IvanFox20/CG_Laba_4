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
    public partial class MiddlePoint_Form : Form
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
        private bool invisible = false;
        public MiddlePoint_Form()
        {
            InitializeComponent();
            bitmap = new Bitmap(GridWidth, GridHeight);
            g = Graphics.FromImage(bitmap);
            FileInput();
            windowFilling();
            MiddlePoint();
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
                using (StreamReader sr = new StreamReader("MiddlePointInput.txt"))
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

        private void MiddlePoint()
        {
            int i = 0;
            double eps = 0.0001;
            int sum1;
            int sum2;
            int multiplication;
            PointF replacePoint;
        Increment:
            {
                segment1Code = End(segmentPoints[0].X, segmentPoints[0].Y, window);
                segment2Code = End(segmentPoints[1].X, segmentPoints[1].Y, window);
                sum1 = Sum(segment1Code);
                sum2 = Sum(segment2Code);
                if (sum1 == 0 && sum2 == 0) goto Draw;
                multiplication = LogicalMultiplication(segment1Code, segment2Code);
                if (multiplication != 0)
                {
                    invisible = true;
                    goto Finish;
                }
                replacePoint = segmentPoints[0];
                if (i > 2) goto Multiplication;
            }
        MiddlePointCalculate:
            {
                if (sum2 == 0) goto Replacement;
                if (Math.Sqrt(Math.Pow((segmentPoints[0].X - segmentPoints[1].X), 2) + Math.Pow((segmentPoints[0].Y - segmentPoints[1].Y), 2)) < eps) goto Replacement;
                float tmpX = (segmentPoints[0].X + segmentPoints[1].X) / 2;
                float tmpY = (segmentPoints[0].Y + segmentPoints[1].Y) / 2;
                PointF middlePoint = new PointF(tmpX, tmpY);
                PointF tmpPoint = segmentPoints[0];
                segmentPoints[0] = middlePoint;

                segment1Code = End(segmentPoints[0].X, segmentPoints[0].Y, window);
                sum1 = Sum(segment1Code);
                multiplication = LogicalMultiplication(segment1Code, segment2Code);
                if (multiplication == 0) goto MiddlePointCalculate;
                segmentPoints[0] = tmpPoint;
                segmentPoints[1] = middlePoint;
                goto MiddlePointCalculate;
            }
        Replacement:
            {
                segmentPoints[0] = segmentPoints[1];
                segmentPoints[1] = replacePoint;
                i++;
                goto Increment;
            }
        Multiplication:
            {
                multiplication = LogicalMultiplication(segment1Code, segment2Code);
                if (multiplication != 0)
                {
                    invisible = true;
                    goto Finish;
                }
            }
        Draw:
            {
                DrawMiddlePoint();
            }
        Finish:
            {
                DrawMiddlePoint();
            }
        }

        private List<int> End(float x, float y, List<float> window)
        {
            List<int> segmentCodeResult = new List<int>() { 0, 0, 0, 0 };
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
            for (int i = 0; i < 4; i++)
            {
                multiplication += (int)((segment1Code[i] + segment2Code[i]) >> 1);
            }
            return multiplication;
        }

        private void DrawMiddlePoint()
        {
            ClearCanvas();
            DrawGrid();
            List<PointF> convertedPolygonPoints = PointConvertionToCoordinates(polygonPoints);
            List<PointF> convertedSegmentPoints = PointConvertionToCoordinates(segmentPoints);
            List<PointF> convertedStartSegmentPoints = PointConvertionToCoordinates(startSegmentPoints);
            Pen startPen = new Pen(Color.Blue, 3);
            Pen visiblePen = new Pen(Color.Green, 3);
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

        private void MiddlePoint_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
