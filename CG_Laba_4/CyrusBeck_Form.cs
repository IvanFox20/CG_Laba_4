using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Laba_4
{
    public partial class CyrusBeck_Form : Form
    {

        private const int GridWidth = 630;
        private const int GridHeight = 630;
        private const int CellSize = 30;
        private Graphics g;
        private Bitmap bitmap;
        List<PointF> polygonPoints = new List<PointF>();
        List<PointF> segmentPoints = new List<PointF>();
        List<PointF> potentialPoints = new List<PointF>();
        List<PointF> startSegmentPoints;
        List<float> window = new List<float>();
        private bool invisible = false;
        int polygonPointsCnt = 0;

        public CyrusBeck_Form()
        {
            InitializeComponent();
            bitmap = new Bitmap(GridWidth, GridHeight);
            g = Graphics.FromImage(bitmap);
            FileInput();
            windowFilling();
            CyrusBeck();
        }

        private void CyrusBeck()
        {
            float tMin = 0f;
            float tMax = 1f;
            PointF directrix = new PointF(segmentPoints[1].X - segmentPoints[0].X, segmentPoints[1].Y - segmentPoints[0].Y);
            potentialPoints.Add(new PointF(segmentPoints[0].X + directrix.X * tMin, segmentPoints[0].Y + directrix.Y * tMin));
            potentialPoints.Add(new PointF(segmentPoints[0].X + directrix.X * tMax, segmentPoints[0].Y + directrix.Y * tMax));
            for (int i = 0; i < polygonPointsCnt; i++)
            {
                int nextIndex = (i == polygonPointsCnt - 1) ? 0 : i + 1;
                PointF w = new PointF(segmentPoints[0].X - polygonPoints[i].X, segmentPoints[0].Y - polygonPoints[i].Y);
                PointF n = new PointF(polygonPoints[nextIndex].Y - polygonPoints[i].Y, polygonPoints[i].X - polygonPoints[nextIndex].X);
                float dScalar = ScalarMultiplication(directrix, n);
                float wScalar = ScalarMultiplication(w, n);
                if(dScalar != 0)
                {
                    float t = -wScalar / dScalar;
                    potentialPoints.Add(new PointF(segmentPoints[0].X + directrix.X * t, segmentPoints[0].Y + directrix.Y * t));
                    if (dScalar > 0)
                    {
                        tMin = Math.Max(t, tMin);
                    }
                    else if(dScalar < 0)
                    {
                        tMax = Math.Min(t, tMax);
                    }
                }
                else if(wScalar < 0)
                {
                    invisible = true;
                }
            }
            if(tMin <= tMax && !invisible)
            {
                float tmpX1 = segmentPoints[0].X + directrix.X * tMin;
                float tmpY1 = segmentPoints[0].Y + directrix.Y * tMin;
                float tmpX2 = segmentPoints[0].X + directrix.X * tMax;
                float tmpY2 = segmentPoints[0].Y + directrix.Y * tMax;
                segmentPoints[0] = new PointF(tmpX1, tmpY1);
                segmentPoints[1] = new PointF(tmpX2, tmpY2);
            }
            DrawCyrusBeck();
        }

        private float ScalarMultiplication(PointF vec1, PointF vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }
        private void DrawCyrusBeck()
        {
            ClearCanvas();
            DrawGrid();
            List<PointF> convertedPolygonPoints = PointConvertionToCoordinates(polygonPoints);
            List<PointF> convertedSegmentPoints = PointConvertionToCoordinates(segmentPoints);
            List<PointF> convertedStartSegmentPoints = PointConvertionToCoordinates(startSegmentPoints);
            List<PointF> convertedPotentialPoints = PointConvertionToCoordinates(potentialPoints);
            Pen startPen = new Pen(Color.Blue, 3);
            Pen visiblePen = new Pen(Color.Green, 3);
            Pen polygonPen = new Pen(Color.Red, 3);
            g.DrawPolygon(polygonPen, convertedPolygonPoints.ToArray());
            g.DrawLines(startPen, convertedStartSegmentPoints.ToArray());
            if (!invisible) g.DrawLines(visiblePen, convertedSegmentPoints.ToArray());
            DrawPoints(convertedPotentialPoints);
        }

        private void DrawPoints(List<PointF> points)
        {
            int r = 10;
            foreach (var point in points)
            {
                g.FillEllipse(Brushes.Black, point.X - r/2, point.Y - r/2, r, r);
            }
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
                using (StreamReader sr = new StreamReader("CyrusBeckInput.txt"))
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
                            polygonPointsCnt++;
                            continue;
                        }
                        segmentPoints.Add(new PointF(x, y));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            startSegmentPoints = new List<PointF>(segmentPoints);
        }
        private void CyrusBeck_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
