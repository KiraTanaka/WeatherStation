using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteo.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;
using Meteo.ExternalData;
using Meteo.Validation;
using Meteo.BusinessLogic;

namespace Meteo.Controllers
{
    public class CardController : Controller
    {
        private CardContext db = new CardContext();

        public CardController() { }

        public JsonResult GetTodayGraphicAndToday()
        {
            PackageTodayGraphicAndPresent package = new PackageTodayGraphicAndPresent();
            DateTime present = DateTime.Now.Date;
            DateTime endPresent = present.AddDays(1);
            DateTime nearestDate = DateTime.Now.AddMinutes(-30);
            HistoryCard historyCard = db.HistoryCards.FirstOrDefault(x => x.DateTime > nearestDate);
            var historyCardsToday = db.HistoryCards
                .Where(x => 
                x.DateTime > present)
                .ToList()
                .Select(card => new JsonTodayGraphic(card))
                .ToList();
            var forecastCardsToday = db.ForecastCards.Where(x => 
                x.DateTime > present &&
                x.DateTime < endPresent)
                .ToList()
                .Select(card => new JsonTodayGraphic(card))
                .ToList();
            if (historyCard != null)
            {
                package.Present = new JsonPresentCard(historyCard);
            }
            package.TodayGraphic = historyCardsToday.Concat(forecastCardsToday)
                .OrderBy(x => x.TimeHour)
                .ThenBy(y => y.TimeMin)
                .ToList();
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(package, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MeteoData(string data)
        {
            //string data = "r:533|t:15.09|h:84.17|pt:19.43|p:983.65|g:10.60=r:470|t:15.08|h:84.22|pt:19.41|p:983.53|g:6.62=r:477|t:15.11|h:84.61|pt:19.41|p:983.61|g:21.19=";  
            if (data != null)
            {
                HistoryCard historyCard = ParserMeteoData.ParseInCard(data);
                historyCard.DateTime = DateTime.Now;
                if (DataValidation.AllValidation(historyCard))
                {
                    db.HistoryCards.Add(historyCard);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("OpenWeatherData");
        } 

        public JsonResult GetFutureAndPast()
        {
            DateTime present = DateTime.Now;
            DateTime EndDate = DateTime.Now.AddDays(5);
            var forecastCards = db.ForecastCards.Where(x =>
                    x.DateTime.Day != present.Day &&
                    x.DateTime >= present &&
                    x.DateTime <= EndDate)
                    .ToList()
                    .Select(card => new JsonCard(card))
                    .ToList();
            EndDate = DateTime.Now.AddDays(-5);
            var historyCards = db.HistoryCards.Where(x => 
                x.DateTime.Day!=present.Day &&
                x.DateTime <= present && 
                x.DateTime > EndDate)
                .ToList()
                .Select(card => new JsonCard(card))
                .ToList();
            PackageFutureAndPast package = LogicGetFutureAndPast.CreateEachDayCards(forecastCards,historyCards);
         
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(package,JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenWeatherData()
        {
            List<ForecastCard> forecastCards = DownloadOpenWeatherData.ParseInJson().ToCard();
            LogicOpenWeatherData.UpdateData(db,forecastCards);
            return new EmptyResult();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}