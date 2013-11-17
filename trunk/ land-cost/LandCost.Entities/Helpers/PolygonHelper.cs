using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.WindowsForms;
using System.Drawing;
using GMap.NET.ObjectModel;

namespace LandCost.Entities.Helpers
{
    public static class PolygonHelper
    {
        public static Pen GeneralPen = new Pen(Color.FromArgb(255, Color.DarkBlue), 1);

        public static Brush InactiveBrush = new SolidBrush(Color.FromArgb(100, Color.Red));
        public static Brush ActiveBrush = new SolidBrush(Color.FromArgb(100, Color.Green));
        public static Brush CurrentBrush = new SolidBrush(Color.FromArgb(100, Color.Blue));
        public static Brush SelectedBrush = new SolidBrush(Color.FromArgb(50, Color.White));
        public static Brush MainBrush = new SolidBrush(Color.FromArgb(50, Color.Green));

        public static GMapPolygon GMapPolygonByEntity(LandPolygon pol)
        {
            GMapPolygon ret = new GMapPolygon(pol.Points, pol.Name);
            ret.IsHitTestVisible = true;
            return ret;
        }

        public static GMapPolygon FindGMapPolygon(LandPolygon pol, ObservableCollectionThreadSafe<GMapPolygon> gpols)
        {
            GMapPolygon ret = null;
            foreach (GMapPolygon p in gpols)
            {
                if (PolygonsEqual(p, pol))
                {
                    ret = p;
                    break;
                }
            }
            return ret;
        }

        public static LandPolygon FindPolygon(GMapPolygon gpol, List<LandPolygon> lpols)
        {
            LandPolygon ret = null;
            foreach (LandPolygon lp in lpols)
            {
                if (PolygonsEqual(gpol, lp))
                {
                    ret = lp;
                    break;
                }
            }
            return ret;
        }

        public static bool PolygonsEqual(GMapPolygon gpol, LandPolygon lpol)
        {
            return gpol.Name == lpol.Name;
        }

        public static bool Contains(GMapPolygon gpol, List<LandPolygon> lpols)
        {
            bool bRet = false;
            foreach (LandPolygon lpol in lpols)
            {
                if (PolygonsEqual(gpol, lpol))
                {
                    bRet = true;
                    break;
                }
            }
            return bRet;
        }
    }
}
