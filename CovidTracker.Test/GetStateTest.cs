using CovidTracker.Client.Responses;
using CovidTracker.Models;

using Newtonsoft.Json;

namespace CovidTracker.Test
{
    [TestClass]
    public class GetStateTest
    {
        
        [TestMethod()]
        public void CovidStateModel_ShouldCompare()
        {
            //testing equals, hash of model
            var expectedCovidResults = new CovidStateModel
            {
                dateModified = new DateTime(2021, 03, 07, 14, 50, 00),
                positive = 1023487,
                negative = 0,
                total = 1023487,
                hospitalizedCurrently = 2008
            };

            var actualCovidResults = new CovidStateModel
            {
                dateModified = new DateTime(2021, 03, 07, 14, 50, 00),
                positive = 1023487,
                negative = 0,
                total = 1023487,
                hospitalizedCurrently = 2008
            };
            Assert.AreEqual(expectedCovidResults, actualCovidResults);  

        }

        [TestMethod()]
        public void StateResponse_ShouldCompare()
        {
            //testing equals, hash of model
            var expectedCovidResults = new StateResponse
            {
                dateModified = new DateTime(2021, 03, 07, 14, 50, 00),
                positive = 1023487,
                negative = 0,
                total = 1023487,
                hospitalizedCurrently = 2008
            };

            var actualCovidResults = new StateResponse
            {
                dateModified = new DateTime(2021, 03, 07, 14, 50, 00),
                positive = 1023487,
                negative = 0,
                total = 1023487,
                hospitalizedCurrently = 2008
            };
            Assert.AreEqual(expectedCovidResults, actualCovidResults);
        }
    }
}
