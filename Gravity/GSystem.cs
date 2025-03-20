using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Gravity
{
    public class GSystem
    {
        public double g = 39.5;
        public double dt = 0.008;
        public double softeningFactor = 0.15;

        public List<GOBject> gOBjects = new List<GOBject>();

        /// <summary>
        /// Метод Расчета ускорения
        /// </summary>
        private void CalculateAccelerationVectors()
        {
            for (int i = 0; i < gOBjects.Count; i++)
            {
                double AX = 0;
                double AY = 0;

                for (int j = 0; j < gOBjects.Count; j++)
                {
                    if (i != j)
                    {
                        double dx = gOBjects[j].X - gOBjects[i].X;
                        double dy = gOBjects[j].Y - gOBjects[i].Y;

                        double distSq = dx * dx + dy * dy;

                        double f = (g * gOBjects[j].Mass) / (distSq * Math.Sqrt(distSq + softeningFactor));

                        AX += dx * f;
                        AY += dy * f;
                    }
                }
                gOBjects[i].AX = AX;
                gOBjects[i].AY = AY;
            }
        }

        /// <summary>
        /// Метод расчета скорости
        /// </summary>
        private void CalculateVelocityVectors()
        {
            for (int i = 0; i < gOBjects.Count; ++i)
            {
                gOBjects[i].VX += gOBjects[i].AX * dt;
                gOBjects[i].VY += gOBjects[i].AY * dt;
            }
        }

        /// <summary>
        /// Метод расчета позиции
        /// </summary>
        private void CalculatePositionVectors()
        {
            for (int i = 0; i < gOBjects.Count; ++i)
            {
                gOBjects[i].X += gOBjects[i].VX * dt;
                gOBjects[i].Y += gOBjects[i].VY * dt;
            }
        }

        /// <summary>
        /// Метод проводит операции расчета по порядку согласно ТЗ
        /// </summary>
        public void Compute()
        {
            CalculatePositionVectors();
            CalculateAccelerationVectors();
            CalculateVelocityVectors();
        }

        /// <summary>
        /// Добавляет объекты по параметрам из конструктора
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mass"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vx"></param>
        /// <param name="vy"></param>
        public void AddGobject(string name, double mass, Color color, double x, double y, double vx, double vy)
        {
            GOBject o = new GOBject(name, mass, color, x, y, vx, vy);
            gOBjects.Add(o);
        }

        /// <summary>
        /// Добфвляет объекты по параметрам из конструктора
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mass"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vx"></param>
        /// <param name="vy"></param>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        public void AddGobject(string name, double mass, Color color, double x, double y, double vx, double vy, double ax, double ay)
        {
            GOBject o = new GOBject(name, mass, color, x, y, vx, y, ax, ay);
            gOBjects.Add(o);
        }

        /// <summary>
        /// Удаляет все объекты
        /// </summary>
        public void DeleteAllObjects()
        {
            gOBjects.Clear();
        }

        /// <summary>
        /// Метод загрузки готовых объектов из файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GSystem LoadXML(string path)
        {
            GSystem gSystem = new GSystem();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                XmlNode xml_GSystem = xmlDoc.GetElementsByTagName("GSystem")[0];

                XmlAttribute xml_G = xml_GSystem.Attributes["g"];
                double g = Convert.ToDouble(xml_G.Value);
                gSystem.g = g;

                XmlAttribute xml_DT = xml_GSystem.Attributes["dt"];
                double dt = Convert.ToDouble(xml_DT.Value);
                gSystem.dt = dt;

                XmlAttribute xml_SofteningFactor = xml_GSystem.Attributes["softeningfactor"];
                double sofeningFactor = Convert.ToDouble(xml_SofteningFactor.Value);
                gSystem.softeningFactor = sofeningFactor;

                XmlNodeList xml_GOBject = xml_GSystem.ChildNodes;

                foreach(XmlNode node in xml_GOBject)
                {
                    XmlAttribute xml_Name = node.Attributes["Name"];
                    string name = xml_Name.Value;

                    XmlAttribute xml_Mass = node.Attributes["Mass"];
                    double mass = Convert.ToDouble(xml_Mass.Value);

                    XmlAttribute xml_Color_R = node.Attributes["Color_R"];
                    int R = Convert.ToInt32(xml_Color_R.Value);

                    XmlAttribute xml_Color_G = node.Attributes["Color_G"];
                    int G = Convert.ToInt32(xml_Color_G.Value);

                    XmlAttribute xml_Color_B = node.Attributes["Color_B"];
                    int B = Convert.ToInt32(xml_Color_B.Value);

                    Color color = Color.FromArgb(R, G, B);

                    XmlAttribute xml_X = node.Attributes["X"];
                    double x = Convert.ToDouble(xml_X.Value);

                    XmlAttribute xml_Y = node.Attributes["Y"];
                    double y = Convert.ToDouble(xml_Y.Value);

                    XmlAttribute xml_VX = node.Attributes["VX"];
                    double vx = Convert.ToDouble(xml_VX.Value);

                    XmlAttribute xml_VY = node.Attributes["VY"];
                    double vy = Convert.ToDouble(xml_VY.Value);

                    XmlAttribute xml_AX = node.Attributes["AX"];
                    double ax = Convert.ToDouble(xml_AX.Value);

                    XmlAttribute xml_AY = node.Attributes["AY"];
                    double ay = Convert.ToDouble(xml_AY.Value);

                    XmlAttribute xml_K = node.Attributes["K"];
                    float k = float.Parse(xml_K.Value);

                    gSystem.AddGobject(name, mass, color, x, y, vx, vy, ax, ay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return gSystem;
        }
    }
}
