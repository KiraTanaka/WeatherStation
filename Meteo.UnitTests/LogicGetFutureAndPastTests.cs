using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NUnit.Framework;
using Meteo.Models;
using Meteo.BusinessLogic;

namespace Meteo.UnitTests
{
    [TestFixture]
    class LogicGetFutureAndPastTests
    {
        [Test]
        public void CheckPackageFutureAndPast(PackageFutureAndPast package,PackageFutureAndPast packageFutureAndPast)
        {
            int count = package.Future.Count;
            for (int i = 0; i < count; i++)
            {
                JsonCard cardFirst = package.Future[i];
                JsonCard cardSecond = packageFutureAndPast.Future[i];
                Assert.AreEqual(cardFirst.Temperature, cardSecond.Temperature);
                Assert.AreEqual(cardFirst.Humidity, cardSecond.Humidity);
                Assert.AreEqual(cardFirst.Date, cardSecond.Date);
                Assert.AreEqual(cardFirst.WindDirection, cardSecond.WindDirection);
                Assert.AreEqual(cardFirst.Description, cardSecond.Description);
            }
            count = package.Past.Count;
            for (int i = 0; i < count; i++)
            {
                JsonCard cardFirst = package.Past[i];
                JsonCard cardSecond = packageFutureAndPast.Past[i];
                Assert.AreEqual(cardFirst.Temperature, cardSecond.Temperature);
                Assert.AreEqual(cardFirst.Humidity, cardSecond.Humidity);
                Assert.AreEqual(cardFirst.Date, cardSecond.Date);
                Assert.AreEqual(cardFirst.WindDirection, cardSecond.WindDirection);
                Assert.AreEqual(cardFirst.Description, cardSecond.Description);
            }
            
        }
        [Test]
        public void CheckCountPackage(PackageFutureAndPast package,PackageFutureAndPast packageFutureAndPast)
        {
            Assert.AreEqual(package.Future.Count, packageFutureAndPast.Future.Count);
            Assert.AreEqual(package.Past.Count,packageFutureAndPast.Past.Count);
        }
        public void CreateEachDayCards_CorrectData()
        {
            List<JsonCard> forecastCards=new List<JsonCard>();
            List<JsonCard> historyCards=new List<JsonCard>();
            historyCards.Add(new JsonCard() { Date="14.11",Temperature = 13, Humidity = 80f, Description = "-", WindDirection = "WE"});
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 13, Humidity = 75, Description = "-", WindDirection = "E" });
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 14, Humidity = 73f, Description = "-", WindDirection = "E" });
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 15, Humidity = 75f, Description = "-", WindDirection = "NE" });
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 15, Humidity = 75f, Description = "-", WindDirection = "NE" });
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 14, Humidity = 70f, Description = "-", WindDirection = "NE" });
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 13, Humidity = 84.33f, Description = "-", WindDirection = "WE" });
            historyCards.Add(new JsonCard() { Date = "14.11", Temperature = 13, Humidity = 84.33f, Description = "-", WindDirection = "WE" });

            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 15, Humidity = 80f, Description = "-", WindDirection = "WE" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 14, Humidity = 75, Description = "-", WindDirection = "E" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 16, Humidity = 73f, Description = "-", WindDirection = "E" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 16, Humidity = 75f, Description = "-", WindDirection = "E" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 15, Humidity = 75f, Description = "-", WindDirection = "E" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 14, Humidity = 70f, Description = "-", WindDirection = "NE" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 13, Humidity = 84.33f, Description = "-", WindDirection = "WE" });
            historyCards.Add(new JsonCard() { Date = "16.11", Temperature = 13, Humidity = 82f, Description = "-", WindDirection = "WE" });
            PackageFutureAndPast package = new PackageFutureAndPast();
            package.Future.Add(new JsonCard() {Date = "16.11", Temperature = 14, Humidity = 77.08f, Description = "-", WindDirection = "NE" });
            package.Past.Add(new JsonCard() {Date = "14.11", Temperature = 13, Humidity = 76.79f, Description = "-", WindDirection = "E" });
            PackageFutureAndPast packageFutureAndPast=LogicGetFutureAndPast.CreateEachDayCards(forecastCards,historyCards);
            CheckCountPackage(package,packageFutureAndPast);
            CheckPackageFutureAndPast(package,packageFutureAndPast);
        }
    }
}
