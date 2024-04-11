﻿using BeerAPI.Models;
using BeerAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerAPI.Tests.Services
{
    public class BeerDescriptionServiceTests
    {
        [Fact]
        public void GetDescription_ShouldContain_BeerName ()
        {
            // AAA method

            // Arrange
            var beer = new Beer ();
            beer.Name = "NotABeer";

            // Act
            var sut = new BeerDescriptionService();
            var result = sut.GetDescription(beer);

            // Assert
            Assert.Contains("NotABeer", result);

        }
    }
}
