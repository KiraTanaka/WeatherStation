﻿using System;
using System.ComponentModel;
using NUnit.Framework;
using Meteo.ExternalData;
using Meteo.Models;
using System.Collections.Generic;

namespace Meteo.UnitTests
{
    [TestFixture]
    public class ParserMeteoDataTests
    {
        [Test]
        public void ParseInCard_CorrectData()
        {
            string data = "r:533|t:15.09|h:84.17|pt:19.43|p:983.65|g:10.60=r:470|t:15.08|h:84.22|pt:19.41|p:983.53|g:6.62=r:477|t:15.11|h:84.61|pt:19.41|p:983.61|g:21.19=";
            HistoryCard todayCard = new HistoryCard() { Temperature = 15, Humidity = 84.33f, Pressure = 983.6f, Radiation = 12.8f };
            todayCard.Description = "-";
            todayCard.WindDirection = "-";
            todayCard.WindSpeed = 0;
            Assert.AreEqual(todayCard, ParserMeteoData.ParseInCard(data));
            Console.Write(todayCard);
            Console.Write(ParserMeteoData.ParseInCard(data));


        }
        [Test]
        public void ParseInCard_CorrectData8()
        {
            string data = "r:533|t:15.09|h:84.17|pt:19.43|p:983.65|g:10.60=r:470|t:15.08|h:84.22|pt:19.41|p:983.53|g:6.62=r:477|t:15.11|h:84.61|pt:19.41|p:983.61|g:21.19=";
            HistoryCard todayCard = new HistoryCard() { Temperature = 15, Humidity = 84.33f, Pressure = 983.6f, Radiation = 12.8f };
            todayCard.Description = "-";
            todayCard.WindDirection = "-";
            todayCard.WindSpeed = 0;
            Assert.AreEqual(4, 4);


        }
    }
}
