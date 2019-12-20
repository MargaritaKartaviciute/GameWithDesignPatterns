using backend.Services.Items;
using backend.Services.MapObjects;
using backend.Services.Maps;
using backend.Services.PlayerItems;
using backend.Services.PlayerMatches;
using backend.Services.Players;
using backend.Services.Response;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Helpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
        {
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IPlayerItemsService, PlayerItemsService>();
            services.AddScoped<IPlayerMatchService, PlayerMatchService>();
            services.AddScoped<IMapObjectsService, RockService>();
            services.AddScoped<IMapObjectsService, TreeService>();
            services.AddScoped<IMapObjectsService, WaterService>();
            services.AddScoped<IMapObjectsFactory, MapObjectsFactory>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IResponseService, ResponseService>();

            return services;
        }
    }
}
