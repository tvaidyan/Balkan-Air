﻿namespace BalkanAir.Api.Tests.RouteTests
{
    using System.Net.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MyTested.WebApi;
    using MyTested.WebApi.Exceptions;

    using Newtonsoft.Json;
    using TestObjects;
    using Web.Areas.Api.Controllers;

    [TestClass]
    public class AirportsControllerTests
    {
        private const string CREATE_PATH = "/Api/Airports/Create/";
        private const string GET_PATH_WITH_INVALID_ACTION = "/Api/Airport/";
        private const string GET_PATH = "/Api/Airports/";
        private const string UPDATE_PATH = "/Api/Airports/Update/";
        private const string DELETE_PATH = "/Api/Airports/Delete/";

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void CreateShouldThrowExceptionWithRouteDoesNotExistWhenHttpMethodIsInvalid()
        {
            var airportRequestModel = TestObjectFactory.GetValidAirportRequesModel();
            string jsonContent = JsonConvert.SerializeObject(airportRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Get)
                .To<AirportsController>(a => a.Create(airportRequestModel));
        }

        [TestMethod]
        public void CreateShouldMapCorrectAction()
        {
            var airportRequestModel = TestObjectFactory.GetValidAirportRequesModel();
            string jsonContent = JsonConvert.SerializeObject(airportRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(CREATE_PATH)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Post)
                .To<AirportsController>(a => a.Create(airportRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH_WITH_INVALID_ACTION)
                .To<AirportsController>(a => a.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetAllShouldThrowExceptionWithRouteDoesNotExistWhenControllerIsInvalid()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .To<AircraftsController>(a => a.All());
        }

        [TestMethod]
        public void GetAllShouldMapCorrectAction()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .To<AirportsController>(a => a.All());
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + negativeId)
                .To<AirportsController>(a => a.Get(negativeId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + pathId)
                .To<AirportsController>(a => a.Get(methodId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByIdShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + notIntegerId)
                .To<AirportsController>(a => a.Get(1));
        }

        [TestMethod]
        public void GetByIdShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + validId)
                .To<AirportsController>(a => a.Get(validId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByAbbreviationShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsTooLong()
        {
            var longAbbreviation = "Too long abbreviation";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + longAbbreviation)
                .To<AirportsController>(a => a.GetByAbbreviation(longAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByAbbreviationShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsTooShort()
        {
            var shortAbbreviation = "S";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + shortAbbreviation)
                .To<AirportsController>(a => a.GetByAbbreviation(shortAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByAbbreviationShouldThrowExceptionWithRouteDoesNotExistWhenAbbreviationIsNotString()
        {
            var shortAbbreviation = "123";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + shortAbbreviation)
                .To<AirportsController>(a => a.GetByAbbreviation(shortAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void GetByAbbreviationShouldThrowExceptionWithActionNotMatchWhenAbbreviationIsNull()
        {
            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH)
                .To<AirportsController>(a => a.GetByAbbreviation(null));
        }

        [TestMethod]
        public void GetByAbbreviationShouldMapCorrectAction()
        {
            var shortAbbreviation = "SOF";

            MyWebApi
                .Routes()
                .ShouldMap(GET_PATH + shortAbbreviation)
                .To<AirportsController>(a => a.GetByAbbreviation(shortAbbreviation));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var updateRequestModel = TestObjectFactory.GetValidUpdateAirportRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + negativeId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<AirportsController>(a => a.Update(negativeId, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var updateRequestModel = TestObjectFactory.GetValidUpdateAirportRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + pathId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<AirportsController>(a => a.Update(methodId, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var updateRequestModel = TestObjectFactory.GetValidUpdateAirportRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + notIntegerId)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<AirportsController>(a => a.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void UpdateShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            var updateRequestModel = TestObjectFactory.GetValidUpdateAirportRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Post)
                .To<AirportsController>(a => a.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        public void UpdateShouldMapCorrectAction()
        {
            var updateRequestModel = TestObjectFactory.GetValidUpdateAirportRequestModel();
            var jsonContent = JsonConvert.SerializeObject(updateRequestModel);

            MyWebApi
                .Routes()
                .ShouldMap(UPDATE_PATH + updateRequestModel.Id)
                .WithJsonContent(jsonContent)
                .And()
                .WithHttpMethod(HttpMethod.Put)
                .To<AirportsController>(a => a.Update(updateRequestModel.Id, updateRequestModel));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNegative()
        {
            var negativeId = -1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + negativeId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<AirportsController>(a => a.Delete(negativeId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithDifferenParameterWhenIdDoesNotMatch()
        {
            var pathId = 1;
            var methodId = 2;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + pathId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<AirportsController>(a => a.Delete(methodId));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenIdIsNotInteger()
        {
            var notIntegerId = "a";

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + notIntegerId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<AirportsController>(a => a.Delete(1));
        }

        [TestMethod]
        [ExpectedException(typeof(RouteAssertionException))]
        public void DeleteShouldThrowExceptionWithRouteDoesNotExistWhenActionIsInvalid()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(HttpMethod.Post)
                .To<AirportsController>(a => a.Delete(validId));
        }

        [TestMethod]
        public void DeleteShouldMapCorrectAction()
        {
            var validId = 1;

            MyWebApi
                .Routes()
                .ShouldMap(DELETE_PATH + validId)
                .WithHttpMethod(HttpMethod.Delete)
                .To<AirportsController>(a => a.Delete(validId));
        }
    }
}