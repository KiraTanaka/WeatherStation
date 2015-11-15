using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meteo.Models;

namespace Meteo.BusinessLogic
{
    public static class LogicGetFutureAndPast
    {
        public static JsonCard CreateDayCard(List<JsonCard> listCards)
        {
            if (listCards.Count != 0)
            {
                JsonCard day = new JsonCard();
                day.Date = listCards[0].Date;
                day.Description = listCards[0].Description;
                day.WindDirection = listCards[0].WindDirection;
                int maxCountWindDirection = 1;
                int maxCountDescription = 1;
                int count = 0;
                foreach (var card in listCards)
                {
                    day.Humidity += card.Humidity / listCards.Count;
                    day.Temperature += card.Temperature / listCards.Count;

                    count = listCards.Where(x => x.WindDirection == card.WindDirection).ToList().Count;
                    if (count > maxCountWindDirection)
                    {
                        day.WindDirection = card.WindDirection;
                        maxCountWindDirection = count;
                    }
                    count = listCards.Where(x => x.Description == card.Description).ToList().Count;
                    if (count > maxCountDescription)
                    {
                        day.Description = card.Description;
                        maxCountDescription = count;
                    }
                }
                return day;
            }
            return null;
        }
        public static PackageFutureAndPast CreateEachDayCards(List<JsonCard> forecastCards, List<JsonCard> historyCards)
        {
            PackageFutureAndPast package = new PackageFutureAndPast();
            int countDays = 5;
            DateTime present = DateTime.Now;
            List<JsonCard> listOfFutureAndPastCards = new List<JsonCard>();
            listOfFutureAndPastCards=forecastCards.Concat(historyCards).ToList();
            for (var counter = 0; counter <countDays; counter++)
            {
                JsonCard card = CreateDayCard(listOfFutureAndPastCards.Where(x => 
                    x.Date == (present.AddDays(counter + 1).Day + "." + present.Month))
                    .ToList());
                if (card != null)
                    package.Future.Add(card);
                card = CreateDayCard(listOfFutureAndPastCards.Where(x => 
                    x.Date == (present.AddDays(-counter - 1).Day + "." + present.Month))
                    .ToList());
                if (card != null)
                    package.Past.Add(card);
            }
            return package;
        }
    }
}