﻿namespace BalkanAir.Api.Tests
{
    using System.Reflection;
    using System.Web.Http;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MyTested.WebApi;

    using Common;
    using Web.App_Start;

    [TestClass]
    public class TestInitialize
    {
        // The method will be executed before all other tests.
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.BALKAN_AIR_WEB));

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            MyWebApi.IsUsing(config);
        }
    }
}