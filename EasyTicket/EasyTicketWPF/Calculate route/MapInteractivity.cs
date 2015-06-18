using System;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;

namespace EasyTicketWPFClient.RouteCalc
{
    public class MapInteractivity
    {
        #region RouteResult

        public static readonly DependencyProperty RouteResultProperty = DependencyProperty.RegisterAttached("RouteResult", typeof(BingMapsService.RouteResult), typeof(MapInteractivity), new UIPropertyMetadata(null, OnRouteResultChanged));
        public static BingMapsService.RouteResult GetRouteResult(DependencyObject target)
        {
            return (BingMapsService.RouteResult)target.GetValue(RouteResultProperty);
        }
        public static void SetRouteResult(DependencyObject target, BingMapsService.RouteResult value)
        {
            target.SetValue(RouteResultProperty, value);
        }

        private static void OnRouteResultChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnRouteResultChanged((Map)o, (BingMapsService.RouteResult)e.OldValue, (BingMapsService.RouteResult)e.NewValue);
        }

        private static void OnRouteResultChanged(Map map, BingMapsService.RouteResult oldValue, BingMapsService.RouteResult newValue)
        {
            MapPolyline routeLine = new MapPolyline();
            routeLine.Locations = new LocationCollection();
            routeLine.Opacity = 0.65;
            routeLine.Stroke = new SolidColorBrush(Colors.Blue);
            routeLine.StrokeThickness = 5.0;

            foreach (BingMapsService.Location loc in newValue.RoutePath.Points)
            {
                routeLine.Locations.Add(new Location(loc.Latitude, loc.Longitude));
            }

            var routeLineLayer = GetRouteLineLayer(map);
            if (routeLineLayer == null)
            {
                routeLineLayer = new MapLayer();
                SetRouteLineLayer(map, routeLineLayer);
            }

            routeLineLayer.Children.Clear();
            routeLineLayer.Children.Add(routeLine);

            //Set the map view
            LocationRect rect = new LocationRect(routeLine.Locations[0], routeLine.Locations[routeLine.Locations.Count - 1]);
            map.SetView(rect);
        }

        #endregion //RouteResult

        #region RouteLineLayer

        public static readonly DependencyProperty RouteLineLayerProperty = DependencyProperty.RegisterAttached("RouteLineLayer", typeof(MapLayer), typeof(MapInteractivity), new UIPropertyMetadata(null, OnRouteLineLayerChanged));
        public static MapLayer GetRouteLineLayer(DependencyObject target)
        {
            return (MapLayer)target.GetValue(RouteLineLayerProperty);
        }
        public static void SetRouteLineLayer(DependencyObject target, MapLayer value)
        {
            target.SetValue(RouteLineLayerProperty, value);
        }
        private static void OnRouteLineLayerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnRouteLineLayerChanged((Map)o, (MapLayer)e.OldValue, (MapLayer)e.NewValue);
        }
        private static void OnRouteLineLayerChanged(Map map, MapLayer oldValue, MapLayer newValue)
        {
            if (!map.Children.Contains(newValue))
                map.Children.Add(newValue);
        }

        #endregion //RouteLineLayer
    }
}
