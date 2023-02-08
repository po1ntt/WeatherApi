using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;

namespace WeatherApp.ViewsModels
{
    public partial class TownInfoVm : BaseVm
    {
        public ISeries[] Series { get; set; } =
     {
                new LineSeries<DateTimePoint>
                {
                    TooltipLabelFormatter = (chartPoint) =>
                        $"{new DateTime((long) chartPoint.SecondaryValue):H:mm}: {chartPoint.PrimaryValue}",
                    Values = new ObservableCollection<DateTimePoint>
                    {
                        new DateTimePoint(new DateTime(2021, 1, 1, 0, 0, 0), 3.5),
                        new DateTimePoint(new DateTime(2021, 1, 1, 1, 0 ,0), 2.1),
                        new DateTimePoint(new DateTime(2021, 1, 1, 2, 0 ,0), 1.4),
                        new DateTimePoint(new DateTime(2021, 1, 1, 3 ,0 ,0), 9.1),
                         new DateTimePoint(new DateTime(2021, 1, 1, 4 ,0 ,0), 9.1),
                          new DateTimePoint(new DateTime(2021, 1, 1, 5 ,0 ,0), 5.3),
                           new DateTimePoint(new DateTime(2021, 1, 1, 6 ,0 ,0), 3.2),
                            new DateTimePoint(new DateTime(2021, 1, 1, 7 ,0 ,0), 6.4),
                             new DateTimePoint(new DateTime(2021, 1, 1, 8 ,0 ,0), 12.3),

                    },
                    Name="Температура С"



                }
            };
        public TownInfoVm()
        {

        }
        public Axis[] XAxes { get; set; } =
        {
         new Axis
            {
                    Labeler = value => new DateTime((long) value).ToString("H:mm"),
                    LabelsRotation = 0,

                    // when using a date time type, let the library know your unit 
                    UnitWidth = TimeSpan.FromHours(1).Ticks,
                    TextSize = 24,
                    MinStep = TimeSpan.FromHours(1).Ticks
                }
            };
        public Axis[] XYxes { get; set; } =
            {
                new Axis
                {
                    LabelsRotation = 0,

                    TextSize = 24,
                }
            };
    }
}

