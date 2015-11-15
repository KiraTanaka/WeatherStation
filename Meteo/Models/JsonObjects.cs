using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteo.Models;


namespace Meteo.Models
{
    [JsonObject]
    public class JsonPresentCard {
		[JsonProperty]
        public int 	Temperature { get; set; }
		[JsonProperty]
        public float Humidity { get; set; }
		[JsonProperty]
        public int 	WindSpeed  { get; set; }
		[JsonProperty]
        public float Pressure { get; set; }
		[JsonProperty]
        public float Radiation { get; set; }
		[JsonProperty]
        public string WindDirection { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        public JsonPresentCard (HistoryCard card)
        {
            this.Temperature = card.Temperature;
            this.Humidity = card.Humidity;
            this.Pressure = card.Pressure;
            this.Radiation = card.Radiation;
            this.WindDirection = card.WindDirection;
            this.Description = card.Description;
        }
    }
    [JsonObject]
    public class JsonCard
    {
        [JsonProperty]
        public string Date { get; set; }
        [JsonProperty]
        public int Temperature { get; set; }
        [JsonProperty]
        public float Humidity { get; set; }
        [JsonProperty]
        public string WindDirection { get; set; }
        [JsonProperty]
        public string Description { get; set; }

        public JsonCard() { }
        public JsonCard(Card card) {
            this.Humidity = card.Humidity;
            this.Description = card.Description;
            this.Date = card.DateTime.Day+"."+card.DateTime.Month;
            this.Temperature = card.Temperature;
            this.WindDirection = card.WindDirection;
        }
    }
    [JsonObject]
    public class JsonTodayGraphic
    {
        [JsonProperty]
        public int TimeHour { get; set; }
        [JsonProperty]
        public int TimeMin { get; set; }
        [JsonProperty]
        public int Temperature { get; set; }

        public JsonTodayGraphic(Card card)
        {
            this.TimeHour = card.DateTime.Hour;
            this.TimeMin = card.DateTime.Minute;
            this.Temperature = card.Temperature;
        }
    }
    public class PackageTodayGraphicAndPresent
    {
        [JsonProperty]
        public JsonPresentCard Present { get; set; }
        [JsonProperty]
        public List<JsonTodayGraphic> TodayGraphic { get; set; }

        public PackageTodayGraphicAndPresent()
        {
            this.TodayGraphic = new List<JsonTodayGraphic>();

        }
    }
    public class PackageFutureAndPast
    {
        [JsonProperty]
        public List<JsonCard> Future { get; set; }
        [JsonProperty]
        public List<JsonCard> Past { get; set; }

        public PackageFutureAndPast()
        {
            this.Past = new List<JsonCard>();
            this.Future = new List<JsonCard>();

        }
    }
}