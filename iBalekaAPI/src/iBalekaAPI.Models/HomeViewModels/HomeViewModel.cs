using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBalekaAPI.Models.MapViewModels;
using iBalekaAPI.Models.EventViewModels;

namespace iBalekaAPI.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel(EventViewModel[] evnts,RouteViewModel[] routes)
        {
            Events = evnts;
            Routes = routes;
        }

        public HomeViewModel()
        {
        }

        public int NumberOfEvents { get; set; }
        public int NumberOfRoutes { get; set; }
        public EventViewModel[] Events { get; set; }
        public RouteViewModel[] Routes { get; set; }
    }
}
