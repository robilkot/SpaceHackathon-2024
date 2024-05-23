using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceHackathon_2024.ViewModels
{
    public partial class RatingViewModel
    {
        public ObservableCollection<ChartEntry> Entries { get; set; }

        public RatingViewModel()
        {
            Entries = new ObservableCollection<ChartEntry>
            {
                new ChartEntry(212)
                {
                    Label = "Windows",
                    ValueLabel = "112",
                    Color = SKColor.Parse("#2c3e50")
                },
                new ChartEntry(248)
                {
                    Label = "Android",
                    ValueLabel = "648",
                    Color = SKColor.Parse("#77d065")
                },
                new ChartEntry(128)
                {
                    Label = "iOS",
                    ValueLabel = "428",
                    Color = SKColor.Parse("#b455b6")
                },
                new ChartEntry(514)
                {
                    Label = ".NET MAUI",
                    ValueLabel = "214",
                    Color = SKColor.Parse("#3498db")
                }
            };
        }
    }
}
