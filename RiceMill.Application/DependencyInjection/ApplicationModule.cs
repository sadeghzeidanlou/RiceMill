﻿using Mapster;
using Microsoft.Extensions.DependencyInjection;
using RiceMill.Application.UseCases.ConcernServices;
using RiceMill.Application.UseCases.DeliveryServices;
using RiceMill.Application.UseCases.DryerHistoryServices;
using RiceMill.Application.UseCases.DryerServices;
using RiceMill.Application.UseCases.IncomeServices;
using RiceMill.Application.UseCases.InputLoadServices;
using RiceMill.Application.UseCases.PaymentServices;
using RiceMill.Application.UseCases.PersonServices;
using RiceMill.Application.UseCases.RiceMillServices;
using RiceMill.Application.UseCases.RiceThreshingServices;
using RiceMill.Application.UseCases.UserActivityServices;
using RiceMill.Application.UseCases.UserServices;
using RiceMill.Application.UseCases.VehicleServices;
using RiceMill.Application.UseCases.VillageServices;
using System.Reflection;

namespace RiceMill.Application.DependencyInjection
{
    public static class ApplicationModule
    {
        /// <summary>
        /// Register all application services and modules
        /// </summary>
        /// <param name="services">Service collection for dependency</param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IConcernCommands, ConcernCommands>();
            services.AddTransient<IConcernQueries, ConcernQueries>();

            services.AddTransient<IRiceMillCommands, RiceMillCommands>();
            services.AddTransient<IRiceMillQueries, RiceMillQueries>();

            services.AddTransient<IUserCommands, UserCommands>();
            services.AddTransient<IUserQueries, UserQueries>();

            services.AddTransient<IUserActivityQueries, UserActivityQueries>();
            services.AddTransient<IUserActivityCommands, UserActivityCommands>();

            services.AddTransient<IVillageQueries, VillageQueries>();
            services.AddTransient<IVillageCommands, VillageCommands>();

            services.AddTransient<IVehicleQueries, VehicleQueries>();
            services.AddTransient<IVehicleCommands, VehicleCommands>();

            services.AddTransient<IPersonQueries, PersonQueries>();
            services.AddTransient<IPersonCommands, PersonCommands>();

            services.AddTransient<IDryerQueries, DryerQueries>();
            services.AddTransient<IDryerCommands, DryerCommands>();

            services.AddTransient<IPaymentQueries, PaymentQueries>();
            services.AddTransient<IPaymentCommands, PaymentCommands>();

            services.AddTransient<IInputLoadQueries, InputLoadQueries>();
            services.AddTransient<IInputLoadCommands, InputLoadCommands>();

            services.AddTransient<IDryerHistoryQueries, DryerHistoryQueries>();
            services.AddTransient<IDryerHistoryCommands, DryerHistoryCommands>();

            services.AddTransient<IRiceThreshingQueries, RiceThreshingQueries>();
            services.AddTransient<IRiceThreshingCommands, RiceThreshingCommands>();

            services.AddTransient<IIncomeQueries, IncomeQueries>();
            services.AddTransient<IIncomeCommands, IncomeCommands>();

            services.AddTransient<IDeliveryQueries, DeliveryQueries>();
            services.AddTransient<IDeliveryCommands, DeliveryCommands>();

            AddMapsTerConfig();

            return services;
        }

        /// <summary>
        /// Register custom config of mapsTer
        /// This model of writing because spell checking
        /// </summary>
        /// <param name="services">Service collection</param>
        private static void AddMapsTerConfig() => TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}