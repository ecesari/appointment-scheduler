using iPractice.Application.Common.Services;

namespace iPractice.Test.Services
{
    public class TimeSplitterTest
    {

        private readonly TimeSplitter timeSplitter;

        public TimeSplitterTest()
        {
            timeSplitter = new TimeSplitter();
        }

        [Theory]
        [InlineData("2023-01-01T10:00:00Z", "2023-01-01T12:00:00Z",30,4 )]
        public void Given_Dates_ShoulReturn_ListOfTimeSlots(string startTimeString, string endTimeString, int duration, int numberOfTimeSlots)
        {
            var list = timeSplitter.Split(DateTime.Parse(startTimeString), DateTime.Parse(endTimeString), duration);
            Assert.Equal(numberOfTimeSlots, list.Count);
        }
    }
}
